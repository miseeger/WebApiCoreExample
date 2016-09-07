using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApiFull.Interfaces;
using WebApiFull.Models;
using WebApiFull.Models.Services;

namespace WebApiFull.Controllers
{
	[Route("api/[controller]")]
	public class StoriesController : Controller
	{
		private IDataService _dataService;


		public StoriesController(IDataService dataService)
		{
			_dataService = dataService;
		}


		// GET api/stories
		[HttpGet]
		public IActionResult Get()
		{
			var stories = _dataService.Db.GetCollection<Story>("Story");
			return Json(stories.FindAll());
		}

		// GET api/stories/page/2
		[HttpGet]
		[Route("page/{page}")]
		public IActionResult GetPage(int page)
		{
			return
				Json(_dataService.CreatePageData<Story>(3, "api/stories/page", page));
		}

		// GET api/stories/5
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var stories = _dataService.Db.GetCollection<Story>("Story");
			return Json(stories.FindById(id));
		}

		// POST api/stories
		[HttpPost]
		public IActionResult Post([FromBody]Story newStory)
		{
			var stories = _dataService.Db.GetCollection<Story>("Story");
			newStory.Id = stories.Insert(newStory).AsInt32;
			return Ok(Json(newStory));

		}

		// PUT api/stories/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody]Story updatedStory)
		{
			if (updatedStory != null)
			{
				var stories = _dataService.Db.GetCollection<Story>("Story");
				stories.Update(updatedStory);
			}
			return Ok(Json(updatedStory));
		}

		// DELETE api/stories/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			var stories = _dataService.Db.GetCollection<Story>("Story");
			stories.Delete(id);
		}

	}

}

