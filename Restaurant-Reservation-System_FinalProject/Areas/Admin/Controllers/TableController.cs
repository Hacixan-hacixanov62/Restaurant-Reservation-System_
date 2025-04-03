using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.Dtos.TableDtos;
using Restaurant_Reservation_System_.Service.Services.IService;

namespace Restaurant_Reservation_System_FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Superadmin")]
    public class TableController : Controller
    {
        private readonly ITableService _tableService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TableController(ITableService tableService,AppDbContext context,IMapper mapper)
        {            
            _tableService = tableService;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] TableCreateDto tableCreateDto)
        {
            var isExist = await _context.Tables.AnyAsync(x => x.TableNo == tableCreateDto.TableNo);

            if (isExist)
            {
                ModelState.AddModelError("TableNo", "Table already exist");
                return View(tableCreateDto);
            }
            var allowedPersonCount = (tableCreateDto.PersonCount != 2 && tableCreateDto.PersonCount != 4 && tableCreateDto.PersonCount != 6 && tableCreateDto.PersonCount != 8 && tableCreateDto.PersonCount != 10);

            if (allowedPersonCount)
            {
                ModelState.AddModelError("PersonCount", "Person Count can only 2,4,6,8,10");
                return View(tableCreateDto);

            }
            await _tableService.CreateAsync(tableCreateDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,TableUpdateDto tableUpdateDto )
        {
            return View();
        }

    }
}
