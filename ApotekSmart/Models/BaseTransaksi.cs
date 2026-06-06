using System;
using System.Collections.Generic;

namespace ApotekSmart.Models
{
    public abstract class BaseTransaksi
    {
        private int _idTransaksi;
        private int _idKasir;
        private string _jenisTransaksi;
        private string _status;
        private DateTime _createdAt;

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

        public int IdKasir
        {
            get { return _idKasir; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("IdKasir harus lebih dari 0.");
                _idKasir = value;
            }
        }

        public string JenisTransaksi
        {
            get { return _jenisTransaksi; }
            set
            {
                if (value != "biasa" && value != "resep")
                    throw new ArgumentException("JenisTransaksi harus 'biasa' atau 'resep'.");
                _jenisTransaksi = value;
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                if (value != "selesai" && value != "menunggu" && value != "ditolak")
                    throw new ArgumentException(
                        "Status harus 'selesai', 'menunggu', atau 'ditolak'.");
                _status = value;
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

        public decimal Total { get; protected set; }

        public List<ItemTransaksi> Items { get; set; } = new List<ItemTransaksi>();
        public Kasir Kasir { get; set; }

        public void HitungTotal()
        {
            Total = 0;
            foreach (var item in Items)
                Total += item.Subtotal;
        }

        public abstract void ProsesTransaksi();

        public virtual void CetakStruk()
        {
            Console.WriteLine("=====================================");
            Console.WriteLine($"ID Transaksi : {IdTransaksi}");
            Console.WriteLine($"Jenis        : {JenisTransaksi}");
            Console.WriteLine($"Status       : {Status}");
            Console.WriteLine($"Total        : Rp {Total:N0}");
            Console.WriteLine("=====================================");
        }
    }
}