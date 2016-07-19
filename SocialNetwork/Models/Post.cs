using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class Post
    {
        public int Id { get; set; }

        [DisplayName("Тип поста")]
        public int PostTypeId { get; set; }
        public PostType PostType { get; set; }


        [DisplayName("Автор")]
        public ApplicationUser Author { get; set; }

        [DisplayName("Текст")]
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(500,ErrorMessage="Длина текста не должна быть больше 500 символов")]
        public string Content { get; set; }

        [DisplayName("Дата создания")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [DisplayName("Понравилось")]
        public int Likes { get; set; }

        [DisplayName("Комментарии")]
        public ICollection<Comment> Comments { get; set; }


        public Post()
        {
            DateCreated = DateTime.Now;
        }
    }
    
}