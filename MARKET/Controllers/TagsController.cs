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
    public class TagsController : ControllerBase
    {
        private readonly IUnitOfWork context;
        private readonly IMapper mapper;

        public TagsController(IUnitOfWork context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElementResource>>> GetCategories()
        {
            var tags = await context.Tags.GetAll();
            var resources = mapper.Map<IEnumerable<Tag>, IEnumerable<ElementResource>>(tags);
            return resources.ToList();
        }

        // GET api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            Tag tag = await context.Tags.GetById(id);
            if (tag == null)
            {
                return NotFound();
            }
            return tag;
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<ElementResource>> PostTag([FromBody] SaveElementResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var tag = mapper.Map<SaveElementResource, Tag>(resource);
            var result = await context.Tags.Add(tag);
            if (result.Success)
            {
                var newTag = mapper.Map<Tag, ElementResource>(result.Entity);
                return Ok(newTag);
            }
            return BadRequest(result.Message);

        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Tag>> PutTag(int id, [FromBody] Tag resource)
        {
            if (id != resource.Id)
            {
                return BadRequest("The Id in the request URL does not match the Id in the request body");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await context.Tags.Edit(resource);
            if (result.Success)
            {
                var newTag = mapper.Map<Tag, ElementResource>(result.Entity);
                return Ok(newTag);
            }
            return BadRequest(result.Message);
        }

        //DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tag>> DeleteTag(int id)
        {
            var result = await context.Tags.Delete(id);
            if (result.Success)
            {
                var newTag = mapper.Map<Tag, ElementResource>(result.Entity);
                return Ok(newTag);
            }
            return BadRequest(result.Message);
        }
    }
}
