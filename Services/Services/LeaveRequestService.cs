using AutoMapper;
using Data;
using entity.DB;
using entity.DTO;
using entity.Enum;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LeaveRequestService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LeaveRequestDto>> GetAll()
        {
            var data = await _context.LeaveRequests.Include(e => e.Employee).ToListAsync();
            return _mapper.Map<List<LeaveRequestDto>>(data);
        }

        public async Task<LeaveRequestCreationDto> Create(LeaveRequestCreationDto dto)
        {
            if (dto.LeaveType == "Sick" && string.IsNullOrWhiteSpace(dto.Reason))
            {
                throw new InvalidOperationException("Le motif est obligatoire pour un congé maladie.");
            }

            var entity = _mapper.Map<LeaveRequest>(dto);
            entity.CreatedAt = DateTime.Now;
            entity.Status = LeaveStatus.Pending;

            var employee = await _context.Employees.FindAsync(dto.EmployeeId);
            if (employee == null)
                throw new InvalidOperationException("Employé introuvable.");

            int requestedDays = (dto.EndDate - dto.StartDate).Days + 1;

            if (dto.LeaveType == "Sick")
            {
                employee.SickLeaveDays += requestedDays;
            }
            else
            {
                if (employee.AnnualLeaveDaysUsed + requestedDays > 20)
                    throw new InvalidOperationException("Limite annuelle de 20 jours dépassée.");

                employee.AnnualLeaveDaysUsed += requestedDays;
            }

            employee.TotalLeaveDaysUsed = employee.AnnualLeaveDaysUsed + employee.SickLeaveDays;

            _context.LeaveRequests.Add(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<LeaveRequestCreationDto>(entity);
        }

        public async Task<LeaveRequestUpdateDTO> Update(int id, LeaveRequestUpdateDTO dto)
        {
            var entity = await _context.LeaveRequests.FindAsync(id);
            if (entity == null) return null;

            if (entity.Status != LeaveStatus.Pending)
            {
                throw new InvalidOperationException("Impossible de modifier une demande qui a déjà été traitée.");
            }

            var leaveType = Enum.Parse<LeaveType>(dto.LeaveType);
            if (leaveType == LeaveType.Sick && string.IsNullOrWhiteSpace(dto.Reason))
            {
                throw new InvalidOperationException("Le motif est obligatoire pour un congé maladie.");
            }

            entity.LeaveType = leaveType;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.Reason = dto.Reason;

            await _context.SaveChangesAsync();
            return _mapper.Map<LeaveRequestUpdateDTO>(entity);
        }

        public async Task<LeaveRequestApproveDTO> confirmation(int id, LeaveRequestApproveDTO dto)
        {
            var entity = await _context.LeaveRequests
                .Include(lr => lr.Employee)
                .FirstOrDefaultAsync(lr => lr.Id == id);

            if (entity == null) return null;

            if (entity.Status != LeaveStatus.Pending)
            {
                throw new InvalidOperationException("Impossible de modifier une demande qui a déjà été traitée.");
            }

            var newStatus = Enum.Parse<LeaveStatus>(dto.Status);

            if (newStatus == LeaveStatus.Approved)
            {
                int daysRequested = (int)(entity.EndDate - entity.StartDate).TotalDays + 1;

                if (entity.LeaveType == LeaveType.Sick)
                {
                    entity.Employee.SickLeaveDays += daysRequested;
                }
                else
                {
                    if (entity.Employee.AnnualLeaveDaysUsed + daysRequested > 20)
                        throw new InvalidOperationException("Limite annuelle de 20 jours dépassée.");

                    entity.Employee.AnnualLeaveDaysUsed += daysRequested;
                }

                entity.Employee.TotalLeaveDaysUsed = entity.Employee.SickLeaveDays + entity.Employee.AnnualLeaveDaysUsed;
            }

            entity.Status = newStatus;
            await _context.SaveChangesAsync();

            return _mapper.Map<LeaveRequestApproveDTO>(entity);
        }



        public async Task<bool> Delete(int id)
        {
            var entity = await _context.LeaveRequests.FindAsync(id);
            if (entity == null) return false;

            _context.LeaveRequests.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetFilteredLeaveRequests(LeaveRequestFilterDto filter)
        {
            var query = _context.LeaveRequests.Include(lr => lr.Employee).AsQueryable();

            if (filter.EmployeeId.HasValue)
                query = query.Where(lr => lr.EmployeeId == filter.EmployeeId.Value);

            if (!string.IsNullOrEmpty(filter.LeaveType))
                query = query.Where(l => l.LeaveType.ToString().ToLower() == filter.LeaveType.ToLower());

            if (!string.IsNullOrEmpty(filter.Status))
                query = query.Where(l => l.Status.ToString().ToLower() == filter.Status.ToLower());
;

            if (filter.StartDate.HasValue)
                query = query.Where(lr => lr.StartDate >= filter.StartDate.Value);

            if (filter.EndDate.HasValue)
                query = query.Where(lr => lr.EndDate <= filter.EndDate.Value);

            if (!string.IsNullOrEmpty(filter.Keyword))
                query = query.Where(lr => lr.Reason.ToString().ToLower() == filter.Keyword.ToLower());


            query = query.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);

            query = filter.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(lr => EF.Property<object>(lr, filter.SortBy))
                : query.OrderBy(lr => EF.Property<object>(lr, filter.SortBy));

            var leaveRequests = await query.Select(lr => new LeaveRequestDto
            {
                Id = lr.Id,
                EmployeeName = lr.Employee.FullName,
                LeaveType = lr.LeaveType.ToString(),
                Status = lr.Status.ToString(),
                Reason = lr.Reason,
                StartDate = lr.StartDate,
                EndDate = lr.EndDate
            }).ToListAsync();

            return leaveRequests;
        }

        public async Task<IEnumerable<LeaveReportDto>> GetLeaveReportAsync(LeaveReportFilterDto filter)
        {
            var query = _context.LeaveRequests
                .Include(lr => lr.Employee)
                .Where(lr =>
                    (filter.Year == null ||
                        lr.StartDate.Year == filter.Year || lr.EndDate.Year == filter.Year) &&
                    (string.IsNullOrEmpty(filter.Department) || lr.Employee.Department == filter.Department) &&
                    (!filter.StartDate.HasValue || lr.StartDate >= filter.StartDate.Value) &&
                    (!filter.EndDate.HasValue || lr.EndDate <= filter.EndDate.Value)
                );

            var leaveRequests = await query.ToListAsync();

            var grouped = leaveRequests
                .GroupBy(lr => new { lr.Employee.Id, lr.Employee.FullName, lr.Employee.Department })
                .Select(g => new LeaveReportDto
                {
                    FullName = g.Key.FullName,
                    Department = g.Key.Department,
                    AnnualLeaveDaysUsed = g.Where(x => x.LeaveType == LeaveType.Annual)
                                           .Sum(x => (x.EndDate - x.StartDate).Days + 1),
                    SickLeaveDays = g.Where(x => x.LeaveType == LeaveType.Sick)
                                     .Sum(x => (x.EndDate - x.StartDate).Days + 1),
                    TotalLeaveDaysUsed = g.Sum(x => (x.EndDate - x.StartDate).Days + 1)
                })
                .ToList();

            return grouped;
        }

    }
}
