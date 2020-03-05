using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class SimpleDay
    {
        private DateTime daydate;
        public DateTime DayDate
        {
            get {  return daydate.Date;  }
            set { daydate = value; }
        }
        

        public SimpleDay()
        {
            DayDate = DateTime.Now;
          
        }
        public SimpleDay(DateTime D)
        {
            DayDate = D;
        }

        #region Date Methodes
        public DateTime IDaysBefore(double i)
        {
            DateTime week_before = DayDate.AddDays(-1*i);

            return week_before.Date;
        }
        public DateTime InAWeek()
        {
            DateTime week_after = DayDate.AddDays(7);
            return week_after.Date;
        }
        #endregion
       public bool Equlse(DateTime d)
        {
            if (DayDate.Equals(d))
                return true;
            return false;
        }
      
        }
}
