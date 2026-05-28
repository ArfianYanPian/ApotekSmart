-- TRIGGER 1: Kurangi stok otomatis setelah insert detail_transaksi
CREATE OR REPLACE FUNCTION fn_trigger_kurangi_stok()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE obat 
    SET stok = stok - NEW.qty
    WHERE id_obat = NEW.id_obat;
    
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE TRIGGER trg_kurangi_stok
AFTER INSERT ON detail_transaksi
FOR EACH ROW
EXECUTE FUNCTION fn_trigger_kurangi_stok();

-- TRIGGER 2: Log setiap perubahan stok
CREATE OR REPLACE FUNCTION fn_trigger_log_stok()
RETURNS TRIGGER AS $$
BEGIN
    IF OLD.stok <> NEW.stok THEN
        INSERT INTO log_stok (id_obat, stok_sebelum, stok_sesudah, keterangan)
        VALUES (NEW.id_obat, OLD.stok, NEW.stok, 'Update otomatis');
    END IF;
    
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE TRIGGER trg_log_perubahan_stok
AFTER UPDATE ON obat
FOR EACH ROW
EXECUTE FUNCTION fn_trigger_log_stok();

-- TRIGGER 3: Alert jika stok di bawah minimum
CREATE OR REPLACE FUNCTION fn_trigger_cek_stok_minimum()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.stok <= NEW.stok_minimum THEN
        INSERT INTO alert_stok (id_obat, pesan)
        VALUES (
            NEW.id_obat,
            'Stok ' || (SELECT nama_obat FROM obat WHERE id_obat = NEW.id_obat) || 
            ' hampir habis! Sisa: ' || NEW.stok
        );
    END IF;
    
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE TRIGGER trg_cek_stok_minimum
AFTER UPDATE ON obat
FOR EACH ROW
EXECUTE FUNCTION fn_trigger_cek_stok_minimum();

-- Uji trg_kurangi_stok: insert detail transaksi, cek stok berkurang
-- Cek stok obat id 1 sebelum
SELECT id_obat, nama_obat, stok FROM obat WHERE id_obat = 1;

-- Insert detail transaksi baru
INSERT INTO detail_transaksi (id_transaksi, id_obat, qty, harga_satuan, subtotal)
VALUES (1, 1, 5, 1000, 5000);

-- Cek stok setelah -- harusnya berkurang 5
SELECT id_obat, nama_obat, stok FROM obat WHERE id_obat = 1;

-- Cek log_stok -- harusnya ada entry baru
SELECT * FROM log_stok;

-- Cek alert_stok -- muncul kalau stok <= stok_minimum
SELECT * FROM alert_stok;