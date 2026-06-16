using System;

namespace ApotekSmart.Models
{
    // [ABSTRACT CLASS] BaseObat adalah abstract class — tidak bisa diinstansiasi langsung
    // [CLASS LIBRARY] Bagian dari Models library dalam namespace ApotekSmart.Models
    public abstract class BaseObat
    {
        // [ENCAPSULATION] Semua field private, hanya bisa diakses lewat property
        private int _idObat;
        private int _idKategori;
        private string _namaObat;
        private string _jenis;
        private string _satuan;
        private decimal _hargaBeli;
        private decimal _hargaJual;
        private int _stok;
        private int _stokMinimum;
        private DateTime _tanggalExp;
        private string _deskripsi;
        private bool _isActive;

        // [ENCAPSULATION] Property dengan validasi di setter
        public int IdObat
        {
            get { return _idObat; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("IdObat harus lebih dari 0.");
                _idObat = value;
            }
        }

        public int IdKategori
        {
            get { return _idKategori; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("IdKategori harus lebih dari 0.");
                _idKategori = value;
            }
        }

        public string NamaObat
        {
            get { return _namaObat; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nama obat tidak boleh kosong.");
                _namaObat = value.Trim();
            }
        }

        // [ENCAPSULATION] Setter membatasi nilai hanya 'bebas' atau 'resep'
        public string Jenis
        {
            get { return _jenis; }
            set
            {
                if (value != "bebas" && value != "resep")
                    throw new ArgumentException("Jenis harus 'bebas' atau 'resep'.");
                _jenis = value;
            }
        }

        public string Satuan
        {
            get { return _satuan; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Satuan tidak boleh kosong.");
                _satuan = value.Trim();
            }
        }

        // [ENCAPSULATION] Validasi nilai tidak boleh negatif
        public decimal HargaBeli
        {
            get { return _hargaBeli; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Harga beli tidak boleh negatif.");
                _hargaBeli = value;
            }
        }

        public decimal HargaJual
        {
            get { return _hargaJual; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Harga jual tidak boleh negatif.");
                _hargaJual = value;
            }
        }

        public int Stok
        {
            get { return _stok; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Stok tidak boleh negatif.");
                _stok = value;
            }
        }

        public int StokMinimum
        {
            get { return _stokMinimum; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Stok minimum tidak boleh negatif.");
                _stokMinimum = value;
            }
        }

        public DateTime TanggalExp
        {
            get { return _tanggalExp; }
            set { _tanggalExp = value; }
        }

        public string Deskripsi
        {
            get { return _deskripsi; }
            set { _deskripsi = value; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        // [POLYMORPHISM] virtual — subclass boleh override Validate()
        // [ENCAPSULATION] Validasi kombinasi field dilakukan di dalam class, bukan di luar
        public virtual void Validate()
        {
            if (_hargaJual < _hargaBeli)
                throw new InvalidOperationException(
                    "Harga jual tidak boleh lebih kecil dari harga beli.");
            if (_idKategori <= 0)
                throw new InvalidOperationException("Kategori belum dipilih.");
            if (string.IsNullOrWhiteSpace(_namaObat))
                throw new InvalidOperationException("Nama obat tidak boleh kosong.");
        }

        // [ABSTRACT] Method abstract — wajib di-override oleh ObatBebas dan ObatResep
        // [POLYMORPHISM] ObatBebas dan ObatResep punya logika CekKelayakan() berbeda
        public abstract bool CekKelayakan();

        // [POLYMORPHISM] override ToString() dari class Object bawaan C#
        public override string ToString()
            => $"{NamaObat} - Stok: {Stok} - Exp: {TanggalExp:dd/MM/yyyy}";
    }
}