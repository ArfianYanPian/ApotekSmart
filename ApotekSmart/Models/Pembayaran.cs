using System;

namespace ApotekSmart.Models
{
    public class Pembayaran
    {
        private int _idPembayaran;
        private int _idTransaksi;
        private decimal _total;
        private decimal _jumlahBayar;
        private string _metodeBayar;

        public int IdPembayaran
        {
            get { return _idPembayaran; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("IdPembayaran harus lebih dari 0.");
                _idPembayaran = value;
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

        public decimal Total
        {
            get { return _total; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Total tidak boleh negatif.");
                _total = value;
            }
        }

        public decimal JumlahBayar
        {
            get { return _jumlahBayar; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Jumlah bayar tidak boleh negatif.");
                _jumlahBayar = value;
            }
        }

        // Kembalian read-only, dihitung otomatis
        public decimal Kembalian
        {
            get
            {
                if (_jumlahBayar < _total)
                    throw new InvalidOperationException(
                        "Jumlah bayar tidak boleh kurang dari total.");
                return _jumlahBayar - _total;
            }
        }

        public string MetodeBayar
        {
            get { return _metodeBayar; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Metode bayar tidak boleh kosong.");
                string v = value.Trim().ToLower();
                if (v != "cash" && v != "transfer" && v != "kartu")
                    throw new ArgumentException(
                        "Metode bayar harus 'cash', 'transfer', atau 'kartu'.");
                _metodeBayar = v;
            }
        }

        public DateTime CreatedAt { get; set; }

        public Pembayaran()
        {
            _metodeBayar = "cash";
        }

        public override string ToString()
            => $"Pembayaran #{IdPembayaran} | Total: Rp {Total:N0} | " +
               $"Bayar: Rp {JumlahBayar:N0} | Kembali: Rp {Kembalian:N0} | {MetodeBayar}";
    }
}