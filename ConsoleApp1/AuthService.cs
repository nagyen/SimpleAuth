using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SimpleAuth
{
    public class AuthService : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase();
        }

        public DbSet<Auth> Auths { get; set; }

        public void Register(Auth auth)
        {
            Auths.Add(auth);
            SaveChanges();
        }

        public bool Login(Auth auth)
        {
            // check if valid auth details
            if (string.IsNullOrEmpty(auth.Username) || string.IsNullOrEmpty(auth.Password)) return false;

            // get user
            var user = Auths.FirstOrDefault(x => string.Equals(x.Username, auth.Username, StringComparison.CurrentCultureIgnoreCase));
            
            // validate
            if (user == null) return false;
            if (user.Password != auth.Password) return false;

            // login user
            user.IsAuthenticated = true;
            Auths.Update(user);
            SaveChanges();
            return true;
        }

        public void Logout(string username)
        {
            // check if valid auth details
            if (string.IsNullOrEmpty(username)) return;

            // get user
            var user = Auths.FirstOrDefault(x => string.Equals(x.Username, username, StringComparison.CurrentCultureIgnoreCase));

            // validate
            if (user == null) return;

            // logout user
            user.IsAuthenticated = false;
            Auths.Update(user);
            SaveChanges();
        }
    }
}
