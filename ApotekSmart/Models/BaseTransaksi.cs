using System;

namespace ApotekSmart.Models
{
    public abstract class BaseTransaksi
    {
        public int IdTransaksi { get; set; }
        public int IdKasir { get; set; }
        public string JenisTransaksi { get; set; } // 'biasa' atau 'resep'
        public string Status { get; set; } // 'selesai', 'menunggu', 'ditolak'
        public decimal Total { get; private set; }
        public DateTime CreatedAt { get; set; }

        // Navigational Property (Relasi)
        public Kasir Kasir { get; set; }

        public abstract void ProsesTransaksi();

        public virtual void CetakStruk()
        {
            Console.WriteLine("=====================================");
            Console.WriteLine($"ID Transaksi : {IdTransaksi}");
            Console.WriteLine($"Jenis        : {JenisTransaksi}");
            Console.WriteLine($"Status       : {Status}");
            Console.WriteLine($"Total        : Rp {Total}");
            Console.WriteLine("=====================================");
        }
    }
}