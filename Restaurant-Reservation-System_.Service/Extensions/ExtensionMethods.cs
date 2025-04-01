
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Restaurant_Reservation_System_.Service.Extensions
{
    public static class ExtensionMethods
    {

        //public static async Task InitDatabaseAsync(this WebApplication app)
        //{
        //    using (var scope = app.Services.CreateScope())
        //    {
        //        var initializer = scope.ServiceProvider.GetRequiredService<DataInit>();
        //        await initializer.InitDatabaseAsync();
        //    }
        //}
        //public static void RenderSelectedLanguage(this WebApplication app)
        //{
        //    using (var scope = app.Services.CreateScope())
        //    {
        //        var languageService = scope.ServiceProvider.GetRequiredService<ILanguageService>();
        //        languageService.RenderSelectedLanguage();
        //    }
        //}
        public static string GetReturnUrl(this HttpRequest Request)
        {
            string? returnUrl = Request.Headers["Referer"];

            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "/";

            return returnUrl;
        }
    }
}
