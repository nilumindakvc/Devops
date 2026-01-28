using Microsoft.AspNetCore.Mvc;

namespace agent.DTOs
{
    public class UploadModel
    {
        [FromForm(Name = "logo")]
        public IFormFile Logo { get; set; }

        [FromForm(Name = "doc1")]
        public IFormFile Doc1 { get; set; }

        [FromForm(Name = "doc2")]
        public IFormFile Doc2 { get; set; }
    }
}