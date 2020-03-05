using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BE
{ 
   public  class Test:SimpleDay
    {
        static int ID = 0;
        [Key]
        public int id { get; set; }

        [ForeignKey("OnerUser")] public string UserName { get; set; }
        public User OnerUser { get; set; }

        public Double Inr { get ; set; } //valu of the blood chek
       
        public Double Dosage { get; set; } //valu of the weekly-dosage
         
        public  Double Weight { get; set; } //I still thinking if it soppose be hear,and chage,
                           //or in user and be const
        #region constractors
        public  Test()
        {
            id = ++ID;
        }
        public Test(double inr, double dosage, double weight) :base()
        {
            id = ++ID;
            Inr = inr;
            Dosage = dosage;
            Weight = weight;
        }
        public Test(DateTime d, double inr, double dosage, double weight):base(d)
        {
            id = ++ID;
            Inr = inr;
            Dosage = dosage;
            Weight = weight;
        }
        public Test(double inr, double dosage):base()
        {
            id = ++ID;
            Inr = inr;
            Dosage = dosage;
        }
        #endregion
        #region Standart obj methode
        public bool Equals(Test eqTest)
        {
            if (!DayDate.Equals(eqTest.DayDate) ||
                !(Inr == eqTest.Inr) ||
                !(Dosage== eqTest.Dosage) ||
                !(Weight==eqTest.Weight ))
               return false;
            return true;
        }
        public bool DayEquals(Test eqTest)
        {
            if (!DayDate.Equals(eqTest.DayDate))
                return false;
            return true;
        }
        public override string ToString()
        {
            return "Inr: " + Inr + " Dosag: " + Dosage + " Whight: " + Weight;
        }
        #endregion
    }
}
