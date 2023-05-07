using Event_manager_API.Entities;
using Event_manager_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event_manager_API.Controllers
{
    [ApiController]
    [Route("Form")]
    public class FormController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<FormController> logger;
        private readonly IWebHostEnvironment env;
        
        public FormController(
                    ApplicationDbContext context,
                    IService service,
                    ServiceTransient serviceTransient,
                    ServiceScoped serviceScoped,
                    ServiceSingleton serviceSingleton,
                    ILogger<FormController> logger,
                    IWebHostEnvironment env
               )
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
            this.env = env;
        }

        //GET ALL--------------------------------------------------------------------------------

        /// <summary>
        /// Get a list of Forms.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Form>>> GetAll()
        {
            logger.LogInformation("Getting Form List");
            return await dbContext.Form.ToListAsync();
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Form by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Form>> GetById(int id)
        {
            return await dbContext.Form.FirstOrDefaultAsync(x => x.Id == id);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a Form.
        /// </summary>
        /// <param name="form"></param>
        /// <returns>A newly created Form</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new form follow this strcture
        ///     {
        ///        "createdAt": "2023-05-06T18:01:53.212Z",
        ///        "name": "Arena Monterrey",
        ///        "address": "Av. Francisco I. Madero 2500, Centro, 64010 Monterrey, N.L.",
        ///        "capacity": "0"
        ///     }
        ///
        /// </remarks>

        [HttpPost]

        public async Task<ActionResult> Post(Form form)
        {
            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == celular.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("Does not exist");
            }
            */
            dbContext.Add(form);

            await dbContext.SaveChangesAsync();

            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update a Form.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="id"></param>
        /// <returns>A newly created Form</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update a form follow this strcture, and specify id
        ///     {
        ///        "Id": "1",
        ///        "createdAt": "2023-05-06T18:01:53.212Z",
        ///        "name": "Arena Monterrey",
        ///        "address": "Av. Francisco I. Madero 2500, Centro, 64010 Monterrey, N.L.",
        ///        "capacity": "0"
        ///     }
        ///
        /// </remarks>

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Form form, int id)
        {
            if (form.Id != id)
            {
                return BadRequest("The Id does not match the one established in the URL.");
            }

            /*var existeMarca = await dbContext.Marca.AnyAsync(x => x.Id == event_.MarcaID);
            if (!existeMarca)
            {
                return BadRequest("Does not exist");
            }*/

            dbContext.Update(form);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Form by Id.
        /// </summary>

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Form.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("Not found in the database");
            }
            dbContext.Remove(new Form()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
