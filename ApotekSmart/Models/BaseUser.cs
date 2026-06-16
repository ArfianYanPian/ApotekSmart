using System;

namespace ApotekSmart.Models
{
    // [ABSTRACT CLASS] BaseUser adalah abstract class — tidak bisa diinstansiasi langsung
    // [CLASS LIBRARY] Bagian dari Models library dalam namespace ApotekSmart.Models
    public abstract class BaseUser
    {
        // [ENCAPSULATION] Semua field private, hanya bisa diakses lewat property
        private int _idUser;
        private string _nama;
        private string _username;
        private string _password;  // [ENCAPSULATION] _password sepenuhnya tersembunyi, tidak ada getter property langsung
        private string _role;
        private DateTime _createdAt;
        private bool _isActive;

        // [ENCAPSULATION] Property dengan validasi di setter — data tidak bisa diisi sembarangan
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

        // [ABSTRACT] Method abstract — wajib di-override oleh setiap subclass
        // [POLYMORPHISM] Setiap subclass (Apoteker, Kasir) punya implementasi TampilInfo() sendiri
        public abstract void TampilInfo();

        // [ENCAPSULATION] SetPassword & GetPassword mengontrol akses ke _password
        // _password tidak punya public property getter — hanya bisa diset dan dicek lewat method ini
        public void SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password tidak boleh kosong.");
            _password = password;
        }

        public bool CekPassword(string password) => _password == password;
        public string GetPassword() => _password;

        // [POLYMORPHISM] virtual — subclass boleh override method Login ini
        public virtual bool Login(string username, string password)
            => Username == username && CekPassword(password) && IsActive;
    }
}