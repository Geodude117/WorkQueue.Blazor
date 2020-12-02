using CallBack_Model.Model;
using DomainData.Models.QuestionModels;
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
        private readonly IHttpConnectionFactory<CSU_Callback> _httpClientConnection2;

        public CSUCallbackService([FromServices] IHttpConnectionFactory<QItemHolder> httpClientConnection, [FromServices] IHttpConnectionFactory<CSU_Callback> httpClientConnection2)
        {
            _httpClientConnection = httpClientConnection;
            _httpClientConnection2 = httpClientConnection2;
        }

        public async Task<bool> PostCSU(IDomainViewModel model, string userName)
        {
            try
            {
                CustomMapper mapper = new CustomMapper();

                var queueItem = mapper.Map(model);
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }

            var mappedObject = ApplyMap(model, userName);

            var result = await _httpClientConnection.PostAsync(mappedObject);

            return result;
        }

        public async Task<CSU_Callback> Get(int Id)
        {
            var result = await _httpClientConnection2.GetAsync(Id);
            return result;
        }

        public QItemHolder ApplyMap (IDomainViewModel model, string userName)
        {
            QItemHolder qItemHolder = new QItemHolder();
          
            QueueItem queueItem = new QueueItem();
            CSU_Callback csuCallbackItem = new CSU_Callback();

            queueItem.CreatedDate = DateTime.Now;
            queueItem.CreatedBy = userName;
            queueItem.QueueID = int.Parse(model.DomainGroup.ExternalReferenceId);
            queueItem.QueueGroupID = model.DomainGroup.Id;

            csuCallbackItem.ReasonForTransfer = " ";

            queueItem = MapQueueItem(model.DomainInfoViewModels, queueItem);

            //csuCallbackItem = MapCSUCallbackItem(model.DomainInfoViewModels, csuCallbackItem);

            qItemHolder.queueItem = queueItem;
            qItemHolder.TModel = csuCallbackItem;

            return qItemHolder;
        }


        public QueueItem MapQueueItem (List<IDomainInfoViewModels> list, QueueItem queueItem)
        {
            PropertyInfo[] queueItemproperties = typeof(QueueItem).GetProperties();
            foreach (var domainItem in list)
            {
                foreach (PropertyInfo property in queueItemproperties)
                {
                    if (domainItem.DomainInformation.PropertyMapping == property.Name)
                    {
                        switch (domainItem.DomainType.TypeName)
                        {
                            case "StringType":
                                TextQuestion txtQuestion = (TextQuestion)domainItem.Question;
                                property.SetValue(queueItem, txtQuestion.Value);
                                break;
                            case "BoolType":
                                BoolQuestion boolQuestion = (BoolQuestion)domainItem.Question;
                                property.SetValue(queueItem, boolQuestion.Value);
                                break;
                            case "IntType":
                                IntQuestion intQuestion = (IntQuestion)domainItem.Question;
                                property.SetValue(queueItem, intQuestion.Value);
                                break;
                            case "DateTimeType":
                                DateTimeQuestion dateTimeQuestion = (DateTimeQuestion)domainItem.Question;
                                property.SetValue(queueItem, dateTimeQuestion.Value);
                                break;
                        }
                    }
                }
            }

            return queueItem;
        }
        //public CSU_Callback MapCSUCallbackItem(List<IDomainInfoViewModels> list, CSU_Callback csuCallbackItem)
        //{
        //    PropertyInfo[] csuCallbackproperties = typeof(CSU_Callback).GetProperties();

        //    foreach (var domainItem in list)
        //    {
        //        foreach (PropertyInfo property in csuCallbackproperties)
        //        {
        //            List<string> mappings = domainItem.DomainInformation.PropertyMapping.Split(',').ToList();
        //            foreach (var item in mappings)
        //            {
        //                if (item == property.Name)
        //                {
        //                    switch (domainItem.DomainType.TypeName)
        //                    {
        //                        case "StringType":
        //                            TextQuestion txtQuestion = (TextQuestion)domainItem.Question;
        //                            property.SetValue(csuCallbackItem, txtQuestion.Value);
        //                            break;
        //                        case "BoolType":
        //                            BoolQuestion boolQuestion = (BoolQuestion)domainItem.Question;
        //                            property.SetValue(csuCallbackItem, boolQuestion.Value);
        //                            break;
        //                        case "IntType":
        //                            IntQuestion intQuestion = (IntQuestion)domainItem.Question;
        //                            property.SetValue(csuCallbackItem, intQuestion.Value);
        //                            break;
        //                        case "DateTimeType":
        //                            DateTimeQuestion dateTimeQuestion = (DateTimeQuestion)domainItem.Question;
        //                            property.SetValue(csuCallbackItem, dateTimeQuestion.Value);
        //                            break;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return csuCallbackItem;
        //}

    }
}
