using Microsoft.AspNetCore.Mvc;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    public class ThemeController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> SetTheme([FromBody] ThemeRequest request)
        {
            Response.Cookies.Append("mode", request.Theme, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });

            return Ok();
        }
    }

    public class ThemeRequest
    {
        public string Theme { get; set; } // "dark" və ya "light"
    }
}
