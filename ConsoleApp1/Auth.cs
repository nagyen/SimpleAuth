namespace SimpleAuth
{
    public class Auth
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
