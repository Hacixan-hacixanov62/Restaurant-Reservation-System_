using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Reservation_System_.Service.Dtos.TableDtos
{
    public class TableCreateDto
    {
        public int TableNo { get; set; }
        [Range(0, 10)]
        public int PersonCount { get; set; }
    }
}
