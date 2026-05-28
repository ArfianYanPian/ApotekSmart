using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApotekSmart.Models
{
    public class ObatBebas : BaseObat
    {
        // Inheritance + Polymorphism
        // Override CekKelayakan() — layak jika exp masih > 30 hari dari sekarang
        public override bool CekKelayakan()
        {
            return TanggalExp > DateTime.Now.AddDays(30);
        }

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
