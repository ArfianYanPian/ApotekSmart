using System;

namespace ApotekSmart.Models
{
    public abstract class BaseUser
    {
        private int _idUser;
        private string _nama;
        private string _username;
        private string _password;
        private string _role;
        private DateTime _createdAt;
        private bool _isActive;

        public int IdUser
        {
            get { return _idUser; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("IdUser harus lebih dari 0.");
                _idUser = value;
            }
        }

        public string Nama
        {
            get { return _nama; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nama tidak boleh kosong.");
                _nama = value.Trim();
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Username tidak boleh kosong.");
                if (value.Trim().Length < 3)
                    throw new ArgumentException("Username minimal 3 karakter.");
                _username = value.Trim();
            }
        }

        public string Role
        {
            get { return _role; }
            set
            {
                if (value != "apoteker" && value != "kasir")
                    throw new ArgumentException("Role harus 'apoteker' atau 'kasir'.");
                _role = value;
            }
        }

        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("CreatedAt tidak boleh di masa depan.");
                _createdAt = value;
            }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public abstract void TampilInfo();

        public void SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password tidak boleh kosong.");
            _password = password;
        }

        public bool CekPassword(string password) => _password == password;
        public string GetPassword() => _password;

        public virtual bool Login(string username, string password)
            => Username == username && CekPassword(password) && IsActive;
    }
}