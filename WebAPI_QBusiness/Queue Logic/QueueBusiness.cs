using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CallBack_Model.Interface;
using CallBack_Model.Model;
using WebAPI_QRepository;
using WebAPI_QRepository.Specific_Repo;

namespace WebAPI_QBusiness
{
    public class QueueBusiness : IQueueBusiness
    {
        private IQueueItmRepo _qItemRepo;

        public QueueBusiness(IUnitOfWork unitofwork)
        {
            _qItemRepo = unitofwork.QueueItemRepo;
        }

        public async Task<IEnumerable<QueueItem>> Get_QueueItmsAsync(int QueueGroupID)
        {
            return await _qItemRepo.GetByGroupAsync(QueueGroupID);
        }

        public async Task<QueueItem> Get_QueueItm(int QueueItemID)
        {
            return await _qItemRepo.GetAsync(QueueItemID);
        }

        /// <summary>
        /// Returning a null is valid no items may match the search critera
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<IEnumerable<QueueItem>> Get_QueueItms(SearchParameters search)
        {
            return await _qItemRepo.GetSearchAsync(search);
        }
       
        /// <summary>
        /// Updates Exisiting Entry - AD Group Restricted TL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itmQueueItem"></param>
        /// <returns></returns>
        public async Task<bool> Put_QueueItemComplete(QueueItem itmQueueItem)
        {
            var result = itmQueueItem.CompareTo(await _qItemRepo.GetAsync(itmQueueItem.QueueItemID.Value)) > 0;
            if (!result)
                return result;

            return await _qItemRepo.UpdateAsync(itmQueueItem);
        }

        public async Task<bool> Put_QueueItemEditAsync(QueueItem itmQueueItem)
        {
            var result = itmQueueItem.CompareTo(await _qItemRepo.GetAsync(itmQueueItem.QueueItemID.Value)) > 0;
            if (!result)
                return result;
            return await _qItemRepo.UpdateAsync(itmQueueItem);
        }

        /// <summary>
        /// Not to be referenced by Web Api. For other process to access using
        /// a logical seperation. Create New Entry - Developer Only!
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itmQueueItem"></param>
        /// <returns></returns>
        public async Task<int?> Post_QueueItem(QueueItem itmQueueItem)
        { 
            if (itmQueueItem.CompletedDate != null || !string.IsNullOrEmpty(itmQueueItem.CompletedBy) || itmQueueItem.CreatedDate < DateTime.Today)
                return null;
            return await _qItemRepo.InsertAsync(itmQueueItem);
        }

        public async Task<bool> Delete_QueueItem(int QueueItemID)
        {
            var result = await Get_QueueItm(QueueItemID);
            if (result == null)
                throw new ArgumentNullException("Queue item identifer was not set.");
            return await _qItemRepo.DeleteAsync(QueueItemID);
        }
    }
}
