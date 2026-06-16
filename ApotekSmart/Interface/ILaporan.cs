using System;
using System.Data;

namespace ApotekSmart.Interfaces
{
    // [INTERFACE] ILaporan mendefinisikan kontrak untuk semua jenis laporan
    // Setiap class laporan WAJIB mengimplementasikan GenerateLaporan() dan CetakLaporan()
    // [CLASS LIBRARY] Bagian dari Interfaces library dalam namespace ApotekSmart.Interfaces
    public interface ILaporan
    {
        // [INTERFACE] Kontrak generate laporan — wajib diimplementasikan
        DataTable GenerateLaporan(DateTime tanggalMulai, DateTime tanggalAkhir);

        // [INTERFACE] Kontrak cetak laporan — wajib diimplementasikan
        // [POLYMORPHISM] Setiap class laporan bisa cetak dengan cara berbeda
        void CetakLaporan();
    }
}