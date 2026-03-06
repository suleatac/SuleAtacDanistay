using System;
using System.Collections.Generic;
using System.Text;

namespace App.Repository
{
    public class ConnectionToStringOption
    {
        public const string Key = "ConnectionStrings";
        public string SqlServer { get; set; } = default!;
    }
}
