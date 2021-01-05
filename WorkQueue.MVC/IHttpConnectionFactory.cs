using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallBack_Model.Model;

namespace WorkQueue.MVC
{
    public interface IHttpConnectionFactory<TModel> where TModel : class, new()
    {
        Task<IEnumerable<TModel>> GetAllAsync();

        Task<IEnumerable<TModel>> GetAllAsync(int Id);

        Task<TModel> GetAsync(int Id);

        Task<IEnumerable<TModel>> GetSearchAsync(object SearchParameter);

        Task<bool> PostAsync(TModel ModelItem);

        Task<bool> PutAsync(int QueueResult, TModel QIteHolder);

        Task<bool> PutAsync(TModel x);

        Task<bool> DeleteAsync(TModel x);
    }
}
