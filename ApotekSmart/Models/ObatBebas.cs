using System;

namespace ApotekSmart.Models
{
    // [INHERITANCE] ObatBebas mewarisi semua property dan method dari BaseObat
    // [CLASS LIBRARY] Bagian dari Models library dalam namespace ApotekSmart.Models
    public class ObatBebas : BaseObat
    {
        // [POLYMORPHISM] override CekKelayakan() dari BaseObat
        // Implementasi khusus ObatBebas — hanya cek tanggal exp
        // Tidak perlu cek golongan seperti ObatResep
        public override bool CekKelayakan()
            => TanggalExp > DateTime.Now.AddDays(30);

        // [ENCAPSULATION] GetStatusKelayakan() menyembunyikan logika
        // penentuan status kelayakan di dalam class
        // Pemanggil cukup dapat string status tanpa tahu cara hitungnya
        public string GetStatusKelayakan()
        {
            if (TanggalExp <= DateTime.Now)
                return "KADALUARSA";
            else if (TanggalExp <= DateTime.Now.AddDays(30))
                return "HAMPIR_EXP";
            else
                return "LAYAK";
        }
    }
}