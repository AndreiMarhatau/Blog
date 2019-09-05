using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class PostWithComments
    {
        public string this [string key]
        {
            get
            {
                return Post[key];
            }
        }
        public IDictionary<string, string> Post { get; protected set; }
        public List<CommentInPost> Comments { get; set; } = new List<CommentInPost>();

        public PostWithComments(IDictionary<string,string> post, List<CommentInPost> comment)
        {
            Post = post;
            Comments = comment;
        }
    }
    public class CommentInPost
    {
        private IDictionary<string, string> _comment;

        public string this[string key]
        {
            get
            {
                return _comment[key];
            }
            set
            {
                _comment[key] = value;
            }
        }

        public CommentInPost(IDictionary<string,string> comment)
        {
            _comment = comment;
        }
    }
}
