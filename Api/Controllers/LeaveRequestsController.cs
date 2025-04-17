using entity.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/leaverequests")]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly ILeaveRequestService _service;

        public LeaveRequestsController(ILeaveRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllApi()
        {
            var list = await _service.GetAll();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateApi([FromBody] LeaveRequestCreationDto dto)
        {
            var created = await _service.Create(dto);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApi(int id, [FromBody] LeaveRequestUpdateDTO dto)
        {
            var updated = await _service.Update(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ConfirmationApi(int id, [FromBody] LeaveRequestApproveDTO dto)
        {
            var updated = await _service.confirmation(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApi(int id)
        {
            var success = await _service.Delete(id);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> GetFilteredLeaveRequests(
        [FromQuery] LeaveRequestFilterDto filter)
        {
            try
            {
                var leaveRequests = await _service.GetFilteredLeaveRequests(filter);
                return Ok(leaveRequests);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpGet("report")]
        public async Task<ActionResult<IEnumerable<LeaveReportDto>>> GetLeaveReport([FromQuery] LeaveReportFilterDto filter)
        {
            var report = await _service.GetLeaveReportAsync(filter);
            return Ok(report);
        }
    }
}
