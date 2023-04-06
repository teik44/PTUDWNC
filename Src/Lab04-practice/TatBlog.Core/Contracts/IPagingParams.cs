using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.Core.Contracts
{
   //thong tin phân trang
    public interface IPagingParams
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
        string SortColumn { get; set; }
        string SortOrder { get; set; }
    }
}