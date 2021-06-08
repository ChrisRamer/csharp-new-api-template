using CretaceousPark.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CretaceousPark.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AnimalsController : ControllerBase
	{
		private CretaceousParkContext _db;

		public AnimalsController(CretaceousParkContext db)
		{
			_db = db;
		}

		// Get api/animals
		[HttpGet]
		public ActionResult<IEnumerable<Animal>> Get()
		{
			return _db.Animals.ToList();
		}

		// Post api/animals
		[HttpPost]
		public void Post([FromBody] Animal animal)
		{
			_db.Animals.Add(animal);
			_db.SaveChanges();
		}

		// Get api/animals/5
		[HttpGet("{id}")]
		public ActionResult<Animal> Get(int id)
		{
			return _db.Animals.FirstOrDefault(entry => entry.AnimalId == id);
		}
	}
}