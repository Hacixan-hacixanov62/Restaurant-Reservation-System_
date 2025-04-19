

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Dtos.MessageDtos;
using Restaurant_Reservation_System_.Service.Services.IService;
using System.Security.Claims;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly UserManager<AppUser> _userManager;

        public MessageService(IMessageRepository messageRepository, UserManager<AppUser> userManager)
        {
            _messageRepository = messageRepository;
            _userManager = userManager;
        }

        public async Task<Message?> SendMessageAsync(string userId, MessageSendDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var chat = await _messageRepository.GetChatWithUsersAndMessagesAsync(dto.ChatId, userId);
            if (chat == null) return null;

            var message = new Message
            {
                Text = dto.Text,
                ChatId = dto.ChatId,
                SenderId = userId,
                CreatedAt = DateTime.UtcNow
            };

            return await _messageRepository.AddMessageAsync(message);
        }

        public async Task<Chat?> GetChatDetailAsync(int chatId, string userId)
        {
            return await _messageRepository.GetChatWithUsersAndMessagesAsync(chatId, userId);
        }

        public async Task<List<Chat>> GetUserChatsAsync(string userId)
        {
            return await _messageRepository.GetUserChatsAsync(userId);
        }
    }
}
