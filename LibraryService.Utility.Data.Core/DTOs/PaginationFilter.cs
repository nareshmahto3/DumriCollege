using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryService.Utility.Data.Core.DTOs
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Skip => (PageNumber - 1) * PageSize;
    }
}
