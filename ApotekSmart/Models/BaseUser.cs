using System;

namespace ApotekSmart.Models
{
    public abstract class BaseUser
    {
        public int IdUser { get; set; }
        public string Nama { get; set; }
        public string Username { get; set; }
        private string _Password;
        public string Role { get; set; } // 'apoteker' atau 'kasir'
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        // Metode abstrak dan virtual
        public abstract void TampilInfo();

        public void SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password tidak boleh kosong.");
            _Password = password;
        }
        public bool CekPassword(string password)
        {
            return _Password == password;
        }
        public string GetPassword()
        {
            return _Password;
        }
        public virtual bool Login(string username, string password)
        {
            return this.Username == username && CekPassword(password) && this.IsActive;
        }
    }
}