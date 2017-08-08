
using System.Collections.Generic;

namespace F21.BLOGGER.Model
{
    public class PageList<T>
    {
        public long TotalCount { get; set; }
        public IList<T> Items { get; set; }
    }
}
