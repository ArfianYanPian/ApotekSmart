using System;

namespace ApotekSmart.Models
{
    public class ItemTransaksi
    {
        public int IdObat { get; private set; }
        public string NamaObat { get; private set; }
        public int Jumlah { get; private set; }
        public decimal HargaSatuan { get; private set; }

        public decimal Subtotal
        {
            get { return Jumlah * HargaSatuan; }
        }

        public ItemTransaksi(int idObat, string namaObat, int jumlah, decimal hargaSatuan)
        {
            if (idObat <= 0)
                throw new Exception("Id obat tidak valid.");

            if (string.IsNullOrWhiteSpace(namaObat))
                throw new Exception("Nama obat wajib diisi.");

            if (jumlah <= 0)
                throw new Exception("Jumlah harus lebih dari 0.");

            if (hargaSatuan < 0)
                throw new Exception("Harga satuan tidak boleh negatif.");

            IdObat = idObat;
            NamaObat = namaObat;
            Jumlah = jumlah;
            HargaSatuan = hargaSatuan;
        }
    }
}
