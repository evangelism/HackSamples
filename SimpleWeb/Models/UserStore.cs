using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWeb.Models
{
    public static class UserStore
    {
        public static List<string> Users { get; set; } = new List<string>();
    }
}