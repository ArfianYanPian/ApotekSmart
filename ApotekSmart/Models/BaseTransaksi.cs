using System;
using System.Collections.Generic;

namespace ApotekSmart.Models
{
    // [ABSTRACT CLASS] BaseTransaksi adalah abstract class — tidak bisa diinstansiasi langsung
    // [CLASS LIBRARY] Bagian dari Models library dalam namespace ApotekSmart.Models
    public abstract class BaseTransaksi
    {
        // [ENCAPSULATION] Semua field private, hanya bisa diakses lewat property
        private int _idTransaksi;
        private int _idKasir;
        private string _jenisTransaksi;
        private string _status;
        private DateTime _createdAt;

        // [ENCAPSULATION] Property dengan validasi di setter
        public int IdTransaksi
        {
            get { return _idTransaksi; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("IdTransaksi harus lebih dari 0.");
                _idTransaksi = value;
            }
        }

        public int IdKasir
        {
            get { return _idKasir; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("IdKasir harus lebih dari 0.");
                _idKasir = value;
            }
        }

        // [ENCAPSULATION] Setter membatasi nilai hanya 'biasa' atau 'resep'
        public string JenisTransaksi
        {
            get { return _jenisTransaksi; }
            set
            {
                if (value != "biasa" && value != "resep")
                    throw new ArgumentException("JenisTransaksi harus 'biasa' atau 'resep'.");
                _jenisTransaksi = value;
            }
        }

        // [ENCAPSULATION] Setter membatasi nilai status yang valid
        public string Status
        {
            get { return _status; }
            set
            {
                if (value != "selesai" && value != "menunggu" && value != "ditolak")
                    throw new ArgumentException(
                        "Status harus 'selesai', 'menunggu', atau 'ditolak'.");
                _status = value;
            }
        }

        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("CreatedAt tidak boleh di masa depan.");
                _createdAt = value;
            }
        }

        // [ENCAPSULATION] Total hanya bisa diubah dari dalam class (protected set)
        // Tidak bisa diset dari luar — harus lewat HitungTotal()
        public decimal Total { get; protected set; }

        // [COMPOSITION] BaseTransaksi memiliki List<ItemTransaksi>
        // Jika BaseTransaksi dihancurkan, Items ikut dihancurkan
        // [ASSOCIATION] BaseTransaksi berelasi dengan class Kasir
        public List<ItemTransaksi> Items { get; set; } = new List<ItemTransaksi>();

        // [ASSOCIATION] BaseTransaksi berelasi dengan Kasir
        // Kasir bisa hidup tanpa BaseTransaksi
        public Kasir Kasir { get; set; }

        // [ENCAPSULATION] HitungTotal() mengontrol cara Total dihitung
        // Total tidak bisa dimanipulasi langsung dari luar
        public void HitungTotal()
        {
            Total = 0;
            foreach (var item in Items)
                Total += item.Subtotal;
        }

        // [ABSTRACT] ProsesTransaksi wajib di-override oleh subclass
        // [POLYMORPHISM] TransaksiBiasa dan TransaksiResep punya proses berbeda
        public abstract void ProsesTransaksi();

        // [POLYMORPHISM] virtual — subclass boleh override CetakStruk()
        // TransaksiResep misalnya menambahkan info nomor resep di struk
        public virtual void CetakStruk()
        {
            Console.WriteLine("=====================================");
            Console.WriteLine($"ID Transaksi : {IdTransaksi}");
            Console.WriteLine($"Jenis        : {JenisTransaksi}");
            Console.WriteLine($"Status       : {Status}");
            Console.WriteLine($"Total        : Rp {Total:N0}");
            Console.WriteLine("=====================================");
        }
    }
}