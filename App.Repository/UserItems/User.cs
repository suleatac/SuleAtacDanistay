using App.Repository.DocumentItems;
using App.Repository.NotificationItems;
using System;
using System.Collections.Generic;

using System.Text;

namespace App.Repository.UserItems
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}
