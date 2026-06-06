using System;

namespace ApotekSmart.Models
{
    public class Kategori
    {
        private int _idKategori;
        private string _namaKategori;
        private string _deskripsi;

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

        public string Deskripsi
        {
            get { return _deskripsi; }
            set { _deskripsi = value; } // boleh null/kosong
        }

        public override string ToString() => _namaKategori;
    }
}