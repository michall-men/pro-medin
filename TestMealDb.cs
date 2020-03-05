/*using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using BE;

namespace BE
{
    public class TestMealDb : DbContext
    {
        public DbSet<Test> Testes { get; set; }
        public DbSet<Meal> Meals { get; set; }

        public TestMealDb()
        {
            Database.SetInitializer<TestMealDb>(new DropCreateDatabaseIfModelChanges<TestMealDb>());
        }

        public bool AddMeal(Meal newlanch)
        {
            // var found = Meals.Where(x => x.Equals(newlanch));
            // if (found == null)//if is exist- is will'nt insert
            //  {
            Meals.Add(newlanch);
            this.SaveChanges();
            return true;
            //  }

            //  return false;
        }

        public double DailyK(DateTime day)
        {
            double sum = 0;
            foreach (Meal m in Meals.Where(x => x.DayDate == day.Date))
            {
                sum += SumOfAllVitamink();
            }
            return sum;
        }
        public double DailyIron(DateTime day)
        {
            double sum = 0;
            var mealsOfDay = (from Meal i in Meals where (i.DayDate == day.Date) select i);
            foreach (Meal m in mealsOfDay)
            {
                sum += m.IronSumOfAllOIron();
            }
            return sum;
        }
        #region Test methode
        public string AddTest(Test newtest)
        {   //check input
            if (newtest.Inr == 0 || newtest.Dosage == 0) return "Inr or Dosage are zero value";
            if (newtest.Weight == 0) newtest.Weight = TheLastWeigh(newtest);
            //chek if it aerly added, and return

            var found = (from t in Testes where (t.Equals(newtest)) select t);

            // if (found != null) //if is exist- is will'nt insert

            // return "insacly same test exist";

            Testes.Add(newtest);
            this.SaveChanges();

            return "Test added";
        }
        public Test TestInDay(DateTime curretnt_day)
        {
                return (from Test i in Testes where (i.DayDate == curretnt_day.Date) select i).FirstOrDefault();
            

        }
      /*  public Double WeekleyDosag(DateTime curretnt_day, int i = 1)
        {
             
            try
            {
                return Testes.Where(x => x.DayDate.DayOfYear == curretnt_day.Date).LastOrDefault().Dosage;
            }
            catch (Exception e)
            {
                i--;
                return WeekleyDosag(curretnt_day.Date.AddDays(i));
            }
        }
    
            double TheLastWeigh(Test test)
            {
                if (Testes == null)
                    return 0;
                return Testes.Last().Weight;
            }
            //להוסיף: בדיקות 7 ימים אחרונים, ויטמין 7 ימים אחרונים
            public IEnumerable<Test> TestsToList()//אפשר להוסיף פרמטר של כמות
            {
                var r = Testes.ToList();
               // MessageBox.Show(r.Any().ToString() + " " + r.ToString());
                return r;
            }

            #endregion

        }

    }*/

