using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.Core.Entittes;
using Restaurant_Reservation_System_.DataAccess.Repositories;
using Restaurant_Reservation_System_.DataAccess.Repositories.IRepositories;
using Restaurant_Reservation_System_.Service.Dtos.TableDtos;
using Restaurant_Reservation_System_.Service.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.Service.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TableService(ITableRepository tableRepository,IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;            
        }
        public async Task CreateAsync(TableCreateDto tableCreateDto)
        {
            Table table = _mapper.Map<Table>(tableCreateDto);

            //var usernsme = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            //table.CreatedBy = usernsme;
            //table.UpdatedBy = usernsme;
            //table.CreatedAt = DateTime.UtcNow;
            //table.UpdatedAt = DateTime.UtcNow;

            await _tableRepository.CreateAsync(table);
            await _tableRepository.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var table = await _tableRepository.GetAll().FirstOrDefaultAsync(m => m.Id == id);
            if (table == null)
            {
                throw new Exception("Topic tapılmadı");
            }

            _tableRepository.Delete(table);
            await _tableRepository.SaveChangesAsync();
        }

        public async Task<Table> DetailAsync(int id)
        {
            var topic = await _tableRepository.GetAll()
                 .Where(s => s.Id == id)
                 .Select(s => new Table
                 {
                     Id = s.Id,
                     TableNo = s.TableNo,
                     PersonCount = s.PersonCount
                 })
                 .FirstOrDefaultAsync();

            if (topic == null)
            {
                throw new Exception("Topic tapılmadı");
            }

            return topic;
        }

        public async Task EditAsync(int id, TableUpdateDto tableUpdateDto)
        {
            var table = await _tableRepository.GetAll().FirstOrDefaultAsync(m=>m.Id == id);
            if (table == null)
            {
                throw new Exception("Table tapılmadı");
            }

            TableUpdateDto dto = _mapper.Map<TableUpdateDto>(table);

            table = _mapper.Map(tableUpdateDto,table);

            _tableRepository.Update(table);
            await _tableRepository.SaveChangesAsync();

        }

        public async Task<List<Table>> GetAllAsync()
        {
            var topics = await _tableRepository.GetAll().ToListAsync();
            return topics.Select(s => new Table
            {
                Id = s.Id,
               TableNo= s.TableNo,
               PersonCount = s.PersonCount
            }).ToList();
        }
    }
}
