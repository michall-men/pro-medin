using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;

namespace BE
{
   public class DailyDosage
    {
      private DateTime daydate;
       [Key,Column(Order =0)]
    public DateTime DayDate
        { 
            get { return daydate.Date; }
            set { daydate = value; }
        }
    public Double Dosage { get; set; }
       Nullable<bool> pilltaken { get; set; }
            public Nullable<bool> Pilltaken
             {
                 get {
               if (pilltaken == null || pilltaken == false) return false;
                return pilltaken; }
                 set
                 {
                   //  pilltaken = (value == true) ? value : true;
                pilltaken = (value == null|| value == false) ? value : true;
            }
             }

        //[ForeignKey("OnerUser")]
        [Key, Column(Order = 1)]
        public string UserName { get; set; }
       /// public User OnerUser { get; set; }

        public DailyDosage()
        {
            DayDate = DateTime.Now.Date;
        }
        public DailyDosage(DateTime newDate, string newUsername)
        {
            DayDate = newDate.Date;
            UserName = newUsername;
        }
        public DailyDosage(DateTime newDate,string newUsername, Double nwDosage,Nullable<bool> tock)
        {
            DayDate = newDate.Date;
            UserName = newUsername;
            Dosage = nwDosage;
            Pilltaken = tock;
        }
    }
}
