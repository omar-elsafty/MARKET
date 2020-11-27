using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MARKET.Data.Repository;
using MARKET.Data.UnitOfWork;
using MARKET.Extentions;
using MARKET.Models.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static MARKET.Mapping.ResourseToModelProfile;

namespace MARKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController<T> : ControllerBase
        where T :class
    {
        private readonly IRepository<T> context;
        private readonly IMapper mapper;

        public ApiController(IRepository<T> context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            var elements = await context.GetAll();
            //var resources = mapper.Map<IEnumerable<T>, IEnumerable<ElementResource>>(elements);             
            //return resources.ToList();
            return elements;
        }

        // GET api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<T>> Get(int id)
        {
            var element = await context.GetById(id);

            if (element == null)
            {
                return NotFound();
            }
            return element;
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<ElementResource>> Post([FromBody] SaveElementResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
          
            var element = mapper.Map<Source<SaveElementResource>, Destination<T>>
                (new Source<SaveElementResource>());
          
            var result = await context.Add(element.Value);
            if (result.Success)
            {
                var newElement= mapper.Map<T, ElementResource>(result.Entity);
                return Ok(newElement);
            }
            return BadRequest(result.Message);

        }

        //// PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<ActionResult<T>> Put(int id, [FromBody] T resource)
        {
            //if (id != resource.Id)
            //{
            //    return BadRequest("The Id in the request URL does not match the Id in the request body");
            //}

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await context.Edit(resource);
            if (result.Success)
            {
                var newCategory = mapper.Map<T, ElementResource>(result.Entity);
                return Ok(newCategory);
            }
            return BadRequest(result.Message);
        }

        //DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<T>> Delete(int id)
        {
            var result = await context.Delete(id);
            if (result.Success)
            {
                var newCategory = mapper.Map<T, ElementResource>(result.Entity);
                return Ok(newCategory);
            }
            return BadRequest(result.Message);
        }
    }
}

