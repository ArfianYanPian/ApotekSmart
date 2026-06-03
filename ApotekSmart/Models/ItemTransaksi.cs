using System;
using ApotekSmart.Models;

namespace ApotekSmart.Models
{
    public class ItemTransaksi
    {
        public int IdDetailTransaksi { get; set; }
        public int IdTransaksi { get; set; }

        // Agregasi ke BaseObat
        public BaseObat Obat { get; set; }

        public int Qty { get; set; }
        public decimal HargaSatuan { get; set; }
        public string AturanPakai { get; set; }

        // Encapsulation — Subtotal read-only, dihitung otomatis
        public decimal Subtotal
        {
            get { return Qty * HargaSatuan; }
        }
    }
}