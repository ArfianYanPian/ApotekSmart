using System;

namespace ApotekSmart.Models
{
    // [CLASS LIBRARY] Bagian dari Models library dalam namespace ApotekSmart.Models
    // [AGGREGATION] ItemTransaksi menyimpan referensi ke BaseObat
    // BaseObat bisa hidup tanpa ItemTransaksi — obat tetap ada walau item dihapus
    public class ItemTransaksi
    {
        // [ENCAPSULATION] Semua field private, hanya bisa diakses lewat property
        private int _idDetailTransaksi;
        private int _idTransaksi;
        private BaseObat _obat;
        private int _qty;
        private decimal _hargaSatuan;
        private string _aturanPakai;

        // [ENCAPSULATION] Property dengan validasi di setter
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

        // [AGGREGATION] _obat adalah referensi ke BaseObat
        // ItemTransaksi tidak memiliki/menciptakan BaseObat
        // BaseObat bisa hidup sendiri tanpa ItemTransaksi
        // [POLYMORPHISM] _obat bertipe BaseObat — bisa menampung ObatBebas maupun ObatResep
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
            set { _aturanPakai = value; }
        }

        // [ENCAPSULATION] Subtotal read-only — dihitung otomatis dari _qty dan _hargaSatuan
        // Tidak bisa diset dari luar, mencegah manipulasi nilai subtotal
        public decimal Subtotal
        {
            get { return _qty * _hargaSatuan; }
        }

        // [POLYMORPHISM] override ToString() dari class Object bawaan C#
        // [AGGREGATION] Akses NamaObat lewat referensi _obat
        public override string ToString()
            => $"{_obat?.NamaObat ?? "-"} x{_qty} @ Rp {_hargaSatuan:N0} = Rp {Subtotal:N0}";
    }
}