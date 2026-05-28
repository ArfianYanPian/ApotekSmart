-- TABEL USERS
CREATE TABLE users (
    id_user SERIAL PRIMARY KEY,
    nama VARCHAR(100) NOT NULL,
    username VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    role VARCHAR(20) NOT NULL CHECK (role IN ('apoteker', 'kasir')),
    nomor_identitas VARCHAR(50),
    nomor_shift VARCHAR(20),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE
);

-- TABEL KATEGORI
CREATE TABLE kategori (
    id_kategori SERIAL PRIMARY KEY,
    nama_kategori VARCHAR(100) UNIQUE NOT NULL,
    deskripsi TEXT
);

-- TABEL OBAT
CREATE TABLE obat (
    id_obat SERIAL PRIMARY KEY,
    id_kategori INT NOT NULL REFERENCES kategori(id_kategori),
    nama_obat VARCHAR(150) NOT NULL,
    jenis VARCHAR(20) NOT NULL CHECK (jenis IN ('bebas', 'resep')),
    golongan VARCHAR(50),
    satuan VARCHAR(20) NOT NULL,
    harga_beli NUMERIC(12,2) NOT NULL CHECK (harga_beli >= 0),
    harga_jual NUMERIC(12,2) NOT NULL CHECK (harga_jual >= 0),
    stok INT NOT NULL DEFAULT 0 CHECK (stok >= 0),
    stok_minimum INT NOT NULL DEFAULT 10,
    tanggal_exp DATE NOT NULL,
    deskripsi TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE
);

-- TABEL TRANSAKSI
CREATE TABLE transaksi (
    id_transaksi SERIAL PRIMARY KEY,
    id_kasir INT NOT NULL REFERENCES users(id_user),
    jenis_transaksi VARCHAR(20) NOT NULL CHECK (jenis_transaksi IN ('biasa', 'resep')),
    status VARCHAR(20) NOT NULL DEFAULT 'selesai' CHECK (status IN ('selesai', 'menunggu', 'ditolak')),
    total NUMERIC(12,2) NOT NULL DEFAULT 0,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- TABEL DETAIL TRANSAKSI
CREATE TABLE detail_transaksi (
    id_detail SERIAL PRIMARY KEY,
    id_transaksi INT NOT NULL REFERENCES transaksi(id_transaksi) ON DELETE CASCADE,
    id_obat INT NOT NULL REFERENCES obat(id_obat),
    qty INT NOT NULL CHECK (qty > 0),
    harga_satuan NUMERIC(12,2) NOT NULL,
    subtotal NUMERIC(12,2) NOT NULL
);

-- TABEL PEMBAYARAN
CREATE TABLE pembayaran (
    id_pembayaran SERIAL PRIMARY KEY,
    id_transaksi INT NOT NULL UNIQUE REFERENCES transaksi(id_transaksi) ON DELETE CASCADE,
    jumlah_bayar NUMERIC(12,2) NOT NULL CHECK (jumlah_bayar >= 0),
    kembalian NUMERIC(12,2) NOT NULL DEFAULT 0,
    metode VARCHAR(20) NOT NULL DEFAULT 'cash',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- TABEL RESEP
CREATE TABLE resep (
    id_resep SERIAL PRIMARY KEY,
    id_transaksi INT NOT NULL REFERENCES transaksi(id_transaksi) ON DELETE CASCADE,
    id_apoteker INT REFERENCES users(id_user),
    nomor_resep VARCHAR(50) UNIQUE NOT NULL,
    nama_dokter VARCHAR(100),
    nama_pasien VARCHAR(100) NOT NULL,
    status_validasi VARCHAR(20) NOT NULL DEFAULT 'menunggu' CHECK (status_validasi IN ('menunggu', 'disetujui', 'ditolak')),
    catatan_apoteker TEXT,
    validated_at TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- TABEL DETAIL RESEP
CREATE TABLE detail_resep (
    id_detail_resep SERIAL PRIMARY KEY,
    id_resep INT NOT NULL REFERENCES resep(id_resep) ON DELETE CASCADE,
    id_obat INT NOT NULL REFERENCES obat(id_obat),
    qty INT NOT NULL CHECK (qty > 0),
    aturan_pakai VARCHAR(100)
);

-- TABEL LOG STOK
CREATE TABLE log_stok (
    id_log SERIAL PRIMARY KEY,
    id_obat INT NOT NULL REFERENCES obat(id_obat),
    id_user INT REFERENCES users(id_user),
    stok_sebelum INT NOT NULL,
    stok_sesudah INT NOT NULL,
    keterangan VARCHAR(100),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- TABEL ALERT STOK
CREATE TABLE alert_stok (
    id_alert SERIAL PRIMARY KEY,
    id_obat INT NOT NULL REFERENCES obat(id_obat),
    pesan TEXT NOT NULL,
    is_read BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- KATEGORI
INSERT INTO kategori (nama_kategori, deskripsi) VALUES
('Antibiotik', 'Obat untuk mengatasi infeksi bakteri'),
('Analgesik', 'Obat pereda nyeri dan demam'),
('Vitamin & Suplemen', 'Suplemen untuk menjaga kesehatan'),
('Antasida', 'Obat gangguan lambung dan pencernaan'),
('Antihistamin', 'Obat alergi dan reaksi hipersensitivitas');

-- USERS (password disimpan plaintext dulu, nanti di-hash dari C#)
INSERT INTO users (nama, username, password, role, nomor_identitas) VALUES
('Reihan Islami', 'apoteker1', 'apotek123', 'apoteker', 'SIPA-2024-001'),
('Arfian Dzaki', 'kasir1', 'kasir123', 'kasir', NULL);

-- OBAT BEBAS
INSERT INTO obat (id_kategori, nama_obat, jenis, satuan, harga_beli, harga_jual, stok, stok_minimum, tanggal_exp) VALUES
(2, 'Paracetamol 500mg', 'bebas', 'tablet', 500, 1000, 200, 20, '2026-12-31'),
(2, 'Ibuprofen 400mg', 'bebas', 'tablet', 1500, 3000, 150, 15, '2026-10-31'),
(3, 'Vitamin C 1000mg', 'bebas', 'tablet', 2000, 4000, 100, 10, '2027-06-30'),
(3, 'Vitamin B Complex', 'bebas', 'tablet', 1500, 3500, 80, 10, '2027-03-31'),
(4, 'Antasida Doen', 'bebas', 'tablet', 800, 1500, 120, 15, '2026-08-31'),
(4, 'Omeprazole 20mg', 'bebas', 'kapsul', 2500, 5000, 60, 10, '2026-11-30'),
(5, 'Cetirizine 10mg', 'bebas', 'tablet', 1200, 2500, 90, 10, '2027-01-31'),
(1, 'Amoxicillin 500mg', 'bebas', 'kapsul', 2000, 4000, 75, 10, '2026-09-30');

-- OBAT RESEP
INSERT INTO obat (id_kategori, nama_obat, jenis, golongan, satuan, harga_beli, harga_jual, stok, stok_minimum, tanggal_exp) VALUES
(1, 'Ciprofloxacin 500mg', 'resep', 'Keras', 'tablet', 3000, 6000, 50, 10, '2026-07-31'),
(1, 'Azithromycin 500mg', 'resep', 'Keras', 'tablet', 5000, 10000, 40, 8, '2026-12-31'),
(2, 'Tramadol 50mg', 'resep', 'Psikotropika', 'tablet', 4000, 8000, 30, 5, '2027-02-28'),
(3, 'Metformin 500mg', 'resep', 'Keras', 'tablet', 1500, 3000, 100, 15, '2027-04-30'),
(4, 'Lansoprazole 30mg', 'resep', 'Keras', 'kapsul', 3500, 7000, 45, 8, '2026-10-31'),
(5, 'Loratadine 10mg', 'resep', 'Keras', 'tablet', 2000, 4500, 55, 10, '2027-05-31'),
(2, 'Ketorolac 10mg', 'resep', 'Keras', 'tablet', 3500, 7000, 25, 5, '2026-08-31');