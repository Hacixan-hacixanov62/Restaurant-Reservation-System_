

using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.Service.Dtos.MessageDtos;

namespace Restaurant_Reservation_System_.Service.Services.IService
{
    public interface IMessageService
    {
        Task<Message?> SendMessageAsync(string userId, MessageSendDto dto);
        Task<Chat?> GetChatDetailAsync(int chatId, string userId);
        Task<List<Chat>> GetUserChatsAsync(string userId);
    }
}
