using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public ApplicationUser Author { get; set; }

        public int PostId { get; set; }


    }
}