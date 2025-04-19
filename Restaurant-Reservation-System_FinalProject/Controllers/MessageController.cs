using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.Dtos.MessageDtos;
using Restaurant_Reservation_System_.Service.Extensions;
using Restaurant_Reservation_System_.Service.Hubs;
using Restaurant_Reservation_System_.Service.Services.IService;
using Restaurant_Reservation_System_.Service.StaticFiles;
using System.Security.Claims;

namespace Restaurant_Reservation_System_FinalProject.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(IMessageService messageService, IHubContext<ChatHub> chatHubContext, UserManager<AppUser> userManager)
        {
            _messageService = messageService;
            _chatHubContext = chatHubContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var chats = await _messageService.GetUserChatsAsync(userId);
            return View(chats);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var chat = await _messageService.GetChatDetailAsync(id, userId);
            if (chat == null) return NotFound();

            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageSendDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var message = await _messageService.SendMessageAsync(userId, dto);
            if (message == null) return NotFound();

            // Send message via SignalR
            var chat = await _messageService.GetChatDetailAsync(dto.ChatId, userId);
            foreach (var userChat in chat.AppUserChats.Where(m => m.AppUserId != userId))
            {
                var connectionDto = HubDatas.Connections.FirstOrDefault(m => m.UserId == userChat.AppUserId);
                foreach (var connection in connectionDto?.ConnectionIds ?? [])
                {
                    await _chatHubContext.Clients.Client(connection).SendAsync("SendMessage", new
                    {
                        text = message.Text,
                        createdTime = message.CreatedAt
                    });
                }
            }

            return Json(new
            {
                text = message.Text,
                createdTime = message.CreatedAt
            });
        }

    }

}
