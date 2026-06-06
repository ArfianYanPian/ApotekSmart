using System;

namespace ApotekSmart.Models
{
    public class ObatResep : BaseObat
    {
        private string _golonganObat;

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

        public override bool CekKelayakan()
            => TanggalExp > DateTime.Now.AddDays(30) && !string.IsNullOrEmpty(_golonganObat);

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