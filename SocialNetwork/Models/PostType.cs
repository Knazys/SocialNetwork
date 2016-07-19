using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class PostType
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}