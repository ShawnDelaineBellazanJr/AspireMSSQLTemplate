using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectName.ApiService.Controllers
{
    /// <summary>
    /// API Controller for managing AI-generated prompts.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PromptsController : ControllerBase
    {
        private static readonly List<PromptModel> _prompts = new List<PromptModel>();

        /// <summary>
        /// Gets all saved prompts.
        /// </summary>
        /// <returns>A list of all prompts.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PromptModel>), 200)]
        public IActionResult GetAllPrompts()
        {
            return Ok(_prompts);
        }

        /// <summary>
        /// Gets a specific prompt by ID.
        /// </summary>
        /// <param name="id">The ID of the prompt.</param>
        /// <returns>The requested prompt, if found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PromptModel), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetPromptById(int id)
        {
            var prompt = _prompts.FirstOrDefault(p => p.Id == id);
            if (prompt == null)
                return NotFound(new { Message = "Prompt not found" });

            return Ok(prompt);
        }

        /// <summary>
        /// Creates a new prompt.
        /// </summary>
        /// <param name="prompt">The prompt to create.</param>
        /// <returns>The created prompt with assigned ID.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PromptModel), 201)]
        public IActionResult CreatePrompt([FromBody] PromptModel prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt.Text))
                return BadRequest(new { Message = "Prompt text cannot be empty." });

            prompt.Id = _prompts.Count + 1;
            _prompts.Add(prompt);
            return CreatedAtAction(nameof(GetPromptById), new { id = prompt.Id }, prompt);
        }

        /// <summary>
        /// Updates an existing prompt.
        /// </summary>
        /// <param name="id">The ID of the prompt to update.</param>
        /// <param name="updatedPrompt">The updated prompt details.</param>
        /// <returns>204 No Content if successful.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePrompt(int id, [FromBody] PromptModel updatedPrompt)
        {
            var existingPrompt = _prompts.FirstOrDefault(p => p.Id == id);
            if (existingPrompt == null)
                return NotFound(new { Message = "Prompt not found" });

            existingPrompt.Text = updatedPrompt.Text;
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific prompt.
        /// </summary>
        /// <param name="id">The ID of the prompt to delete.</param>
        /// <returns>204 No Content if successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePrompt(int id)
        {
            var prompt = _prompts.FirstOrDefault(p => p.Id == id);
            if (prompt == null)
                return NotFound(new { Message = "Prompt not found" });

            _prompts.Remove(prompt);
            return NoContent();
        }
    }

    /// <summary>
    /// Represents an AI-generated prompt.
    /// </summary>
    public class PromptModel
    {
        /// <summary>
        /// The unique identifier for the prompt.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The text of the AI-generated prompt.
        /// </summary>
        public string Text { get; set; }
    }
}
