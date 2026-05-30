using System.Collections.Generic;

namespace ApotekSmart.Interfaces
{
    public interface ICRUDService<T>
    {
        bool Create(T entity);
        List<T> ReadAll();
        T ReadById(int id);
        bool Update(T entity);
        bool Delete(int id);
    }
}