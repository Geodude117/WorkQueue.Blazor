using CallBack_Model.Model;
using DomainData.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WorkQueue.Blazor.Helpers;

namespace WorkQueue.Blazor.Data
{
    public class CSUCallbackService
    {
        private readonly IHttpConnectionFactory<QItemHolder> _httpClientConnection;

        public CSUCallbackService([FromServices] IHttpConnectionFactory<QItemHolder> httpClientConnection)
        {
            _httpClientConnection = httpClientConnection;
        }

        public async Task<bool> PostCSU(DomainViewModel model, string userName)
        {
            var mappedObject = MapCSUObjectObject(model, userName);

            var result = await _httpClientConnection.PostAsync(mappedObject);

            return result;
        }

        public QItemHolder MapCSUObjectObject (DomainViewModel model, string userName)
        {
            QItemHolder qItemHolder = new QItemHolder();
          
            QueueItem queueItem = new QueueItem();
            CSU_Callback csuCallbackItem = new CSU_Callback();

            PropertyInfo[] queueItemproperties = typeof(QueueItem).GetProperties();
            PropertyInfo[] csuCallbackproperties = typeof(CSU_Callback).GetProperties();

            queueItem.CreatedDate = DateTime.Now;
            queueItem.CreatedBy = userName;
            queueItem.QueueID = int.Parse(model.DomainGroup.ExternalReferenceId);
            queueItem.QueueGroupID = model.DomainGroup.Id;

            csuCallbackItem.ReasonForTransfer = " ";

            foreach (var domainItem in model.DomainInfoViewModels)
            {
                foreach (PropertyInfo property in queueItemproperties)
                {
                    List<string> mappings = domainItem.DomainInformation.ObjectMapping.Split(',').ToList();
                    foreach (var item in mappings)
                    {
                        if (item == property.Name)
                        {
                            switch (domainItem.DomainType.TypeName)
                            {
                                case "StringType":
                                    property.SetValue(queueItem, domainItem.TextQuestion.Value);
                                    break;
                                case "BoolType":
                                    property.SetValue(queueItem, domainItem.BoolQuestion.Value);
                                    break;
                                case "IntType":
                                    property.SetValue(queueItem, domainItem.IntQuestion.Value);
                                    break;
                                case "DateTimeType":
                                    property.SetValue(queueItem, domainItem.DateTimeQuestion.Value);
                                    break;
                            }
                        }
                    }
                }
            }

            foreach (var domainItem in model.DomainInfoViewModels)
            {
                foreach (PropertyInfo property in csuCallbackproperties)
                {
                    List<string> mappings = domainItem.DomainInformation.ObjectMapping.Split(',').ToList();
                    foreach (var item in mappings)
                    {
                        if (item == property.Name)
                        {
                            switch (domainItem.DomainType.TypeName)
                            {
                                case "StringType":
                                    property.SetValue(csuCallbackItem, domainItem.TextQuestion.Value);
                                    break;
                                case "BoolType":
                                    property.SetValue(csuCallbackItem, domainItem.BoolQuestion.Value);
                                    break;
                                case "IntType":
                                    property.SetValue(csuCallbackItem, domainItem.IntQuestion.Value);
                                    break;
                                case "DateTimeType":
                                    property.SetValue(csuCallbackItem, domainItem.DateTimeQuestion.Value);
                                    break;
                            }
                        }
                    }
                }
            }

            qItemHolder.queueItem = queueItem;
            qItemHolder.TModel = csuCallbackItem;

            return qItemHolder;
        }
    }
}
