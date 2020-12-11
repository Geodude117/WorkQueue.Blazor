using System;
using System.Collections.Generic;
using System.Text;

namespace DomainData.Models.DPABreachModels
{
    public class BreachStage2
    {
        public int Id { get; set; }

        public string HRDirectorAware { get; set; }


        public string BreachDescription { get; set; }


        public string ActionAlreadyTaken { get; set; }


        public string AdditionalMitigatingActionRequired { get; set; }


        public int NumberOfDataSubjectsAffected { get; set; }


        public bool IndividualsAware { get; set; }


        public string CategoriesOfDataBreaches { get; set; }


        public string PotentialConsequences { get; set; }


        public string RiskRating { get; set; }

        //Options 
        // - Isolated DPA breach
        // - Security Event

        public string Type { get; set; }


        public string RootCause { get; set; }

        // Only for Medium/ High rating

        public string DPORecommendation { get; set; }


        public bool NonDPA { get; set; }


        public bool ICOReportable { get; set; }


        public bool ClientReportable { get; set; }


        public bool DataSubjectReportable { get; set; }

        [Display(Name = "Queue item Identifier")]
        public int? QueueItemID { get; set; }
    }
