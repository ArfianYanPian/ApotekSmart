using System;
using System.Data;
using ApotekSmart.Controllers;

namespace ApotekSmart.Models
{
    public class LaporanHarian : LaporanBase
    {
        public LaporanHarian(DateTime tanggal) : base(tanggal)
        {
        }

        public override DataTable Generate()
        {
            LaporanController controller = new LaporanController();
            return controller.GetLaporanHarian(Periode);
        }
    }
}
