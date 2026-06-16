-- FUNCTION 1: Hitung total transaksi
CREATE OR REPLACE FUNCTION fn_hitung_total(p_transaksi_id INT)
RETURNS NUMERIC AS $$
DECLARE
    v_total NUMERIC;
BEGIN
    SELECT COALESCE(SUM(subtotal), 0)
    INTO v_total
    FROM detail_transaksi
    WHERE id_transaksi = p_transaksi_id;
    
    RETURN v_total;
END;
$$ LANGUAGE plpgsql;

-- FUNCTION 2: Cek stok cukup atau tidak
CREATE OR REPLACE FUNCTION fn_cek_stok(p_obat_id INT, p_qty INT)
RETURNS BOOLEAN AS $$
DECLARE
    v_stok INT;
BEGIN
    SELECT stok INTO v_stok
    FROM obat
    WHERE id_obat = p_obat_id;
    
    RETURN v_stok >= p_qty;
END;
$$ LANGUAGE plpgsql;

-- FUNCTION 3: Cek kelayakan obat
CREATE OR REPLACE FUNCTION fn_cek_kelayakan(p_obat_id INT)
RETURNS VARCHAR AS $$
DECLARE
    v_exp DATE;
    v_jenis VARCHAR;
    v_golongan VARCHAR;
BEGIN
    SELECT tanggal_exp, jenis, golongan
    INTO v_exp, v_jenis, v_golongan
    FROM obat
    WHERE id_obat = p_obat_id;

    IF v_exp <= CURRENT_DATE THEN
        RETURN 'KADALUARSA';
    ELSIF v_exp <= CURRENT_DATE + INTERVAL '30 days' THEN
        RETURN 'HAMPIR_EXP';
    ELSIF v_jenis = 'resep' AND (v_golongan IS NULL OR v_golongan = '') THEN
        RETURN 'GOLONGAN_KOSONG';
    ELSE
        RETURN 'LAYAK';
    END IF;
END;
$$ LANGUAGE plpgsql;


-- Uji fn_hitung_total (pakai id transaksi yang ada, kalau belum ada transaksi return 0)
SELECT fn_hitung_total(1);

-- Uji fn_cek_stok (obat id 1, minta qty 5)
SELECT fn_cek_stok(1, 5);

-- Uji fn_cek_kelayakan (obat id 1)
SELECT fn_cek_kelayakan(1);