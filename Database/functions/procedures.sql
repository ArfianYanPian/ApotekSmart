-- SP 1: Proses transaksi (dengan transaction)
CREATE OR REPLACE PROCEDURE sp_proses_transaksi(
    p_kasir_id INT,
    p_jenis VARCHAR,
    p_items JSON,
    p_jumlah_bayar NUMERIC
)
LANGUAGE plpgsql AS $$
DECLARE
    v_transaksi_id INT;
    v_total NUMERIC := 0;
    v_item JSON;
    v_obat_id INT;
    v_qty INT;
    v_harga NUMERIC;
    v_subtotal NUMERIC;
    v_kembalian NUMERIC;
BEGIN
    -- Hitung total dulu
    FOR v_item IN SELECT * FROM json_array_elements(p_items)
    LOOP
        v_obat_id := (v_item->>'id_obat')::INT;
        v_qty := (v_item->>'qty')::INT;
        
        -- Cek stok
        IF NOT fn_cek_stok(v_obat_id, v_qty) THEN
            RAISE EXCEPTION 'Stok obat id % tidak cukup', v_obat_id;
        END IF;
        
        SELECT harga_jual INTO v_harga FROM obat WHERE id_obat = v_obat_id;
        v_total := v_total + (v_harga * v_qty);
    END LOOP;

    -- Cek jumlah bayar cukup
    IF p_jumlah_bayar < v_total THEN
        RAISE EXCEPTION 'Jumlah bayar tidak cukup';
    END IF;

    -- Insert transaksi
    INSERT INTO transaksi (id_kasir, jenis_transaksi, status, total)
    VALUES (p_kasir_id, p_jenis, 'selesai', v_total)
    RETURNING id_transaksi INTO v_transaksi_id;

    -- Insert detail transaksi
    FOR v_item IN SELECT * FROM json_array_elements(p_items)
    LOOP
        v_obat_id := (v_item->>'id_obat')::INT;
        v_qty := (v_item->>'qty')::INT;
        SELECT harga_jual INTO v_harga FROM obat WHERE id_obat = v_obat_id;
        v_subtotal := v_harga * v_qty;

        INSERT INTO detail_transaksi (id_transaksi, id_obat, qty, harga_satuan, subtotal)
        VALUES (v_transaksi_id, v_obat_id, v_qty, v_harga, v_subtotal);
    END LOOP;

    -- Insert pembayaran
    v_kembalian := p_jumlah_bayar - v_total;
    INSERT INTO pembayaran (id_transaksi, jumlah_bayar, kembalian, metode)
    VALUES (v_transaksi_id, p_jumlah_bayar, v_kembalian, 'cash');

END;
$$;

-- SP 2: Validasi resep
CREATE OR REPLACE PROCEDURE sp_validasi_resep(
    p_resep_id INT,
    p_apoteker_id INT,
    p_status VARCHAR,
    p_catatan TEXT
)
LANGUAGE plpgsql AS $$
BEGIN
    UPDATE resep
    SET 
        status_validasi = p_status,
        id_apoteker = p_apoteker_id,
        catatan_apoteker = p_catatan,
        validated_at = CURRENT_TIMESTAMP
    WHERE id_resep = p_resep_id;

    IF NOT FOUND THEN
        RAISE EXCEPTION 'Resep id % tidak ditemukan', p_resep_id;
    END IF;
END;
$$;

-- SP 3: Update stok masuk
CREATE OR REPLACE PROCEDURE sp_update_stok(
    p_obat_id INT,
    p_qty_masuk INT,
    p_keterangan VARCHAR,
    p_user_id INT
)
LANGUAGE plpgsql AS $$
DECLARE
    v_stok_sebelum INT;
    v_stok_sesudah INT;
BEGIN
    SELECT stok INTO v_stok_sebelum
    FROM obat WHERE id_obat = p_obat_id;

    v_stok_sesudah := v_stok_sebelum + p_qty_masuk;

    UPDATE obat SET stok = v_stok_sesudah
    WHERE id_obat = p_obat_id;

    INSERT INTO log_stok (id_obat, id_user, stok_sebelum, stok_sesudah, keterangan)
    VALUES (p_obat_id, p_user_id, v_stok_sebelum, v_stok_sesudah, p_keterangan);
END;
$$;

-- Uji sp_proses_transaksi (kasir id 2, beli paracetamol id 1 qty 2, bayar 5000)
CALL sp_proses_transaksi(2, 'biasa', '[{"id_obat":1,"qty":2}]', 5000);

-- Cek hasilnya
SELECT * FROM transaksi;
SELECT * FROM detail_transaksi;
SELECT * FROM pembayaran;

-- Uji sp_update_stok (obat id 1, tambah 50, user id 1)
CALL sp_update_stok(1, 50, 'Stok masuk dari supplier', 1);

-- Cek log stok
SELECT * FROM log_stok;