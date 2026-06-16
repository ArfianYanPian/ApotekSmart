using System.Collections.Generic;

namespace ApotekSmart.Interfaces
{
    // [INTERFACE] ICRUDService adalah interface generic
    // Mendefinisikan kontrak operasi CRUD yang wajib diimplementasikan
    // [CLASS LIBRARY] Bagian dari Interfaces library dalam namespace ApotekSmart.Interfaces
    // Generic <T> memungkinkan interface ini dipakai untuk tipe data apapun
    // Contoh: ICRUDService<BaseUser> → UserController
    public interface ICRUDService<T>
    {
        // [INTERFACE] Semua method di bawah adalah kontrak yang WAJIB
        // diimplementasikan oleh class yang menggunakan interface ini

        // [POLYMORPHISM] Setiap implementasi Create() berbeda tergantung tipe T
        // UserController.Create() → insert ke tabel users
        // (jika ada ObatController implements ini → insert ke tabel obat)
        bool Create(T entity);

        // [POLYMORPHISM] ReadAll() berbeda tergantung tipe T
        // UserController.ReadAll() → SELECT dari tabel users
        List<T> ReadAll();
        T ReadById(int id);
        bool Update(T entity);
        bool Delete(int id);
    }
}