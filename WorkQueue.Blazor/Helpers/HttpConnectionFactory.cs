using CallBack_Model.Model;
using CoreRestfulHttpClientWrapper;
using DebtManager3AccountModels.Models;
using DebtManager3NotesModels.Models;
using DomainData.Models;
using DomainData.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace WorkQueue.Blazor.Helpers
{
    public class HttpConnectionFactory<T> : IHttpConnectionFactory<T> where T : class, new()
    {
        private IConfiguration _configuration;
        private IRestfulHttpClientWrapper<T> _restfulHttp;

        public HttpConnectionFactory(IConfiguration xConfiguration)
        {
            _configuration = xConfiguration;
            _restfulHttp = new RestfulHttpClientWrapper<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _restfulHttp.GetAllAsync(FactoryBase(new T()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync(int Id)
        {
            return await _restfulHttp.GetAllAsync(FactoryBase(new T()) + "/" + Id.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(int Id)
        {
            var newObject = new T();
            string uri = FactoryBase(newObject);
            switch (newObject)
            {
                case AccountModel accountModel:
                    return await _restfulHttp.GetAsync(string.Format(uri, Id));
                case DomainViewModel domainViewModel:
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetStringAsync(uri + "/" + Id.ToString());
                        return  JsonConvert.DeserializeObject<T>(response, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.Objects
                        });
                    }
            }
            return await _restfulHttp.GetAsync(uri + "/" + Id.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SearchParameter"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetSearchAsync(object SearchParameter)
        {
            var newObject = new T();
            string uri = FactoryBase(newObject);
            switch (newObject)
            {
                case NotesViewModel notesViewModel:
                    // expected SearchParameter is list of string with 3 items: Account #, Start Date, End Date 
                    if (SearchParameter is List<string>)
                        return await _restfulHttp.GetAllAsync(string.Format(uri, ((List<string>)SearchParameter).ToArray()));
                    break;
            }
            return await _restfulHttp.PutObjectAsJsonAsync(uri + "/search", SearchParameter);
        }

        /// <summary>
        /// Put Complete, QueueItemHolder
        /// </summary>
        /// <param name="QueueResult"></param>
        /// <param name="QIteHolder"></param>
        /// <returns></returns>
        public async Task<bool> PostAsync(T QIteHolder)
        {
            if (QIteHolder is QItemHolder item)
            {
                string uri = FactoryBase(item.TModel);
                return await _restfulHttp.PostAsync(uri, QIteHolder);
            }
          
            throw new NotImplementedException();
        }

        /// <summary>
        /// Put Complete, QueueItemHolder
        /// </summary>
        /// <param name="QueueResult"></param>
        /// <param name="QIteHolder"></param>
        /// <returns></returns>
        public async Task<bool> PutAsync(int QueueResult, T QIteHolder)
        {

            if (QIteHolder is QItemHolder item)
            {
                string uri = FactoryBase(item.TModel);
                return await _restfulHttp.PutAsync(uri + "/" + QueueResult.ToString(), QIteHolder);
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Put Edit
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public async Task<bool> PutAsync(T Model)
        {
            return await _restfulHttp.PutAsync(FactoryBase(Model), Model);
        }

        ///
        public async Task<bool> DeleteAsync(T Model)
        {
            return await _restfulHttp.DeleteAsync(FactoryBase(Model), Model);
        }


        private string FactoryBase(object Model)
        {
            switch (Model)
            {
                case CSU_Callback csuCallbackModel:
                    return _configuration.GetConnectionString(nameof(CSU_Callback));
                case QueueItem qItemModel:
                    return _configuration.GetConnectionString(nameof(QueueItem));
                case QueueGroup qGroupModel:
                    return _configuration.GetConnectionString(nameof(QueueGroup));
                case QResult qResultModel:
                    return _configuration.GetConnectionString(nameof(QResult));
                case QueueModel queueModel:
                    return _configuration.GetConnectionString(nameof(QueueModel));
                case DomainGroup domainGroupModel:
                    return _configuration.GetConnectionString(nameof(DomainGroup));
                case DomainViewModel domainViewModel:
                    return _configuration.GetConnectionString(nameof(DomainViewModel));
                default:
                    throw new NotImplementedException(
                        string.Format("The object type {0} you have sent through is not supported!", Model.GetType()));
            }
        }

       
    }

}
