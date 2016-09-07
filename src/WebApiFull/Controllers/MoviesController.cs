using Microsoft.AspNetCore.Mvc;
using WebApiFull.Interfaces;
using WebApiFull.Models;

namespace WebApiFull.Controllers
{
	[Route("api/[controller]")]
	public class MoviesController : Controller
	{
		private IDataService _dataService;


		public MoviesController(IDataService dataService)
		{
			_dataService = dataService;
		}


		// GET api/movies
		[HttpGet]
		public IActionResult Get()
		{
			var movies = _dataService.Db.GetCollection<Movie>("Movie");
			return Json(movies.FindAll());
		}

		// GET api/movies/page/2
		[HttpGet]
		[Route("page/{page}")]
		public IActionResult GetPage(int page)
		{
			return
				Json(_dataService.CreatePageData<Movie>(3, "api/movies/page", page));
		}

		// GET api/movies/5
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var movies = _dataService.Db.GetCollection<Movie>("Movie");
			return Json(movies.FindById(id));
		}

		// POST api/movies
		[HttpPost]
		public IActionResult Post([FromBody]Movie newMovie)
		{
			var movies = _dataService.Db.GetCollection<Movie>("Movie");
			newMovie.Id = movies.Insert(newMovie).AsInt32;
			return Ok(Json(newMovie));

		}

		// PUT api/movies/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody]Movie updatedMovie)
		{
			if (updatedMovie != null)
			{
				var movies = _dataService.Db.GetCollection<Movie>("Movie");
				movies.Update(updatedMovie);
			}
			return Ok(Json(updatedMovie));
		}

		// DELETE api/movies/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			var movies = _dataService.Db.GetCollection<Movie>("Movie");
			movies.Delete(id);
		}

	}

}

