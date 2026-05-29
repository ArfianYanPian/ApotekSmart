using System;

namespace ApotekSmart.Models
{
    public class Kasir : BaseUser
    {
        public string NomorShift { get; set; }

        public override void TampilInfo()
        {
            Console.WriteLine($"[Kasir] Nama: {Nama} | Shift: {NomorShift} | Status Aktif: {IsActive}");
        }

        public void BuatTransaksiBaru()
        {
            // Logika spesifik kasir saat melayani pembeli
            Console.WriteLine($"Kasir {Nama} sedang memproses transaksi baru.");
        }
    }
}