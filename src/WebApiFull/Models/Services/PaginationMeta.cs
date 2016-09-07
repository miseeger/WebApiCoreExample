namespace WebApiFull.Models.Services
{
	public class PaginationMeta
	{
		public int Total { get; set; }
		public int PerPage { get; set; }
		public int CurrentPage { get; set; }
		public int LastPage { get; set; }
		public string NextPageUrl { get; set; }
		public string PrevPageUrl { get; set; }
		public int From { get; set; }
		public int To { get; set; }
	}
}