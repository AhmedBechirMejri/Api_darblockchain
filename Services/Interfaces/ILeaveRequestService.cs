using entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<List<LeaveRequestDto>> GetAll();
        Task<LeaveRequestCreationDto> Create(LeaveRequestCreationDto dto);
        Task<LeaveRequestUpdateDTO> Update(int id, LeaveRequestUpdateDTO dto);
        Task<LeaveRequestApproveDTO> confirmation(int id, LeaveRequestApproveDTO dto);
        Task<bool> Delete(int id);
        Task<IEnumerable<LeaveRequestDto>> GetFilteredLeaveRequests(LeaveRequestFilterDto filter);
        Task<IEnumerable<LeaveReportDto>> GetLeaveReportAsync(LeaveReportFilterDto filter);
    }
}
