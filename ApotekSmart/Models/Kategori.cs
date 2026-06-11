using System;

namespace ApotekSmart.Models
{
    // [CLASS LIBRARY] Bagian dari Models library dalam namespace ApotekSmart.Models
    // [ASSOCIATION] Kategori berelasi dengan ObatBebas dan ObatResep lewat IdKategori
    // Kategori bisa hidup tanpa Obat, dan Obat butuh Kategori tapi tidak memilikinya
    public class Kategori
    {
        // [ENCAPSULATION] Semua field private, hanya bisa diakses lewat property
        private int _idKategori;
        private string _namaKategori;
        private string _deskripsi;

        // [ENCAPSULATION] Property dengan validasi di setter
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

        // [ENCAPSULATION] NamaKategori tidak boleh kosong
        public string NamaKategori
        {
            get { return _namaKategori; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nama kategori tidak boleh kosong.");
                _namaKategori = value.Trim();
            }
        }

        // [ENCAPSULATION] Deskripsi boleh null/kosong — tidak ada validasi ketat
        public string Deskripsi
        {
            get { return _deskripsi; }
            set { _deskripsi = value; }
        }

        // [POLYMORPHISM] override ToString() dari class Object bawaan C#
        // Memungkinkan Kategori tampil sebagai string nama kategori
        // di ComboBox, ListBox, dll secara otomatis
        public override string ToString() => _namaKategori;
    }
}