using App.Repository.DocumentStatusEnum;
using App.Repository.UserItems;

namespace App.API.DTOs
{
    public class GetDocumentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedDate { get; set; }
        public DocumentStatus Status { get; set; }
    }
}
