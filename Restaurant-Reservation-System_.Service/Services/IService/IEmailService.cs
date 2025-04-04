namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
        Task SendEmailAsync(EmailSendDto dto);
    }
}
