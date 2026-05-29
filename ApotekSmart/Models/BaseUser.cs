using System;

namespace ApotekSmart.Models
{
    public abstract class BaseUser
    {
        public int IdUser { get; set; }
        public string Nama { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // 'apoteker' atau 'kasir'
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        // Metode abstrak dan virtual
        public abstract void TampilInfo();

        public virtual bool Login(string username, string password)
        {
            return this.Username == username && this.Password == password && this.IsActive;
        }
    }
}