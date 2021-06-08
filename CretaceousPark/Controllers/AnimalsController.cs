using CretaceousPark.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

		// GET api/animals
		[HttpGet]
		public ActionResult<IEnumerable<Animal>> Get(string species, string gender, string name, int minimumAge)
		{
			var query = _db.Animals.AsQueryable();

			if (species != null)
			{
				query = query.Where(entry => entry.Species == species);
			}

			if (gender != null)
			{
				query = query.Where(entry => entry.Gender == gender);
			}

			if (name != null)
			{
				query = query.Where(entry => entry.Name == name);
			}

			if (minimumAge > 0)
			{
				query = query.Where(entry => entry.Age >= minimumAge);
			}

			return query.ToList();
		}

		// Post api/animals
		[HttpPost]
		public void Post([FromBody] Animal animal)
		{
			_db.Animals.Add(animal);
			_db.SaveChanges();
		}

		// GET api/animals/5
		[HttpGet("{id}")]
		public ActionResult<Animal> Get(int id)
		{
			return _db.Animals.FirstOrDefault(entry => entry.AnimalId == id);
		}

		// PUT api/animals/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] Animal animal)
		{
			animal.AnimalId = id;
			_db.Entry(animal).State = EntityState.Modified;
			_db.SaveChanges();
		}

		// DELETE api/animals/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			Animal animalToDelete = _db.Animals.FirstOrDefault(entry => entry.AnimalId == id);
			_db.Animals.Remove(animalToDelete);
			_db.SaveChanges();
		}
	}
}