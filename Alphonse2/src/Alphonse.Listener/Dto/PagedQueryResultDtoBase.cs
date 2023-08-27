using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphonse.Listener.Dto;

public class PagedQueryResultDtoBase<TItem>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int NbTotalPage { get; set; }
    public int NbTotalResults { get; set; }
    public string? SearchPattern { get; set; }
    public ICollection<TItem> Results { get; set; } = Array.Empty<TItem>();
}
