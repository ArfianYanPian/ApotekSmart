using System;

namespace ApotekSmart.Models
{
    public class TransaksiResep : BaseTransaksi
    {
        // Atribut khusus tabel resep
        public int IdResep { get; set; }
        public int? IdApoteker { get; set; }
        public string NomorResep { get; set; }
        public string NamaDokter { get; set; }
        public string NamaPasien { get; set; }
        public string StatusValidasi { get; set; } // 'menunggu', 'disetujui', 'ditolak'
        public string CatatanApoteker { get; set; }
        public DateTime? ValidatedAt { get; set; }

        // Relasi
        public Apoteker Apoteker { get; set; }

        public TransaksiResep()
        {
            this.JenisTransaksi = "resep";
            this.StatusValidasi = "menunggu";
            this.Status = "menunggu"; // Status transaksi mengikuti status validasi resep
        }

        public override void ProsesTransaksi()
        {
            if (StatusValidasi == "disetujui")
            {
                this.Status = "selesai";
                Console.WriteLine($"Transaksi Resep {NomorResep} berhasil diproses.");
            }
            else if (StatusValidasi == "ditolak")
            {
                this.Status = "ditolak";
                Console.WriteLine($"Transaksi Resep {NomorResep} ditolak karena: {CatatanApoteker}");
            }
            else
            {
                Console.WriteLine($"Transaksi Resep {NomorResep} masih tertunda menunggu divalidasi oleh Apoteker.");
            }
        }

        public override void TampilStruk()
        {
            base.TampilStruk();
            Console.WriteLine($"Nomor Resep    : {NomorResep}");
            Console.WriteLine($"Nama Pasien    : {NamaPasien}");
            Console.WriteLine($"Nama Dokter    : {NamaDokter}");
            Console.WriteLine($"Status Validasi: {StatusValidasi}");
            if (!string.IsNullOrEmpty(CatatanApoteker))
            {
                Console.WriteLine($"Catatan Apoteker: {CatatanApoteker}");
            }
        }
    }
}