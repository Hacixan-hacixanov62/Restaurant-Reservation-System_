using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.Abstractions.Exceptions
{
    public interface IBaseException
    {
        public HttpStatusCode StatusCode { get; set; }
    }
}
