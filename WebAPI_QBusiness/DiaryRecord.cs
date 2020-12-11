using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_QBusiness
{
    public class DiaryRecord
    {

        [Range(1, 999999999, ErrorMessage = "The account number must be between 1 and 999,999,999")]
        [Required(ErrorMessage = "The account number is required")]
        public int AccountNumber { get; set; }

        [MaxLength(17, ErrorMessage = "The Active Directory and Agent Name cannot exceed 17 characters.")]
        [Required(ErrorMessage = "The agent name cannot be empty.")]
        public string AgentName { get; set; }

        [MaxLength(3, ErrorMessage = "The Diary Code has to be 3 characters in length.")]
        [MinLength(3, ErrorMessage = "The Diary Code has to be 3 characters in length.")]
        [Required(ErrorMessage = "The Diary Code cannot be empty.")]
        public string DiaryCode { get; set; }

        [MaxLength(40, ErrorMessage = "The Diary Description cannot exceed 40 characters.")]
        [Required(ErrorMessage = "The Diary Description cannot be empty.")]
        public string DiaryDescription { get; set; }

        [Range(typeof(DateTime), "01/01/1990", "01/01/2100", ErrorMessage =
            "Value for date must be between {1} and {2}")]
        [Required(ErrorMessage = "The Posted Date Time cannot be empty.")]
        public DateTime PostedDateTime { get; set; }

        [MaxLength(4000, ErrorMessage =
            "Character limit has exceeded 4000, diary note character total has to be equal to or below 4000 characters")]
        public string AgentNotes { get; set; }
    }
}