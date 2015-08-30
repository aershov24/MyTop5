using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTop5.Models
{
    public class Tag
    {

        public int TagId { get; set; }
        public string TagText { get; set; }

        public int Top5ListId { get; set; }
        public virtual Top5List Top5List { get; set; }
    }

    class TagComparer : IEqualityComparer<Tag>
    {
        #region IEqualityComparer<Contact> Members

        public bool Equals(Tag x, Tag y)
        {
            return x.TagText.Equals(y.TagText);
        }

        public int GetHashCode(Tag obj)
        {
            return obj.TagText.GetHashCode();
        }

        #endregion
    }
}