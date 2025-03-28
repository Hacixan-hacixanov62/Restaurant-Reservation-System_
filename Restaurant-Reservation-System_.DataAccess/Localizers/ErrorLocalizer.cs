

using Microsoft.Extensions.Localization;
using System.Text;


namespace Restaurant_Reservation_System_.DataAccess.Localizers
{
    public class ErrorLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public ErrorLocalizer(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create("Errors", "Restaurant");
        }

        public string GetValue(string key)
        {
            return _localizer.GetString(key);
        }
    }
}
