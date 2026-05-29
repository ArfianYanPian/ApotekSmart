using System;

namespace ApotekSmart.Models
{
    public class Apoteker : BaseUser
    {
        public string NomorIdentitas { get; set; } // SIPA/SIA

        public override void TampilInfo()
        {
            Console.WriteLine($"[Apoteker] Nama: {Nama} | SIPA: {NomorIdentitas} | Status Aktif: {IsActive}");
        }

        public void ValidasiResep(TransaksiResep transaksi, string statusValidasi, string catatan)
        {
            transaksi.StatusValidasi = statusValidasi;
            transaksi.CatatanApoteker = catatan;
            transaksi.ValidatedAt = DateTime.Now;
            transaksi.IdApoteker = this.IdUser;

            Console.WriteLine($"Apoteker {Nama} memberikan status '{statusValidasi}' pada resep {transaksi.NomorResep}.");
        }
    }
}