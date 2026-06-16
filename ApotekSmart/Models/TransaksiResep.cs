using System;

namespace ApotekSmart.Models
{
    // [INHERITANCE] TransaksiResep mewarisi semua property dan method dari BaseTransaksi
    // [CLASS LIBRARY] Bagian dari Models library dalam namespace ApotekSmart.Models
    public class TransaksiResep : BaseTransaksi
    {
        // [ENCAPSULATION] Semua field private, hanya bisa diakses lewat property
        private int _idResep;
        private int? _idApoteker;
        private string _nomorResep;
        private string _namaDokter;
        private string _namaPasien;
        private string _statusValidasi;
        private string _catatanApoteker;
        private DateTime? _validatedAt;

        // [ENCAPSULATION] Property dengan validasi di setter
        public int IdResep
        {
            get { return _idResep; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("IdResep harus lebih dari 0.");
                _idResep = value;
            }
        }

        public int? IdApoteker
        {
            get { return _idApoteker; }
            set
            {
                if (value.HasValue && value.Value <= 0)
                    throw new ArgumentException("IdApoteker harus lebih dari 0.");
                _idApoteker = value;
            }
        }

        public string NomorResep
        {
            get { return _nomorResep; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nomor resep tidak boleh kosong.");
                _nomorResep = value.Trim();
            }
        }

        public string NamaDokter
        {
            get { return _namaDokter; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nama dokter tidak boleh kosong.");
                _namaDokter = value.Trim();
            }
        }

        public string NamaPasien
        {
            get { return _namaPasien; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nama pasien tidak boleh kosong.");
                _namaPasien = value.Trim();
            }
        }

        // [ENCAPSULATION] StatusValidasi hanya izinkan 3 nilai yang valid
        public string StatusValidasi
        {
            get { return _statusValidasi; }
            set
            {
                if (value != "menunggu" && value != "disetujui" && value != "ditolak")
                    throw new ArgumentException(
                        "StatusValidasi harus 'menunggu', 'disetujui', atau 'ditolak'.");
                _statusValidasi = value;
            }
        }

        // [ENCAPSULATION] CatatanApoteker boleh null/kosong
        public string CatatanApoteker
        {
            get { return _catatanApoteker; }
            set { _catatanApoteker = value; }
        }

        // [ENCAPSULATION] ValidatedAt tidak boleh di masa depan
        public DateTime? ValidatedAt
        {
            get { return _validatedAt; }
            set
            {
                if (value.HasValue && value.Value > DateTime.Now)
                    throw new ArgumentException("ValidatedAt tidak boleh di masa depan.");
                _validatedAt = value;
            }
        }

        // [AGGREGATION] TransaksiResep menyimpan referensi ke Apoteker
        // Apoteker bisa hidup tanpa TransaksiResep
        // Resep bisa dibuat dulu sebelum ada Apoteker yang validasi (nullable)
        public Apoteker Apoteker { get; set; }

        // [ENCAPSULATION] Constructor mengatur nilai default yang valid
        // JenisTransaksi, StatusValidasi, Status diset otomatis saat dibuat
        public TransaksiResep()
        {
            JenisTransaksi = "resep";
            StatusValidasi = "menunggu";
            Status = "menunggu";
        }

        // [POLYMORPHISM] override ProsesTransaksi() dari BaseTransaksi
        // Implementasi khusus TransaksiResep — proses bergantung pada StatusValidasi
        // Berbeda dengan transaksi biasa yang langsung selesai
        public override void ProsesTransaksi()
        {
            if (StatusValidasi == "disetujui")
            {
                Status = "selesai";
                Console.WriteLine($"Transaksi Resep {NomorResep} berhasil diproses.");
            }
            else if (StatusValidasi == "ditolak")
            {
                Status = "ditolak";
                Console.WriteLine($"Transaksi Resep {NomorResep} ditolak: {CatatanApoteker}");
            }
            else
            {
                Console.WriteLine(
                    $"Transaksi Resep {NomorResep} menunggu validasi apoteker.");
            }
        }

        // [POLYMORPHISM] override CetakStruk() dari BaseTransaksi
        // Memanggil base.CetakStruk() lalu menambahkan info khusus resep
        // Ini contoh POLYMORPHISM + pemanfaatan method parent lewat base
        public override void CetakStruk()
        {
            base.CetakStruk(); // panggil CetakStruk() milik BaseTransaksi
            Console.WriteLine($"Nomor Resep    : {NomorResep}");
            Console.WriteLine($"Nama Pasien    : {NamaPasien}");
            Console.WriteLine($"Nama Dokter    : {NamaDokter}");
            Console.WriteLine($"Status Validasi: {StatusValidasi}");
            if (!string.IsNullOrEmpty(CatatanApoteker))
                Console.WriteLine($"Catatan        : {CatatanApoteker}");
        }
    }
}