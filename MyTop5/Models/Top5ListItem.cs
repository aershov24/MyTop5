using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTop5.Models
{
    public class Top5ListItem
    {
        public int Top5ListItemId { get; set; }
        public int Top5ListId { get; set; }
        public string Text { get; set; }

        //Navigation Property
        public virtual Top5List Top5List { get; set; }
    }
}