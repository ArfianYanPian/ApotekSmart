using System;
using System.Collections.Generic;
using System.Linq;

namespace ApotekSmart.Models
{
    public interface IPembayaran
    {
        decimal HitungTotal();
        decimal HitungKembalian();
    }

    public abstract class Pembayaran : IPembayaran
    {
        public int IdTransaksi { get; protected set; }
        public string MetodeBayar { get; protected set; }
        public decimal UangDiterima { get; protected set; }
        public List<ItemTransaksi> Items { get; protected set; }

        protected Pembayaran(int idTransaksi, string metodeBayar, decimal uangDiterima, List<ItemTransaksi> items)
        {
            if (uangDiterima < 0)
                throw new Exception("Uang diterima tidak boleh negatif.");

            if (items == null || items.Count == 0)
                throw new Exception("Item transaksi belum ada.");

            IdTransaksi = idTransaksi;
            MetodeBayar = metodeBayar;
            UangDiterima = uangDiterima;
            Items = items;
        }

        public decimal HitungTotal()
        {
            return Items.Sum(item => item.Subtotal);
        }

        public abstract decimal HitungKembalian();
    }

    public class PembayaranTunai : Pembayaran
    {
        public PembayaranTunai(int idTransaksi, decimal uangDiterima, List<ItemTransaksi> items)
            : base(idTransaksi, "tunai", uangDiterima, items)
        {
        }

        public override decimal HitungKembalian()
        {
            decimal total = HitungTotal();

            if (UangDiterima < total)
                throw new Exception("Uang tunai kurang dari total pembayaran.");

            return UangDiterima - total;
        }
    }

    public class PembayaranTransfer : Pembayaran
    {
        public PembayaranTransfer(int idTransaksi, decimal uangDiterima, List<ItemTransaksi> items)
            : base(idTransaksi, "transfer", uangDiterima, items)
        {
        }

        public override decimal HitungKembalian()
        {
            decimal total = HitungTotal();

            if (UangDiterima < total)
                throw new Exception("Nominal transfer kurang dari total pembayaran.");

            return UangDiterima - total;
        }
    }

    public class PembayaranQris : Pembayaran
    {
        public PembayaranQris(int idTransaksi, decimal uangDiterima, List<ItemTransaksi> items)
            : base(idTransaksi, "qris", uangDiterima, items)
        {
        }

        public override decimal HitungKembalian()
        {
            decimal total = HitungTotal();

            if (UangDiterima < total)
                throw new Exception("Nominal QRIS kurang dari total pembayaran.");

            return UangDiterima - total;
        }
    }
}
