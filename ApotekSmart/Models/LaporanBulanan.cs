using System;
using System.Data;
using ApotekSmart.Controllers;

namespace ApotekSmart.Models
{
    public class LaporanBulanan : LaporanBase
    {
        public LaporanBulanan(DateTime bulan) : base(bulan)
        {
        }

        public override DataTable Generate()
        {
            LaporanController controller = new LaporanController();
            return controller.GetLaporanBulanan(Periode.Year, Periode.Month);
        }
    }
}
