using System;

namespace ApotekSmart.Models
{
    public class ObatBebas : BaseObat
    {
        public override bool CekKelayakan()
            => TanggalExp > DateTime.Now.AddDays(30);

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