using System;
using Microsoft.EntityFrameworkCore;
using entity.DB;
using entity.Enum;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, FullName = "Ahmed Mejri", Department = "IT", JoiningDate = DateTime.Parse("2025-01-01") },
            new Employee { Id = 2, FullName = "Flen ben Flen", Department = "RH", JoiningDate = DateTime.Parse("2020-05-10") },
            new Employee { Id = 3, FullName = "Sami Trabelsi", Department = "Finance", JoiningDate = DateTime.Parse("2023-03-15") },
            new Employee { Id = 4, FullName = "Nada Belhaj", Department = "Marketing", JoiningDate = DateTime.Parse("2022-11-01") },
            new Employee { Id = 5, FullName = "Rania Hammami", Department = "HR", JoiningDate = DateTime.Parse("2021-07-12") },
            new Employee { Id = 6, FullName = "Khalil Bouzid", Department = "IT", JoiningDate = DateTime.Parse("2024-02-20") },
            new Employee { Id = 7, FullName = "Maha Gharbi", Department = "Legal", JoiningDate = DateTime.Parse("2022-01-25") },
            new Employee { Id = 8, FullName = "Anis Jaziri", Department = "Logistics", JoiningDate = DateTime.Parse("2020-09-10") },
            new Employee { Id = 9, FullName = "Lina Ksiksi", Department = "Procurement", JoiningDate = DateTime.Parse("2023-05-30") },
            new Employee { Id = 10, FullName = "Bassem Ayari", Department = "Finance", JoiningDate = DateTime.Parse("2021-04-04") }
            );


            modelBuilder.Entity<LeaveRequest>().HasData(
                new LeaveRequest
                {
                    Id = 1,
                    EmployeeId = 1,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2025, 4, 1),
                    EndDate = new DateTime(2025, 4, 6),
                    Status = LeaveStatus.Pending,
                    Reason = "Vacation",
                    CreatedAt = new DateTime(2025, 4, 1, 8, 0, 0)
                },
                new LeaveRequest
                {
                    Id = 2,
                    EmployeeId = 2,
                    LeaveType = LeaveType.Sick,
                    StartDate = new DateTime(2025, 3, 10),
                    EndDate = new DateTime(2025, 3, 12),
                    Status = LeaveStatus.Pending,
                    Reason = "Cold symptoms",
                    CreatedAt = new DateTime(2025, 3, 9, 10, 30, 0)
                },
                new LeaveRequest
                {
                    Id = 3,
                    EmployeeId = 3,
                    LeaveType = LeaveType.Other,
                    StartDate = new DateTime(2025, 2, 20),
                    EndDate = new DateTime(2025, 2, 22),
                    Status = LeaveStatus.Pending,
                    Reason = "Personal matters",
                    CreatedAt = new DateTime(2025, 2, 19, 9, 15, 0)
                },
                new LeaveRequest
                {
                    Id = 4,
                    EmployeeId = 4,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2025, 5, 1),
                    EndDate = new DateTime(2025, 5, 10),
                    Status = LeaveStatus.Pending,
                    Reason = "Family trip",
                    CreatedAt = new DateTime(2025, 4, 20, 13, 45, 0)
                },
                new LeaveRequest
                {
                    Id = 5,
                    EmployeeId = 5,
                    LeaveType = LeaveType.Sick,
                    StartDate = new DateTime(2025, 1, 5),
                    EndDate = new DateTime(2025, 1, 7),
                    Status = LeaveStatus.Pending,
                    Reason = "Migraine",
                    CreatedAt = new DateTime(2025, 1, 4, 8, 10, 0)
                },
                new LeaveRequest
                {
                    Id = 6,
                    EmployeeId = 6,
                    LeaveType = LeaveType.Other,
                    StartDate = new DateTime(2025, 4, 15),
                    EndDate = new DateTime(2025, 4, 17),
                    Status = LeaveStatus.Pending,
                    Reason = "Legal paperwork",
                    CreatedAt = new DateTime(2025, 4, 10, 10, 0, 0)
                },
                new LeaveRequest
                {
                    Id = 7,
                    EmployeeId = 7,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2025, 6, 5),
                    EndDate = new DateTime(2025, 6, 15),
                    Status = LeaveStatus.Pending,
                    Reason = "Summer vacation",
                    CreatedAt = new DateTime(2025, 5, 25, 14, 20, 0)
                },
                new LeaveRequest
                {
                    Id = 8,
                    EmployeeId = 8,
                    LeaveType = LeaveType.Sick,
                    StartDate = new DateTime(2025, 3, 1),
                    EndDate = new DateTime(2025, 3, 3),
                    Status = LeaveStatus.Pending,
                    Reason = "Stomach flu",
                    CreatedAt = new DateTime(2025, 2, 28, 11, 0, 0)
                },
                new LeaveRequest
                {
                    Id = 9,
                    EmployeeId = 9,
                    LeaveType = LeaveType.Annual,
                    StartDate = new DateTime(2025, 7, 10),
                    EndDate = new DateTime(2025, 7, 20),
                    Status = LeaveStatus.Pending,
                    Reason = "Wedding leave",
                    CreatedAt = new DateTime(2025, 6, 30, 15, 30, 0)
                },
                new LeaveRequest
                {
                    Id = 10,
                    EmployeeId = 10,
                    LeaveType = LeaveType.Other,
                    StartDate = new DateTime(2025, 4, 25),
                    EndDate = new DateTime(2025, 4, 28),
                    Status = LeaveStatus.Pending,
                    Reason = "Moving house",
                    CreatedAt = new DateTime(2025, 4, 20, 9, 0, 0)
                }
        );

        }
    }
}
