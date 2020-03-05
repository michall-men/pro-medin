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
    #region Dbcontext
   
    #endregion
    #region Enums
    public enum TimeOfMeal
    {
        
        בוקר,
        צהריים,
        ערב
    }
    public enum Feelings
    {
        טוב,
        עייפות,
        כאב 
       ,רעב,
        וירוס, חיידק
    }
    public enum Plases
    {
        בית,
        עבודה,
        בילוי,
        אשפוז,

    }
    #endregion
   public  class  Meal :SimpleDay
    {
        static int ID=0;
         [Key, Column(Order = 0)]
        public int id { get; set; }
          [ForeignKey("OnerUser")]
        public string UserName { get; set; }
        public User OnerUser { get; set; }
       
        public TimeOfMeal MealTime { get; set; }
        public Feelings Feeling { get; set; }
        public  Plases Plase { get; set; }

        public double VitaminK { get; set; }  
     //ויטמין כללי בכל הארוחה?
    #region constractors
    public Meal():base()
        {
            id = ++ID;
            VitaminK = -1;
         }
        public Meal(DateTime d,List<Food> lun, TimeOfMeal timeOfmeal,
        Feelings feeling,
        Plases plase) : base(d)
        {
            id = ++ID;
            MealTime = timeOfmeal;
            Feeling = feeling;
            Plase = plase;
          //  List<Food> Lunch = lun;
        }
   
        public Meal(List<Food> lun, TimeOfMeal timeOfmeal,
        Feelings feeling ,
        Plases plase)
        {
            id = ++ID;
            MealTime = timeOfmeal;
            Feeling = feeling;
            Plase = plase;
           // List<Food> Lunch = lun;
        }
        #endregion
        
       

        #region Standart obj methode
     /*   public  bool Equals(Meal eqLanch)
        {
            if (!DayDate.Equals(eqLanch.DayDate) ||
                !Eat.Equals(eqLanch.Eat) ||
                !MealTime.Equals(eqLanch.MealTime) ||
                !Feeling.Equals(eqLanch.Feeling) ||
                !Plase.Equals(eqLanch.Plase))
                return false;
            return true;   
        }*/
        public override string ToString() //override
        {
            string s="meal: "+ MealTime+", "+Feeling + ", " + Plase + " food:"+"\n";
          
            return s;
        }
        #endregion

    }
}
