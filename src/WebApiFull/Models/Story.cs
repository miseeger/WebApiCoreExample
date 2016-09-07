namespace WebApiFull.Models
{

	public class Story
	{
		public int Id { get; set; }
		public string Plot { get; set; }
		public int Upvotes { get; set; }
		public string Writer { get; set; }

		public Story()
		{
			
		}
	}

}