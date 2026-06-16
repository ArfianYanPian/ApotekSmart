using System;

namespace ApotekSmart.Models
{
    // [INHERITANCE] ObatResep mewarisi semua property dan method dari BaseObat
    // [CLASS LIBRARY] Bagian dari Models library dalam namespace ApotekSmart.Models
    public class ObatResep : BaseObat
    {
        // [ENCAPSULATION] Field private, hanya bisa diakses lewat property
        private string _golonganObat;

        // [ENCAPSULATION] Property dengan validasi ketat — hanya 3 nilai yang diizinkan
        // Ini aturan bisnis khusus ObatResep yang tidak ada di ObatBebas
        public string GolonganObat
        {
            get { return _golonganObat; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Golongan obat tidak boleh kosong.");
                if (value != "Keras" && value != "Psikotropika" && value != "Narkotika")
                    throw new ArgumentException(
                        "Golongan harus 'Keras', 'Psikotropika', atau 'Narkotika'.");
                _golonganObat = value;
            }
        }

        // [POLYMORPHISM] override CekKelayakan() dari BaseObat
        // Implementasi khusus ObatResep — cek tanggal exp DAN golongan
        // Berbeda dengan ObatBebas yang hanya cek tanggal exp
        public override bool CekKelayakan()
            => TanggalExp > DateTime.Now.AddDays(30) && !string.IsNullOrEmpty(_golonganObat);

        // [ENCAPSULATION] GetStatusKelayakan() menyembunyikan logika
        // penentuan status kelayakan di dalam class
        // ObatResep punya status tambahan: GOLONGAN_KOSONG
        // yang tidak ada di ObatBebas
        public string GetStatusKelayakan()
        {
            if (TanggalExp <= DateTime.Now)
                return "KADALUARSA";
            else if (TanggalExp <= DateTime.Now.AddDays(30))
                return "HAMPIR_EXP";
            else if (string.IsNullOrEmpty(_golonganObat))
                return "GOLONGAN_KOSONG";
            else
                return "LAYAK";
        }
    }
}