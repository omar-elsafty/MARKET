using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MARKET.Data.UnitOfWork;
using MARKET.Extentions;
using MARKET.Models.Entities;
using MARKET.Models.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MARKET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;

        public CategoriesController(IUnitOfWork context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElementResource>>> GetCategories()
        {
            var categories = await context.Categories.GetAll();
            var resources = mapper.Map<IEnumerable<Category>, IEnumerable<ElementResource>>(categories);
            return resources.ToList();
        }

        // GET api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            Category category = await context.Categories.GetById(id);

            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<ElementResource>> PostCategory([FromBody] SaveElementResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = mapper.Map<SaveElementResource, Category>(resource);
            var result = await context.Categories.Add(category);
            if (result.Success)
            {
                var newCategory = mapper.Map<Category, ElementResource>(result.Entity);
                return Ok(newCategory);
            }
            return BadRequest(result.Message);

        }

        //// PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> PutCategory(int id, [FromBody] Category resource)
        {
            if (id != resource.Id)
            {
                return BadRequest("The Id in the request URL does not match the Id in the request body");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await context.Categories.Edit(resource);
            if (result.Success)
            {
                var newCategory = mapper.Map<Category, ElementResource>(result.Entity);
                return Ok(newCategory);
            }
            return BadRequest(result.Message);
        }

        //DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var result = await context.Categories.Delete(id);
            if (result.Success)
            {
                var newCategory = mapper.Map<Category, ElementResource>(result.Entity);
                return Ok(newCategory);
            }
            return BadRequest(result.Message);
        }
    }
}
