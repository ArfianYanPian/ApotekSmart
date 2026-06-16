using System;

namespace ApotekSmart.Models
{
    // [INHERITANCE] TransaksiBiasa mewarisi semua property dan method dari BaseTransaksi
    public class TransaksiBiasa : BaseTransaksi
    {
        // [ENCAPSULATION] Constructor mengatur nilai default
        public TransaksiBiasa()
        {
            JenisTransaksi = "biasa";
            Status = "menunggu";
        }

        // [POLYMORPHISM] override ProsesTransaksi() dari BaseTransaksi
        // Implementasi khusus TransaksiBiasa — langsung selesai tanpa validasi apoteker
        public override void ProsesTransaksi()
        {
            HitungTotal();
            Status = "selesai";
            Console.WriteLine($"Transaksi Biasa #{IdTransaksi} berhasil diproses. Total: Rp {Total:N0}");
        }

        // [POLYMORPHISM] override CetakStruk() dari BaseTransaksi
        public override void CetakStruk()
        {
            base.CetakStruk();
            Console.WriteLine($"Kasir: {Kasir?.Nama ?? "-"}");
            foreach (var item in Items)
                Console.WriteLine($"  {item}");
        }
    }
}