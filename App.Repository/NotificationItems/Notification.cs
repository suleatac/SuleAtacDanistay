using App.Repository.UserItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository.NotificationItems
{
    public class Notification
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public string Message { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
