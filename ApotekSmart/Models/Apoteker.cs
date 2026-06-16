using System;

namespace ApotekSmart.Models
{
    // [INHERITANCE] Apoteker mewarisi semua property dan method dari BaseUser
    // [CLASS LIBRARY] Bagian dari Models library dalam namespace ApotekSmart.Models
    public class Apoteker : BaseUser
    {
        // [ENCAPSULATION] Field private, hanya bisa diakses lewat property
        private string _nomorIdentitas;

        // [ENCAPSULATION] Property dengan validasi format SIPA
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

        // [POLYMORPHISM] override TampilInfo() dari BaseUser
        // Implementasi khusus Apoteker — menampilkan SIPA
        public override void TampilInfo()
        {
            Console.WriteLine(
                $"[Apoteker] Nama: {Nama} | SIPA: {NomorIdentitas} | Aktif: {IsActive}");
        }

        // [ASSOCIATION] Apoteker berelasi dengan TransaksiResep
        // Apoteker bisa hidup tanpa TransaksiResep, dan sebaliknya
        // Method ini menunjukkan Apoteker berinteraksi dengan TransaksiResep
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