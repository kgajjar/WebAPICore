using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Repository.IRepository
{
    public interface IRepository<T> where T : class// Generic class here to reuse for Trails and National Parks
    {
        //Async Methods so we are using Task
        Task<T> GetAsync(string url, int Id, string token);

        Task<IEnumerable> GetAllAsync(string url, string token);

        Task<bool> CreateAsync(string url, T objToCreate, string token);

        Task<bool> UpdateAsync(string url, T objToUpdate, string token);

        Task<bool> DeleteAsync(string url, int Id, string token);

    }
}
