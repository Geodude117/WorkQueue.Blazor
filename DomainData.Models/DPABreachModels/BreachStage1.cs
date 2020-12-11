using System;
using System.Collections.Generic;
using System.Text;

namespace DomainData.Models.DPABreachModels
{
    public class BreachStage1
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ReportedBy { get; set; }
        public DateTime DateReported { get; set; }
        public string PersonResponsibleForBreach { get; set; }
        public DateTime DateOfBreach { get; set; }
        public string CustomerOrEmployeeDataBreach { get; set; }
        public string DMReference { get; set; }
        public string ClientAffected { get; set; }
        public string Site { get; set; }
        public string NumberOfIndividualsAffected { get; set; }
        public string AreaResponsible { get; set; }
        public string BreachDescription { get; set; }
        public string ActionAlreadyTaken { get; set; }
        public string ResolutionOwner { get; set; }

        [Display(Name = "Queue item Identifier")]
        public int? QueueItemID { get; set; }


        public int CompareTo(BreachStage1 obj)
        {
            if (obj.QueueItemID.GetValueOrDefault() != this.QueueItemID.GetValueOrDefault() || obj.Id != this.Id)
            {
                return -1;
            }
            return 1;
        }
    }
}
