using System;

namespace ApotekSmart.Models
{
    public class ItemTransaksi
    {
        private int _idDetailTransaksi;
        private int _idTransaksi;
        private BaseObat _obat;
        private int _qty;
        private decimal _hargaSatuan;
        private string _aturanPakai;

        public int IdDetailTransaksi
        {
            get { return _idDetailTransaksi; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("IdDetailTransaksi harus lebih dari 0.");
                _idDetailTransaksi = value;
            }
        }

        public int IdTransaksi
        {
            get { return _idTransaksi; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("IdTransaksi harus lebih dari 0.");
                _idTransaksi = value;
            }
        }

        public BaseObat Obat
        {
            get { return _obat; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Obat tidak boleh null.");
                _obat = value;
            }
        }

        public int Qty
        {
            get { return _qty; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Qty harus lebih dari 0.");
                _qty = value;
            }
        }

        public decimal HargaSatuan
        {
            get { return _hargaSatuan; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Harga satuan tidak boleh negatif.");
                _hargaSatuan = value;
            }
        }

        public string AturanPakai
        {
            get { return _aturanPakai; }
            set { _aturanPakai = value; } // boleh null/kosong
        }

        // FIX: kurung kurawal diperbaiki
        public decimal Subtotal
        {
            get { return _qty * _hargaSatuan; }
        }

        public override string ToString()
            => $"{_obat?.NamaObat ?? "-"} x{_qty} @ Rp {_hargaSatuan:N0} = Rp {Subtotal:N0}";
    }
}