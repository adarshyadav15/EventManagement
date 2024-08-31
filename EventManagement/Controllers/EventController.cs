using AutoMapper;
using EventManagement.Models.Domain;
using EventManagement.Models.DTO;
using EventManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper mapper;
        private readonly IEventUserRepository _eventUserRepository;

        public EventController(IMapper mapper, IEventRepository eventRepository, IEventUserRepository eventUserRepository)
        {
            _eventRepository = eventRepository;
            this.mapper=mapper;
            this._eventUserRepository=eventUserRepository;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events= await _eventRepository.GetEventsAsync();
            var eventDTO = mapper.Map<List<EventDTO>>(events);
            return Ok(eventDTO);
        }

        // GET: api/Events/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetEventByID([FromRoute]Guid id)
        {
            var events = await _eventRepository.GetEventByIdAsync(id);

            if (events == null)
            {
                return NotFound();
            }
            var eventDTO = mapper.Map<EventDTO>(events);

            return Ok(eventDTO);
        }

        // POST: api/Events
        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventDTO newEvent)
        {
            if (newEvent == null)
            {
                return BadRequest();
            }
            var events = mapper.Map<Event>(newEvent);
            var eventCreated= await _eventRepository.CreateEventAsync(events);
            var eventDTO= mapper.Map<EventDTO>(eventCreated);

            // Add admin to the event
            AddEventUserDTO addEventUserDTO = new AddEventUserDTO();
            addEventUserDTO.UserId = eventCreated.CreatedBy;
            addEventUserDTO.EventId = eventCreated.EventId;
            addEventUserDTO.IsAdmin = true;
            var addEventUser=mapper.Map<EventUser>(addEventUserDTO);
            await _eventUserRepository.AddEventUser(addEventUser);

            return CreatedAtAction(nameof(GetEventByID), new { id = eventDTO.EventId }, eventDTO);
        }

        // PUT: api/Events/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEvent(Guid id, UpdateEventDTO updateEvent)
        {
            if (id != updateEvent.EventId)
            {
                return BadRequest();
            }
            var events = await _eventRepository.GetEventByIdAsync(id);
            if (events == null)
            {
                return NotFound();
            }
            events.Name = updateEvent.Name;
            events.Description = updateEvent.Description;
            events.Location = updateEvent.Location;
            events.MaxDate = updateEvent.MaxDate;
            events.MaxParticipants = updateEvent.MaxParticipants;

            var result = await _eventRepository.UpdateEventAsync(events);
            var eventDTO = mapper.Map<EventDTO>(result);
            return Ok(eventDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid id)
        {


            var result = await _eventRepository.DeleteEventAsync(id);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok($"Event with id : {id} was deleted successfully");
        }
        [HttpGet]
        [Route("createdby/{userId:guid}")]
        public async Task<IActionResult> GetEventsCreatedByUser([FromRoute] Guid userId)
        {
            var events = await _eventRepository.GetEventsCreatedByUserAsync(userId);
            var eventDTO = mapper.Map<List<EventDTO>>(events);
            return Ok(eventDTO);
        }
       


    }
}