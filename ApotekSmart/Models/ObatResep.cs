using System;

namespace ApotekSmart.Models
{
    public class ObatResep : BaseObat
    {
        public string GolonganObat { get; set; }

        public override bool CekKelayakan()
        {
            return TanggalExp > DateTime.Now.AddDays(30)
                && !string.IsNullOrEmpty(GolonganObat);
        }

        public string GetStatusKelayakan()
        {
            if (TanggalExp <= DateTime.Now)
                return "KADALUARSA";
            else if (TanggalExp <= DateTime.Now.AddDays(30))
                return "HAMPIR_EXP";
            else if (string.IsNullOrEmpty(GolonganObat))
                return "GOLONGAN_KOSONG";
            else
                return "LAYAK";
        }
    }
}