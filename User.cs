using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BE
{
    public enum FamilyStatus
    {
        single, married, somthing
    }
    public enum Gender
    {
        גבר, אשה
    }
   
    public class User: DbContext
    {
        // static int ID = 0;
        // public int id { get; } //the constructor will give everybody another id
       
        #region kind of Const properties
        [Key,Column(Order=0)]
        public string Name { get; set; }
       [ Column(Order= 1)]
        public string Password { get; set; }
        [Column(Order = 2)]
        public Double Heigh { get; set; }
        [Column(Order = 3)]
       public string ImageUri { get; set; }
        [Column(Order = 4)]
        public FamilyStatus familyStatus { get; set; }
        [Column(Order = 5)]
        public DayOfWeek TestDay { get; set; }
        [Column(Order = 6)]
        public Gender gender { get; set; }
        #endregion
        [Column(Order = 7)]
        public Double StartOfRange { get; set; } //range of inr. [0]=start, [1]=fin
        [Column(Order = 8)]
        public Double EndtOfRange { get; set; }
        [Column(Order = 9)]
        public Int32 YearOfbirth;
     
         
        #region ctors
        public User()
        {

        }
        public User(string nwname, Int32 year, double nwheigh, double newrange, double newR2, FamilyStatus nwfamilyStat, DayOfWeek nwdayOfWeek)
        {
            YearOfbirth = year;
            StartOfRange = newrange;
            EndtOfRange = newR2;
            Name = nwname; Heigh = nwheigh;
            familyStatus = nwfamilyStat; TestDay = nwdayOfWeek;
        }

        #endregion

        public bool UpdateImage(string image)
        {
          //  if (Uri.IsWellFormedUriString(@image, UriKind.RelativeOrAbsolute))
          //  {
                ImageUri = @image;
                return true;
           // }
           // return false;
        }

        public bool Equals(User p)
        {
            if (Name != p.Name || Password != p.Password)
                return false;
            return true;
        }
        public override string ToString() //jast thigh yoy can't chage
        {
            var age = 2019 - YearOfbirth;
            return "name: " + Name + "\n" + "birth at: " + YearOfbirth + "\n" + "gender: " + gender + "\n" + "Int range: " + this.StartOfRange + " to " + EndtOfRange ;
        }
    }
}


