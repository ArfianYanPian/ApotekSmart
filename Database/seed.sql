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