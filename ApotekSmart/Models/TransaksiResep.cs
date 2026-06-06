using System;

namespace ApotekSmart.Models
{
    public class TransaksiResep : BaseTransaksi
    {
        private int _idResep;
        private int? _idApoteker;
        private string _nomorResep;
        private string _namaDokter;
        private string _namaPasien;
        private string _statusValidasi;
        private string _catatanApoteker;
        private DateTime? _validatedAt;

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

        public string CatatanApoteker
        {
            get { return _catatanApoteker; }
            set { _catatanApoteker = value; } // boleh null/kosong
        }

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

        public Apoteker Apoteker { get; set; }

        public TransaksiResep()
        {
            JenisTransaksi = "resep";
            StatusValidasi = "menunggu";
            Status = "menunggu";
        }

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

        public override void CetakStruk()
        {
            base.CetakStruk();
            Console.WriteLine($"Nomor Resep    : {NomorResep}");
            Console.WriteLine($"Nama Pasien    : {NamaPasien}");
            Console.WriteLine($"Nama Dokter    : {NamaDokter}");
            Console.WriteLine($"Status Validasi: {StatusValidasi}");
            if (!string.IsNullOrEmpty(CatatanApoteker))
                Console.WriteLine($"Catatan        : {CatatanApoteker}");
        }
    }
}