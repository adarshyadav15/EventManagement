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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IMapper _mapper;

        public FeedbackController(IFeedbackRepository feedbackRepository, IMapper mapper)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<IActionResult> GetFeedbacks()
        {
            var feedbacks = await _feedbackRepository.GetFeedbacksAsync();
            var feedbackDTOs = _mapper.Map<List<FeedbackDTO>>(feedbacks);
            return Ok(feedbackDTOs);
        }

        // GET: api/Feedback/5
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetFeedbackById([FromRoute] Guid id)
        {
            var feedback = await _feedbackRepository.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            var feedbackDTO = _mapper.Map<FeedbackDTO>(feedback);
            return Ok(feedbackDTO);
        }

        // POST: api/Feedback
        [HttpPost]
        public async Task<IActionResult> CreateFeedback(CreateFeedbackDTO newFeedback)
        {
            if (newFeedback == null)
            {
                return BadRequest();
            }
            var feedback = _mapper.Map<Feedback>(newFeedback);
            var feedbackCreated = await _feedbackRepository.CreateFeedbackAsync(feedback);
            var feedbackDTO = _mapper.Map<FeedbackDTO>(feedbackCreated);
            return CreatedAtAction(nameof(GetFeedbackById), new { id = feedbackDTO.FeedbackId }, feedbackDTO);
        }

        // PUT: api/Feedback/5
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateFeedback(Guid id, UpdateFeedbackDTO updateFeedback)
        {
            if (id != updateFeedback.FeedbackId)
            {
                return BadRequest();
            }
            var feedback = await _feedbackRepository.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            feedback.Rating = updateFeedback.Rating;
            feedback.Comment = updateFeedback.Comment;
            var result = await _feedbackRepository.UpdateFeedbackAsync(feedback);
            var feedbackDTO = _mapper.Map<FeedbackDTO>(result);
            return Ok(feedbackDTO);
        }

        // DELETE: api/Feedback/5
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteFeedback([FromRoute] Guid id)
        {
            var result = await _feedbackRepository.DeleteFeedbackAsync(id);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok($"Feedback with id : {id} was deleted successfully");
        }
    }
}