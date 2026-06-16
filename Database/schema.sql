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