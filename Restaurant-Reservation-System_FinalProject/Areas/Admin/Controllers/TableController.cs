using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reservation_System_.DataAccess.DAL;
using Restaurant_Reservation_System_.Service.Dtos.CategoryDtos;
using Restaurant_Reservation_System_.Service.Dtos.TableDtos;
using Restaurant_Reservation_System_.Service.Services;
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

        public async Task<IActionResult> Index(int page =1)
        {
            int pageCount = (int)Math.Ceiling((decimal)_context.Tables.Count() / 10);

            if (pageCount == 0)
                pageCount = 1;

            ViewBag.PageCount = pageCount;

            if (page > pageCount)
                page = pageCount;

            if (page <= 0)
                page = 1;

            ViewBag.CurrentPage = page;

            var tables = await _context.Tables.OrderByDescending(x => x.Id).Skip((page - 1) * 10).Take(10).ToListAsync();
            return View(tables);
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
            if (id == null)
            {
                return NotFound();
            }

            var table = await _tableService.DetailAsync(id.Value);
            if (table == null)
            {
                return NotFound();
            }

            return View(new TableGetDto
            {
                TableNo = table.TableNo,
                PersonCount = table.PersonCount,
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,TableGetDto tableUpdateDto )
        {
            var existTable = await _context.Tables.FirstOrDefaultAsync(x => x.Id == id);
            if (existTable is null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(tableUpdateDto);

            var isExist = await _context.Tables.AnyAsync(x => x.TableNo == tableUpdateDto.TableNo);

            if (isExist)
            {
                ModelState.AddModelError("TableNo", "Table already exist");
                return View(tableUpdateDto);
            }

            var allowedPersonCount = (tableUpdateDto.PersonCount != 2 && tableUpdateDto.PersonCount != 4 && tableUpdateDto.PersonCount != 6 && tableUpdateDto.PersonCount != 8 && tableUpdateDto.PersonCount != 10);

            if (allowedPersonCount)
            {
                ModelState.AddModelError("PersonCount", "Person Count can only 2,4,6,8,10");
                return View(tableUpdateDto);

            }

            await _tableService.EditAsync(id,tableUpdateDto);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _tableService.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("admin/Table/detail")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
              var table =await _tableService.DetailAsync(id);
                return View(table);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
