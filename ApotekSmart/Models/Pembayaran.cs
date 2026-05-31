public class Pembayaran
{
    private decimal _jumlahBayar;

    public int IdPembayaran { get; set; }
    public int IdTransaksi { get; set; }
    public decimal Total { get; set; }
    public string MetodeBayar { get; set; } = "cash";

    // Encapsulation — validasi jumlah bayar tidak boleh negatif
    public decimal JumlahBayar
    {
        get { return _jumlahBayar; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Jumlah bayar tidak boleh negatif.");
            _jumlahBayar = value;
        }
    }

    // Encapsulation — Kembalian read-only, dihitung otomatis
    public decimal Kembalian
    {
        get { return _jumlahBayar - Total; }
    }
}