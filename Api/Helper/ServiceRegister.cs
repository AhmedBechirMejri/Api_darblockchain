using Services.Interfaces;
using Services.Services;

namespace Api.Helper
{
    public static class ServiceRegister
    {
        public static void RegisterLocalServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(LeaveMappingProfile).Assembly);
            builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
        }
    }
}
