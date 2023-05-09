﻿using AutoMapper;
using Event_manager_API.DTOs.Get;
using Event_manager_API.DTOs.Set;
using Event_manager_API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Event_manager_API.Controllers
{
    [ApiController]
    [Route("Form")]
    public class FormController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<FormController> logger;
        private readonly IMapper mapper;
        public FormController(
                    ApplicationDbContext context,
                    ILogger<FormController> logger,
                    IMapper mapper
               )
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        //GET ALL--------------------------------------------------------------------------------

        /// <summary>
        /// Get a list of Forms.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetFormDTO>>> GetAll()
        {
            logger.LogInformation("Getting Form List");
            var form = await dbContext.Form.ToListAsync();
            return mapper.Map<List<GetFormDTO>>(form);
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Form by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetFormDTO>> GetById(int id)
        {
            var form = await dbContext.Form.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetFormDTO>(form);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a Form.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new form follow this strcture
        ///     {
        ///         "createdAt": "2023-05-07T02:57:19.824Z",
        ///         "formname": "string",
        ///         "email": "form@example.com",
        ///         "password": "string",
        ///         "role": "string"
        ///     }
        ///
        /// </remarks>

        [HttpPost]

        public async Task<ActionResult> Post([FromBody] FormDTO formDTO)
        {
            var form = mapper.Map<Form>(formDTO);
            dbContext.Add(form);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update a Form.
        /// </summary>
        /// <returns>A newly created Form</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update a form follow this strcture, and specify id
        ///     {
        ///         "createdAt": "2023-05-07T02:57:19.824Z",
        ///         "formname": "string",
        ///         "email": "form@example.com",
        ///         "password": "string",
        ///         "role": "form or admin"
        ///     }
        ///
        /// </remarks>

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutForm(FormDTO formDTO, [FromRoute] int id)
        {
            var exists = await dbContext.Form.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("Does not exist");
            }

            /*
            var relationshipExists = await dbContext.Relationship.AnyAsync(x => x.Id == Table.RelationshipId);
            if (!relationshipExists)
            {
                return BadRequest("Does relationship does not exist");
            }*/
            var form = mapper.Map<Form>(formDTO);
            form.Id = id;
            dbContext.Update(form);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Form by Id.
        /// </summary>
        /// 


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Form.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Form()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
