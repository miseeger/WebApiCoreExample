using LiteDB;
using System.Collections.Generic;
using System.Linq;
using WebApiFull.Interfaces;
using WebApiFull.Models.Services;

namespace WebApiFull.Services
{
	public class DataService : IDataService
    {
	    public LiteDatabase Db { get; set; }


		public DataService()
	    {
		    Db = new LiteDatabase("VueDb.dbl");
	    }


	    public DataPage<T> CreatePageData<T>(int perPage, string pageUrl, int page) 
			where T : new()
	    {
		    var collection = Db.GetCollection<T>(typeof(T).Name);
		    var data = collection.FindAll().ToList();

		    return CreatePageData<T>(data, perPage, pageUrl, page);
	    }
		

		public DataPage<T> CreatePageData<T>(List<T> data, int perPage, string pageUrl, 
			int page) where T : new()
		{
			var p = new DataPage<T>
			{
				Pagination =
					{
						PerPage = perPage,
						Total = data.Count()
					}
			};

			if (p.Pagination.Total > 0)
			{
				p.Pagination.LastPage = 
					p.Pagination.Total % p.Pagination.PerPage > 0 
						? (p.Pagination.Total / p.Pagination.PerPage) + 1 
						: p.Pagination.Total / p.Pagination.PerPage;
				p.Pagination.CurrentPage =
					page > p.Pagination.LastPage
						? p.Pagination.LastPage
						: page;
				p.Pagination.NextPageUrl =
					p.Pagination.CurrentPage < p.Pagination.LastPage
						? $"/{pageUrl}/{p.Pagination.CurrentPage + 1}"
						: string.Empty;
				p.Pagination.PrevPageUrl =
					p.Pagination.CurrentPage > 1 ?
						$"/{pageUrl}/{p.Pagination.CurrentPage - 1}"
						: string.Empty;
				p.Pagination.From = ((p.Pagination.CurrentPage - 1) * p.Pagination.PerPage) + 1;
				p.Pagination.To =
					p.Pagination.Total <= p.Pagination.PerPage
						? p.Pagination.Total
						: p.Pagination.CurrentPage * p.Pagination.PerPage;
				p.Data = data
					.Skip(p.Pagination.From - 1)
					.Take(p.Pagination.PerPage)
					.ToList();
			}
			else
			{
				p.Pagination.LastPage = 1;
				p.Pagination.CurrentPage = 1;
				p.Pagination.NextPageUrl = string.Empty;
				p.Pagination.PrevPageUrl = string.Empty;
				p.Pagination.From = 0;
				p.Pagination.To = 0;
				p.Data = new List<T>();
			}

			return p;
		}

	}
}
