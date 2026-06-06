using System;

namespace ApotekSmart.Models
{
    public class Apoteker : BaseUser
    {
        private string _nomorIdentitas;

        public string NomorIdentitas
        {
            get { return _nomorIdentitas; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nomor SIPA tidak boleh kosong.");
                if (!value.StartsWith("SIPA-"))
                    throw new ArgumentException("Nomor SIPA harus diawali 'SIPA-'.");
                _nomorIdentitas = value.Trim();
            }
        }

        public override void TampilInfo()
        {
            Console.WriteLine(
                $"[Apoteker] Nama: {Nama} | SIPA: {NomorIdentitas} | Aktif: {IsActive}");
        }

        public void ValidasiResep(TransaksiResep transaksi,
                                   string statusValidasi, string catatan)
        {
            if (transaksi == null)
                throw new ArgumentNullException("Transaksi tidak boleh null.");
            transaksi.StatusValidasi = statusValidasi;
            transaksi.CatatanApoteker = catatan;
            transaksi.ValidatedAt = DateTime.Now;
            transaksi.IdApoteker = IdUser;
            Console.WriteLine(
                $"Apoteker {Nama} memberi status '{statusValidasi}' pada resep {transaksi.NomorResep}.");
        }
    }
}