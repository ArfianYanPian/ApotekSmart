using System;
using System.Data;

namespace ApotekSmart.Interfaces
{
    public interface ILaporan
    {
        DataTable GenerateLaporan(DateTime tanggalMulai, DateTime tanggalAkhir);
        void CetakLaporan();
    }
}