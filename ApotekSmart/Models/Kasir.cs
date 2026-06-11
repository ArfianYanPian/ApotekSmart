using System;

namespace ApotekSmart.Models
{
    // [INHERITANCE] Kasir mewarisi semua property dan method dari BaseUser
    // [CLASS LIBRARY] Bagian dari Models library dalam namespace ApotekSmart.Models
    public class Kasir : BaseUser
    {
        // [ENCAPSULATION] Field private, hanya bisa diakses lewat property
        private string _nomorShift;

        // [ENCAPSULATION] Property dengan validasi format SHIFT-
        public string NomorShift
        {
            get { return _nomorShift; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nomor shift tidak boleh kosong.");
                if (!value.StartsWith("SHIFT-"))
                    throw new ArgumentException("Nomor shift harus diawali 'SHIFT-'.");
                _nomorShift = value.Trim();
            }
        }

        // [POLYMORPHISM] override TampilInfo() dari BaseUser
        // Implementasi khusus Kasir — menampilkan nomor shift
        // [ENCAPSULATION] Akses _nomorShift langsung (bukan lewat property)
        // agar tidak throw exception saat nilai null
        public override void TampilInfo()
        {
            string shift = string.IsNullOrWhiteSpace(_nomorShift) ? "-" : _nomorShift;
            Console.WriteLine(
                $"[Kasir] Nama: {Nama} | Shift: {shift} | Aktif: {IsActive}");
        }

        // Method khusus Kasir — tidak ada di BaseUser maupun Apoteker
        public void BuatTransaksiBaru()
        {
            Console.WriteLine($"Kasir {Nama} sedang memproses transaksi baru.");
        }
    }
}