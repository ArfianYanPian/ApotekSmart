using System;

namespace ApotekSmart.Models
{
    // [CLASS LIBRARY] Bagian dari Models library dalam namespace ApotekSmart.Models
    // [ASSOCIATION] Pembayaran berelasi dengan Transaksi lewat IdTransaksi
    // Pembayaran tidak memiliki objek Transaksi langsung, hanya menyimpan ID-nya
    public class Pembayaran
    {
        // [ENCAPSULATION] Semua field private, hanya bisa diakses lewat property
        private int _idPembayaran;
        private int _idTransaksi;
        private decimal _total;
        private decimal _jumlahBayar;
        private string _metodeBayar;

        // [ENCAPSULATION] Property dengan validasi di setter
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

        // [ENCAPSULATION] Kembalian read-only — dihitung otomatis dari _jumlahBayar dan _total
        // Tidak bisa diset dari luar, mencegah manipulasi nilai kembalian
        // Validasi bisnis: jumlah bayar tidak boleh kurang dari total
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

        // [ENCAPSULATION] MetodeBayar hanya izinkan 3 nilai: cash, transfer, kartu
        // Normalisasi otomatis ke lowercase di dalam setter
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

        // [ENCAPSULATION] Constructor mengatur nilai default MetodeBayar = "cash"
        // Memastikan objek selalu dalam state valid sejak dibuat
        public Pembayaran()
        {
            _metodeBayar = "cash";
        }

        // [POLYMORPHISM] override ToString() dari class Object bawaan C#
        public override string ToString()
            => $"Pembayaran #{IdPembayaran} | Total: Rp {Total:N0} | " +
               $"Bayar: Rp {JumlahBayar:N0} | Kembali: Rp {Kembalian:N0} | {MetodeBayar}";
    }
}