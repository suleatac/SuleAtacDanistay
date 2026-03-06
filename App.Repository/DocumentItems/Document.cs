
using App.Repository.DocumentStatusEnum;
using App.Repository.UserItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.DocumentItems
{
    public class Document
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedDate { get; set; }
        public DocumentStatus Status { get; set; }
    }
}
