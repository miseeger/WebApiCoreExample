using System.Collections.Generic;

namespace WebApiFull.Models.Services
{
	public class DataPage<T>
    {
	    public PaginationMeta Pagination { get; set; }
		public IEnumerable<T> Data { get; set; }

		public DataPage()
	    {
			Pagination = new PaginationMeta();
	    }
    }
}
