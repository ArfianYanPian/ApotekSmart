-- QUERY 1: Laporan penjualan ROLLUP (harian → bulanan → tahunan → grand total)
SELECT 
    EXTRACT(YEAR FROM t.created_at) AS tahun,
    EXTRACT(MONTH FROM t.created_at) AS bulan,
    EXTRACT(DAY FROM t.created_at) AS hari,
    COUNT(t.id_transaksi) AS jumlah_transaksi,
    SUM(t.total) AS total_penjualan
FROM transaksi t
WHERE t.status = 'selesai'
GROUP BY ROLLUP(
    EXTRACT(YEAR FROM t.created_at),
    EXTRACT(MONTH FROM t.created_at),
    EXTRACT(DAY FROM t.created_at)
)
ORDER BY tahun, bulan, hari;

-- QUERY 2: Laporan per kategori pakai GROUPING SETS
SELECT 
    k.nama_kategori,
    EXTRACT(MONTH FROM t.created_at) AS bulan,
    SUM(dt.subtotal) AS total_penjualan,
    SUM(dt.qty) AS total_qty
FROM transaksi t
JOIN detail_transaksi dt ON t.id_transaksi = dt.id_transaksi
JOIN obat o ON dt.id_obat = o.id_obat
JOIN kategori k ON o.id_kategori = k.id_kategori
GROUP BY GROUPING SETS(
    (k.nama_kategori, EXTRACT(MONTH FROM t.created_at)),
    (k.nama_kategori),
    ()
)
ORDER BY k.nama_kategori, bulan;

-- QUERY 3: UNION transaksi biasa + resep
SELECT 
    t.id_transaksi,
    t.created_at AS tanggal,
    'Biasa' AS jenis,
    t.total,
    u.nama AS kasir,
    NULL AS nomor_resep
FROM transaksi t
JOIN users u ON t.id_kasir = u.id_user
WHERE t.jenis_transaksi = 'biasa'

UNION ALL

SELECT 
    t.id_transaksi,
    t.created_at AS tanggal,
    'Resep' AS jenis,
    t.total,
    u.nama AS kasir,
    r.nomor_resep
FROM transaksi t
JOIN users u ON t.id_kasir = u.id_user
JOIN resep r ON t.id_transaksi = r.id_transaksi
WHERE t.jenis_transaksi = 'resep'
ORDER BY tanggal DESC;

-- QUERY 4: EXCEPT obat tersedia aktif (ada stok & belum kadaluarsa)
SELECT id_obat, nama_obat FROM obat WHERE stok > 0

EXCEPT

SELECT id_obat, nama_obat FROM obat 
WHERE tanggal_exp <= CURRENT_DATE;

-- QUERY 5: Subquery obat yang belum pernah terjual
SELECT id_obat, nama_obat, stok
FROM obat
WHERE id_obat NOT IN (
    SELECT DISTINCT id_obat FROM detail_transaksi
)
AND is_active = TRUE;