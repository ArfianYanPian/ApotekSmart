using System;

namespace ApotekSmart.Models
{
    public abstract class BaseObat
    {
        private int _stok;
        private decimal _hargaJual;

        public int IdObat { get; set; }
        public int IdKategori { get; set; }
        public string NamaObat { get; set; }
        public string Jenis { get; set; }
        public string Satuan { get; set; }
        public decimal HargaBeli { get; set; }
        public DateTime TanggalExp { get; set; }
        public string Deskripsi { get; set; }
        public int StokMinimum { get; set; }
        public bool IsActive { get; set; }

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

        public abstract bool CekKelayakan();

        public override string ToString()
        {
            return $"{NamaObat} - Stok: {Stok} - Exp: {TanggalExp:dd/MM/yyyy}";
        }
    }
}