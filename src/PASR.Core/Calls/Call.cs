using Abp.Domain.Entities;
using PASR.Authorization.Users;
using PASR.Leads;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASR.Calls
{
    public class Call : Entity<int>
    {
        protected Call() { }
        public Call(
            User user,
            Lead lead,
            DateTime callStartDateTime,
            DateTime callEndDateTime,
            CallResult callResult,
            ResultReason resultReason)
        {
            User = user;
            Lead = lead;
            CallStartDateTime = callStartDateTime;
            CallEndDateTime = callEndDateTime;
            CallResult = callResult;
            ResultReason = resultReason;
        }

        [Required]
        public User User { get; set; }

        [Required]
        public Lead Lead { get; set; }

        [Required]
        public DateTime CallStartDateTime { get; set; }

        [Required]
        public DateTime CallEndDateTime { get; set; }
        
        [Required]
        [EnumDataType(typeof(CallResult))]
        public CallResult CallResult { get; set; }

        [Required]
        [EnumDataType(typeof(ResultReason))]
        public ResultReason ResultReason { get; set; }

        public string CallNotes { get; set; }

        public bool Intersted { get; set; }

        public TimeSpan GetTimeSpent() {

            return (CallEndDateTime - CallStartDateTime);
        }
    }

    public enum CallResult
    {
        NotSignificant,
        Significant,
        ScheduledMeeting
    }

    public enum ResultReason
    {
        Absence,
        Ocuppied,
        NotProspectable,
        Brand,
        Indication,
        Necessity,
        ConnectionOrTechnical,
        Concurrency
    }

}
