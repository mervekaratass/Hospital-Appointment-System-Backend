using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmsController : ControllerBase
    {
        private readonly ILogger<SmsController> _logger;

        public SmsController(ILogger<SmsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult SendSms([FromBody] SmsRequest request)
        {
            // SMS gönderim simülasyonu
            _logger.LogInformation($"SMS gönderildi: {request.PhoneNumber} - {request.Message}");

            // Başarılı cevap döndürme
            return Ok(new { message = "SMS başarıyla gönderildi (simülasyon)" });
        }
    }

    public class SmsRequest
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
