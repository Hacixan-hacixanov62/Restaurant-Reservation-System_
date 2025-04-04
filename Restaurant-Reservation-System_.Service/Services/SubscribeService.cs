using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.Localizers;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Dtos.SubscribeDtos;
using Restaurant_Reservation_System_.Service.Exceptions;
using Restaurant_Reservation_System_.Service.Services.IService;


namespace Restaurant_Reservation_System_.Service.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly ISubscribeRepository _subscribeRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public SubscribeService(ISubscribeRepository subscribeRepository,IMapper mapper,IEmailService emailService)
        {            
            _subscribeRepository = subscribeRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<bool> CreateAsync(SubscribeCreateDto dto, ModelStateDictionary ModelState)
        {
            if (!ModelState.IsValid)
                return false;

            var isExist = await _subscribeRepository.IsExistAsync(x => x.Email.ToUpper() == dto.Email.ToUpper());

            if (isExist)
            {
                ModelState.AddModelError("Email", "Bu email artıq mövcuddur");
                return false;
            }

            var subscribe = _mapper.Map<Subscribe>(dto);

            await _subscribeRepository.CreateAsync(subscribe);
            await _subscribeRepository.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var subscribe = await _subscribeRepository.GetAsync(id);

            if (subscribe is null)
                throw new NotFoundException("Not Found");

            _subscribeRepository.Delete(subscribe);
            await _subscribeRepository.SaveChangesAsync();
        }

        public async Task<List<SubscribeGetDto>> GetAllAsync()
        {
            var subscribes = await _subscribeRepository.GetAll().ToListAsync();

            var dtos = _mapper.Map<List<SubscribeGetDto>>(subscribes);

            return dtos;
        }

        public async Task<SubscribeGetDto> GetAsync(int id)
        {
            var subscribe = await _subscribeRepository.GetAsync(id);

            if (subscribe is null)
                throw new NotFoundException("Not Found");

            var dto = _mapper.Map<SubscribeGetDto>(subscribe);

            return dto;
        }

        public async Task<SubscribeUpdateDto> GetUpdatedDtoAsync(int id)
        {
            var subscribe = await _subscribeRepository.GetAsync(id);

            if (subscribe is null)
                throw new NotFoundException("Not Found");

            var dto = _mapper.Map<SubscribeUpdateDto>(subscribe);

            return dto;
        }

        public async Task<bool> SendEmailToSubscribres(SubscribeEmailDto dto, ModelStateDictionary ModelState)
        {

            if (!ModelState.IsValid)
                return false;

            var subscribes = await _subscribeRepository.GetAll().ToListAsync();


            foreach (var subscribe in subscribes)
            {
                var emailDto = new EmailSendDto()
                {
                    Body = dto.Body,
                    Subject = dto.Subject,
                    Attachments = dto.Attachments ?? new(),
                    ToEmail = subscribe.Email
                };

                await _emailService.SendEmailAsync(emailDto);
            }

            return true;
        }

        public async Task<bool> UpdateAsync(SubscribeUpdateDto dto, ModelStateDictionary ModelState)
        {
            if (!ModelState.IsValid)
                return false;

            var existSubscribe = await _subscribeRepository.GetAsync(x => x.Id == dto.Id);

            if (existSubscribe is null)
                throw new NotFoundException("Not Found");

            var isExist = await _subscribeRepository.IsExistAsync(x => x.Email == dto.Email.ToUpper() && x.Id != dto.Id);

            if (isExist)
            {
                ModelState.AddModelError("Email", "Bu email artıq mövcuddur");
                return false;
            }

            existSubscribe = _mapper.Map(dto, existSubscribe);

            _subscribeRepository.Update(existSubscribe);
            await _subscribeRepository.SaveChangesAsync();

            return true;
        }
    }
}
