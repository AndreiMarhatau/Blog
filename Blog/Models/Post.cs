using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public string GetHtml(int userId)
        {
            return
                $"<br><br><div class=\"post_{Id}\">{Date}<br>" +
                Text +
                "<br>" +
                "<form> " +
                $"<input type=\"hidden\" name=\"PostId\" value={Id} />" +
                $"<input type=\"hidden\" name=\"UserId\" value={userId} />" +
                "<textarea name=\"Text\"></textarea>" +
                $"<input type=\"hidden\" name=\"CommentId\" value=-1 />" +
                "<input type=\"submit\" value=\"Добавить комментарий\" " +
                "formmethod=\"post\" formaction=\"/Profile/AddComment\" />" +
                "</form></div>";
        }
    }
}
