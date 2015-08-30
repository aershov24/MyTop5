using MyTop5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyTop5.Models
{
    public class Top5List
    {
        public int Top5ListId { get; set; }
        //public int UserId { get; set; }
        public string Title { get; set; }

        //Navigation Property
        //public virtual User User { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Top5ListItem> Items { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}