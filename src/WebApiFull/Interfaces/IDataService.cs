using System.Collections.Generic;
using LiteDB;
using WebApiFull.Models.Services;

namespace WebApiFull.Interfaces
{

	public interface IDataService
	{
		LiteDatabase Db { get; set; }

		DataPage<T> CreatePageData<T>(List<T> data, int perPage, string pageUrl, int page) where T : new();
		DataPage<T> CreatePageData<T>(int perPage, string pageUrl, int page) where T : new();
	}

}