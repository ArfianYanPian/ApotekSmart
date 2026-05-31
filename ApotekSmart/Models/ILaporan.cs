using System;
using System.Data;

namespace ApotekSmart.Models
{
    public interface ILaporan
    {
        DataTable Generate();
    }

    public abstract class LaporanBase : ILaporan
    {
        public DateTime Periode { get; protected set; }

        protected LaporanBase(DateTime periode)
        {
            Periode = periode;
        }

        public abstract DataTable Generate();
    }
}
