using App.Repository.UserItems;

namespace App.API.DTOs
{
    public class DocumentUploadDto
    {
        public int UserId { get; set; }

        public string Title { get; set; }

        public IFormFile File { get; set; }
    }
}
