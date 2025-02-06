namespace ProjectName.ApiService.Models
{
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
