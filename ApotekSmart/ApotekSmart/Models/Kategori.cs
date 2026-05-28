using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApotekSmart.Models
{
    public class Kategori
    {
        public int IdKategori { get; set; }
        public string NamaKategori { get; set; }
        public string Deskripsi { get; set; }

        public override string ToString()
        {
            return NamaKategori;
        }
    }
}
