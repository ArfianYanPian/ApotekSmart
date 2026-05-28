using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApotekSmart.Models
{
    public class ObatResep : BaseObat
    {
        // Tambahan property khusus obat resep
        public string GolonganObat { get; set; }

        // Override CekKelayakan() — cek exp + golongan tidak boleh kosong
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
