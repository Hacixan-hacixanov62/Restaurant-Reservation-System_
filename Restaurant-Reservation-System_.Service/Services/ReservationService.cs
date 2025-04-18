using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Dtos.ReservationDtos;
using Restaurant_Reservation_System_.Service.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        private readonly AppDbContext _appDbContext;
        public ReservationService(IMapper mapper,IReservationRepository reservationRepository,AppDbContext appDbContext)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _appDbContext = appDbContext;
            
        }

        public async Task<bool> CreateAsync(ReservationCreateDto dto, ModelStateDictionary ModelState)
        {

            if (!ModelState.IsValid)
                return false;

            var reservation = _mapper.Map<Reservation>(dto);

            await _reservationRepository.CreateAsync(reservation);
            await _reservationRepository.SaveChangesAsync();

            return true;
        }

        

        public async Task<Reservation> RepairOrEnd(int id)
        {
           var reservation = await _reservationRepository.GetAll().FirstOrDefaultAsync(m=>m.Id ==id);

            if (reservation == null)
            {
                throw new DirectoryNotFoundException("Reservation NotFound");
            }

            if (reservation.IsDone)
                reservation.IsDone = false;
            else
                reservation.IsDone = true;

            return reservation;
        }

        public async Task<ReservationDto?> GetLatestReservationAsync(string name, string email)
        {
            var reservation = await _reservationRepository.GetAll()
                 .Where(r => r.Name == name && r.Email == email)
                 .OrderByDescending(r => r.Date)
                 .ThenByDescending(r => r.Id)
                 .FirstOrDefaultAsync();

            if (reservation == null)
                return null;

            var reservationDto = _mapper.Map<ReservationDto>(reservation);
            return reservationDto;
        }

        //public async Task<ReservationDto> GetReservationAsync(int id)
        //{
        //    var reservation = await _reservationRepository.GetAll()
        //        .Include(r => r.Products)
        //        .FirstOrDefaultAsync(r => r.Id == id);

        //    if (reservation == null)
        //        throw new KeyNotFoundException("Reservation not found");

        //    var reservationDto = _mapper.Map<ReservationDto>(reservation);


        //    return reservationDto;
        //}
    }
}
