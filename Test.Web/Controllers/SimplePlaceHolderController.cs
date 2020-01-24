using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Test.Web.ViewModels;
using Test.Db.Repositories;
using Test.Db.Model.Placeholder;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test.Web.Controllers
{
    [Route("api/[controller]")]
    public class SimplePlaceHolderController : Controller
    {
        private readonly ISimplePlaceholderRepository SimplePlaceholderRepository;
        private readonly IMapper Mapper;
        public SimplePlaceHolderController(ISimplePlaceholderRepository simplePlaceholderRepository, IMapper mapper)
        {
            SimplePlaceholderRepository = simplePlaceholderRepository;
            Mapper = mapper;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<SimplePlaceHolderViewModel> Get(int skip = 0, int take = 100, string filter = "")
        {
            var query = SimplePlaceholderRepository.GetAll().OrderBy(x => x.SimpleProperty).AsQueryable();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.SimpleProperty.Contains(filter));
            }
            query = query.Skip(skip).Take(take);

            return Mapper.Map<List<SimplePlaceHolderViewModel>>(query);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public SimplePlaceHolderViewModel Get(int id)
        {
            var entity = SimplePlaceholderRepository.GetById(id);
            return Mapper.Map<SimplePlaceHolderViewModel>(entity);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]SimplePlaceHolderViewModel value)
        {
            if (ModelState.IsValid)
            {
                var data = Mapper.Map<SimplePlaceHolderEntity>(value);
                SimplePlaceholderRepository.Add(data);
                SimplePlaceholderRepository.SaveChanges();
                return StatusCode((int)HttpStatusCode.Created, data.Id);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]SimplePlaceHolderViewModel value)
        {
            if (ModelState.IsValid)
            {
                var data = Mapper.Map<SimplePlaceHolderEntity>(value);
                SimplePlaceholderRepository.Update(data);
                SimplePlaceholderRepository.SaveChanges();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entry = SimplePlaceholderRepository.GetById(id);
            if (entry != null)
            {
                SimplePlaceholderRepository.Delete(entry);
                SimplePlaceholderRepository.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
