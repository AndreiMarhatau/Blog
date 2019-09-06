using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public List<CommentInPost> Comments { get; set; } = new List<CommentInPost>();

        public PostViewModel(int Id, string UserName, string UserSurname, int UserId, string Text, DateTime Date, List<CommentInPost> comment)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.UserSurname = UserSurname;
            this.UserId = UserId;
            this.Text = Text;
            this.Date = Date;

            Comments = comment;
        }
    }
    public class CommentInPost
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int AuthorId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int CommentId { get; set; }

        public CommentInPost(int Id, string AuthorName, string AuthorSurname, int PostId, int UserId, int AuthorId, string Text, DateTime Date, int CommentId)
        {
            this.Id = Id;
            this.AuthorName = AuthorName;
            this.AuthorSurname = AuthorSurname;
            this.PostId = PostId;
            this.UserId = UserId;
            this.AuthorId = AuthorId;
            this.Text = Text;
            this.Date = Date;
            this.CommentId = CommentId;
        }
    }
}
