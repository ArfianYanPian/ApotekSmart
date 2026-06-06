using System;

namespace ApotekSmart.Models
{
    public class Kasir : BaseUser
    {
        private string _nomorShift;

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

        // FIX: null-safe — NomorShift bisa null jika belum diisi di DB
        public override void TampilInfo()
        {
            string shift = string.IsNullOrWhiteSpace(_nomorShift) ? "-" : _nomorShift;
            Console.WriteLine(
                $"[Kasir] Nama: {Nama} | Shift: {shift} | Aktif: {IsActive}");
        }

        public void BuatTransaksiBaru()
        {
            Console.WriteLine($"Kasir {Nama} sedang memproses transaksi baru.");
        }
    }
}