﻿using AutoMapper;
using Event_manager_API.DTOs.Get;
using Event_manager_API.DTOs.Set;
using Event_manager_API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace Event_manager_API.Controllers
{
    [ApiController]
    [Route("Ticket")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class TicketController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<TicketController> logger;
        private readonly IMapper mapper;
        public TicketController(
                    ApplicationDbContext context,
                    ILogger<TicketController> logger,
                    IMapper mapper
               )
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        //GET ALL--------------------------------------------------------------------------------

        /// <summary>
        /// Get a list of Tickets.
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetTicketDTO>>> GetAll()
        {
            logger.LogInformation("Getting Ticket List");
            var ticket = await dbContext.Ticket.Include(x=>x.User).Include(x => x.Event).Include(x => x.Coupon).ToListAsync();
            return mapper.Map<List<GetTicketDTO>>(ticket);
        }

        //GET BY ID-------------------------------------------------------------------------------

        /// <summary>
        /// Get Ticket by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetTicketDTO>> GetById(int id)
        {
            var ticket = await dbContext.Ticket.Include(x => x.User).Include(x => x.Event).Include(x => x.Coupon).FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<GetTicketDTO>(ticket);
        }


        //POST---------------------------------------------------------------------------------------

        /// <summary>
        /// Add a Ticket.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     To add a new ticket follow this strcture
        ///     {
        ///         "userId": 0,
        ///         "eventId": 0,
        ///         "couponId": 0 -> 0:No Coupon
        ///     }
        ///
        /// USE USER ID, NOT ACCOUNT ID
        /// </remarks>

        
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] TicketDTO ticketDTO)
        {
            //Check Capacity
            var event_ = await dbContext.Event.FirstOrDefaultAsync(x => x.Id == ticketDTO.EventId);
            var countTickets=await dbContext.Ticket.CountAsync();

            if (event_.EventCapacity <= countTickets)
            {
                return BadRequest("That Event is full");
            }

            var userExists = await dbContext.User.AnyAsync(x => x.Id == ticketDTO.UserId);
            if (!userExists)
            {
                return BadRequest("That User does not exist");
            }

            var eventExists = await dbContext.Event.AnyAsync(x => x.Id == ticketDTO.EventId);
            if (!eventExists)
            {
                return BadRequest("That Event does not exist");
            }

            
            //GET TICKET PRICE
            decimal ticketPrice = event_.TicketPrice;


            if (ticketDTO.CouponId != 0)
            {
                var couponExists = await dbContext.Coupon.AnyAsync(x => x.Id == ticketDTO.CouponId);
                if (!couponExists )
                {
                    return BadRequest("That Coupon does not exist");
                }

                //CALCULATE TICKET PRICE IN CASE COUPON EXISTS
                var coupon = await dbContext.Coupon.FirstOrDefaultAsync(x => x.Id == ticketDTO.CouponId);
                ticketPrice = event_.TicketPrice - (event_.TicketPrice * (coupon.DiscountPercentage/100));
            }
            else
            {

                //No Coupon
                var coupon = await dbContext.Coupon.FirstOrDefaultAsync(x => x.Code == "NoCode" && x.EventId==ticketDTO.EventId);
                ticketDTO.CouponId = coupon.Id;
                ticketPrice = event_.TicketPrice;

            }


            var ticket = mapper.Map<Ticket>(ticketDTO);
            ticket.TicketPrice = ticketPrice;
            ticket.CreatedAt= DateTime.Now;
            dbContext.Add(ticket);
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        //UPDATE------------------------------------------------------------------------------------------------

        /// <summary>
        /// Update Ticket.
        /// </summary>
        /// <returns>A newly created Ticket</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     To Update ticket follow this strcture, and specify id
        ///     {
        ///         "userId": 0,
        ///         "eventId": 0,
        ///         "couponId": 0 -> 0:No Coupon
        ///     }
        ///
        /// USE USER ID, NOT ACCOUNT ID
        /// </remarks>

        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutTicket(TicketDTO ticketDTO, [FromRoute] int id)
        {
            
            
            var exists = await dbContext.Ticket.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("Does not exist");
            }

            var userExists = await dbContext.User.AnyAsync(x => x.Id == ticketDTO.UserId);
            if (!userExists)
            {
                return BadRequest("That User does not exist");
            }

            var eventExists = await dbContext.Event.AnyAsync(x => x.Id == ticketDTO.EventId);
            if (!eventExists)
            {
                return BadRequest("That Event does not exist");
            }

            var event_ = await dbContext.Event.FirstOrDefaultAsync(x => x.Id == ticketDTO.EventId);
            //GET TICKET PRICE
            decimal ticketPrice = event_.TicketPrice;


            if (ticketDTO.CouponId != 0)
            {
                var couponExists = await dbContext.Coupon.AnyAsync(x => x.Id == ticketDTO.CouponId);
                if (!couponExists)
                {
                    return BadRequest("That Coupon does not exist");
                }

                //CALCULATE TICKET PRICE IN CASE COUPON EXISTS
                var coupon = await dbContext.Coupon.FirstOrDefaultAsync(x => x.Id == ticketDTO.CouponId);
                ticketPrice = event_.TicketPrice - (event_.TicketPrice * (coupon.DiscountPercentage / 100));
            }
            else
            {
                //No Coupon
                var coupon = await dbContext.Coupon.FirstOrDefaultAsync(x => x.Code == "NoCode" && x.EventId == ticketDTO.EventId);
                ticketDTO.CouponId = coupon.Id;
                ticketPrice = event_.TicketPrice;

            }



            var ticket = mapper.Map<Ticket>(ticketDTO);
            ticket.TicketPrice = ticketPrice;
            ticket.Id = id;
            ticket.CreatedAt = DateTime.Now;
            dbContext.Update(ticket);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        // DELETE-----------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Delete Ticket.
        /// </summary>
        /// 


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Ticket.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Ticket()
            { Id = id, }
            );
            await dbContext.SaveChangesAsync();
            return Ok();
        }



        
    }
}