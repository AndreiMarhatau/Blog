namespace Domain.Core
{
    public class Token
    {
        public int UserId { get; set; }
        public string StrToken { get; set; }

        public bool IsValidData()
        {
            if (StrToken == null)
            {
                return false;
            }
            return true;
        }
    }
}
