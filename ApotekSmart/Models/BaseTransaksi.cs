using System;
using System.Collections.Generic;

namespace ApotekSmart.Models
{
    public abstract class BaseTransaksi
    {
        public int IdTransaksi { get; set; }
        public int IdKasir { get; set; }
        public string JenisTransaksi { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // FIX: protected set agar subclass/controller bisa isi nilainya
        public decimal Total { get; protected set; }

        public List<ItemTransaksi> Items { get; set; } = new List<ItemTransaksi>();

        // Navigational Property
        public Kasir Kasir { get; set; }

        // Method untuk subclass/controller set total dari Items
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