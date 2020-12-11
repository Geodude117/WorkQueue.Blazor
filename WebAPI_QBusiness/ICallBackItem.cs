using CallBack_Model.Interface;
using CallBack_Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_QBusiness
{
    public interface ICallBackItem<T> where T : class
    {
        Task<bool> PutItemCompleted(int QueueResultID, QueueItem i, T item, string noteWebApiCon);
    }
}
