using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int AuthorId { get; set; }
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public string GetHtml(int userId)
        {
            return 
                $"<div class=\"comment_{Id}\">{Date}<br>" +
                Text +
                "<form>" +
                $"<input type=\"hidden\" name=\"PostId\" value={PostId} />" +
                $"<input type=\"hidden\" name=\"UserId\" value={userId} />" +
                "<textarea name=\"Text\"></textarea>" +
                $"<input type=\"hidden\" name=\"CommentId\" value={Id} />" +
                "<input type=\"submit\" value=\"Ответить\" " +
                "formmethod=\"post\" formaction=\"/Profile/AddComment\" />" +
                "</form></div>";
        }
    }
}
