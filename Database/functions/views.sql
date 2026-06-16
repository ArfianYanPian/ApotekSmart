-- VIEW 1: Stok obat lengkap dengan kategori
CREATE OR REPLACE VIEW v_stok_obat AS
SELECT 
    o.id_obat,
    o.nama_obat,
    o.jenis,
    o.golongan,
    k.nama_kategori,
    o.satuan,
    o.harga_beli,
    o.harga_jual,
    o.stok,
    o.stok_minimum,
    o.tanggal_exp,
    o.is_active,
    CASE 
        WHEN o.tanggal_exp <= CURRENT_DATE THEN 'KADALUARSA'
        WHEN o.tanggal_exp <= CURRENT_DATE + INTERVAL '30 days' THEN 'HAMPIR_EXP'
        ELSE 'LAYAK'
    END AS status_kelayakan,
    CASE 
        WHEN o.stok <= 0 THEN 'HABIS'
        WHEN o.stok <= o.stok_minimum THEN 'KRITIS'
        ELSE 'AMAN'
    END AS status_stok
FROM obat o
JOIN kategori k ON o.id_kategori = k.id_kategori
WHERE o.is_active = TRUE;

-- VIEW 2: Detail transaksi lengkap
CREATE OR REPLACE VIEW v_transaksi_detail AS
SELECT 
    t.id_transaksi,
    t.created_at AS tanggal,
    t.jenis_transaksi,
    t.status,
    u.nama AS nama_kasir,
    o.nama_obat,
    dt.qty,
    dt.harga_satuan,
    dt.subtotal,
    t.total,
    p.jumlah_bayar,
    p.kembalian
FROM transaksi t
JOIN users u ON t.id_kasir = u.id_user
JOIN detail_transaksi dt ON t.id_transaksi = dt.id_transaksi
JOIN obat o ON dt.id_obat = o.id_obat
LEFT JOIN pembayaran p ON t.id_transaksi = p.id_transaksi;

-- VIEW 3: Obat hampir kadaluarsa dan kadaluarsa
CREATE OR REPLACE VIEW v_obat_kadaluarsa AS
SELECT 
    o.id_obat,
    o.nama_obat,
    o.jenis,
    k.nama_kategori,
    o.stok,
    o.tanggal_exp,
    CURRENT_DATE - o.tanggal_exp AS hari_sejak_exp,
    o.tanggal_exp - CURRENT_DATE AS hari_sampai_exp,
    CASE 
        WHEN o.tanggal_exp <= CURRENT_DATE THEN 'KADALUARSA'
        WHEN o.tanggal_exp <= CURRENT_DATE + INTERVAL '30 days' THEN 'HAMPIR_EXP'
        ELSE 'LAYAK'
    END AS status
FROM obat o
JOIN kategori k ON o.id_kategori = k.id_kategori
WHERE o.tanggal_exp <= CURRENT_DATE + INTERVAL '30 days'
AND o.is_active = TRUE
ORDER BY o.tanggal_exp ASC;