using AutoMapper;
using EventManagement.Models.Domain;
using EventManagement.Models.DTO;
using EventManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventUserController : ControllerBase
    {
        private readonly IEventUserRepository _eventUserRepository;
        private readonly IMapper mapper;
        private readonly IEventRepository _eventRepository;

        public EventUserController(IMapper mapper, IEventUserRepository eventUserRepository, IEventRepository eventRepository)
        {
            _eventUserRepository = eventUserRepository;
            this.mapper = mapper;
            this._eventRepository = eventRepository;
        }

        [HttpPost]
        public async Task<ActionResult<EventUser>> AddEventUser(AddEventUserDTO addEventUser)
        {
            // Check if event and user exist
            var eventExists = await _eventUserRepository.EventExists(addEventUser.EventId);
            var userExists = await _eventUserRepository.UserExists(addEventUser.UserId);

            if (!eventExists || !userExists)
            {
                return BadRequest("Event or user does not exist");
            }

            // Check if event user already exists
            var eventUserExists = await _eventUserRepository.EventUserExists(addEventUser.EventId, addEventUser.UserId);

            if (eventUserExists)
            {
                return Conflict("Event user already exists");
            }

            var events = await _eventRepository.GetEventByIdAsync(addEventUser.EventId);
            //Check if max participants
            var currentParticipantsCount = await _eventUserRepository.GetEventUserCount(addEventUser.EventId);

            if (currentParticipantsCount >= events.MaxParticipants)
            {
                return BadRequest("Maximum participants reached for this event");
            }

            // Create new event user
            var eventUser = mapper.Map<EventUser>(addEventUser);

            await _eventUserRepository.AddEventUser(eventUser);

            return Ok("Event User Added Successfuly to event");
        }

        [HttpGet]
        [Route("{eventId:guid}")]
        public async Task<IActionResult> GetEventUsers([FromRoute] Guid eventId)
        {
            var eventExists = await _eventUserRepository.EventExists(eventId);
            if(!eventExists)
            {
                return BadRequest("Event does not exist");
            }
            var eventUsers= await _eventUserRepository.GetEventUsers(eventId);
            //var eventUserDTO=mapper.Map<List<EventUserDTO>>(eventUsers);

            return Ok(eventUsers);
        }

    }
}
