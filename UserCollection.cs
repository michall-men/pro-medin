using System;
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

namespace DL.Model
{ 
    public class UserCollection : DbContext //BL
    {
        public DbSet<BE.User> DbUsers { get; set; }
        public DbSet<Test> Testes { get; set; } //forigin k to User
        public DbSet<Meal> Meals { get; set; }  //forigin k to User
        public DbSet<Food> FoodSet { get; set; } //forigin k to Meal
        public DbSet<DailyDosage> DbDailyDosage { get; set; }

        public UserCollection()
        {
                 Database.SetInitializer<UserCollection>(new DropCreateDatabaseIfModelChanges<UserCollection>());
        }
        public void ClearAll()
        {
            using (var context = new UserCollection())
            {
                foreach (BE.User i in context.DbUsers)
                    removeuser(i.Name);
                context.SaveChanges();
            }
        }
        #region User Functions
        public string AddUserToDB(BE.User u)
        {
            using (var context = new UserCollection())
            {
                if (!CheckUser(u.Name))
                { context.DbUsers.Add(u); context.SaveChanges(); return "המשתמש נוסף בהצלחה" + "\n" + u.Name + "\n" + u.Password; }
               
            } return " שם משתמש קיים במערכת. בחר שם אחר";
        }
        public void RemoveUser(string UserName)
        {
            using (var context = new UserCollection())
            {
                removeuser(UserName);
                context.SaveChanges();
            }
        }
         void removeuser(string UserName)
        {
            using (var context = new UserCollection())
            {
                var userToremove = (from f in context.DbUsers
                                    where (f.Name == UserName)
                                    select f).FirstOrDefault<BE.User>();
                if (userToremove != null)
                {
                    RemoveMealOfUser(UserName);
                    RemoveTestsOfUser(UserName);
                    RemoveDailyDosageOfUser(UserName);
                    context.DbUsers.Remove(userToremove);
                }
            }
        }
        
        public bool CheckUser(string UserName) // checks if the user already exist
        {
            using (var context = new UserCollection())
            {
                var userToCheck = (from usr in context.DbUsers where (usr.Name == UserName) select usr).Any();

                return userToCheck; // this user already exists or not
            }
        }

        public bool UpdateUser(BE.User uOrigin, BE.User uNew)
        {
            
            using (var context = new UserCollection())
            {
                if (uOrigin.Name != uNew.Name || uOrigin.Password != uNew.Password) return false;
               
                var userToUpdate = context.DbUsers.Find(uOrigin.Name);
                context.Entry(userToUpdate).Property("Heigh").CurrentValue = uNew.Heigh;
                context.Entry(userToUpdate).Property("familyStatus").CurrentValue = uNew.familyStatus;
                context.Entry(userToUpdate).Property("TestDay").CurrentValue = uNew.TestDay;
                context.Entry(userToUpdate).Property("ImageUri").CurrentValue = uNew.ImageUri;

                context.Entry(userToUpdate).Property("Heigh").IsModified = true;
                context.Entry(userToUpdate).Property("familyStatus").IsModified = true;
                context.Entry(userToUpdate).Property("TestDay").IsModified = true;
                context.Entry(userToUpdate).Property("ImageUri").IsModified = true;

                context.SaveChanges();
              
                return true;
            }
        }

        public BE.User SimpleSearch(string name)
        {
            using (var context = new UserCollection())
            {
                try
                {
                    return context.DbUsers.Find(name);
                }
                catch (Exception e)
                { return null; }
            }
        }
        public BE.User LoginSearch(string name,string password)
        {
            using (var context = new UserCollection())
            {
                try
                {
                    BE.User temp = context.DbUsers.Find(name);
                    if (temp.Password != password) throw new Exception();
                    return temp;
                }
                catch (Exception e)
                { return null; }
            }
        }
        public DbEntityEntry<BE.User> SearchAndGetEntity(string name, string password)
        {
            using (var context = new UserCollection())
            {
                try
                {
                    return this.Entry(LoginSearch(name, password));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Not Exist! check tour detailse");
                    return null;
                }
            }
        }
        #endregion
        #region Meals methode
        void RemoveMealOfUser(string UserName)
        {
            using (var context = new UserCollection())
            {
                var MealOfUser = (from f in context.Meals
                                  where (f.UserName == UserName)
                                  select f);
                if (MealOfUser == null) return;
                foreach (Meal m in MealOfUser)
                {
                    RemoveMeal(m);
                }
            }
         
        }
        void RemoveMeal(Meal meal)
        {
            using (var context = new UserCollection())
            {
                foreach (Food m in (from Food x in context.FoodSet where (x.MealId == meal.id) select x))
                {
                    RemoveFoodFromMeal(m);
                }
                context.Meals.Remove(meal);
                //  this.SaveChanges();
            }
        }
        public bool AddMeal(Meal newlanch, string user)
        {
            using (var context = new UserCollection())
            {
                newlanch.UserName = user;
                 
                context.Meals.Add(newlanch);
                context.SaveChanges();
                 return true;
            }
        }
        //sum of nutrients
        public double SumSomethingInMeal(string param,Meal m)
        {
            using (var context = new UserCollection())
            {
                double sum = 0;
                var MealIdOfUser = context.Meals.Where(f => f.UserName == m.UserName && f.id == m.id).FirstOrDefault();

                var FoodsInSpesificMeal = from f in context.FoodSet where f.MealId == MealIdOfUser.id select f;
            
                foreach (Food f in FoodsInSpesificMeal)
                { //hear he make new food
                  
                    sum += this.Entry(context.FoodSet.Find(f.name,f.MealId)).Entity.Nutrient(param);
                    
                }
                context.SaveChanges();

                return sum;
            }
        }
        public double SumOfVitaminkInMeal(Meal m)
        {
            using (var context = new UserCollection())
            {//pasible use at "find"
                DbEntityEntry<Meal> temp = this.Entry(Meals.Find(m.id));
                double k = temp.Entity.VitaminK;
                if (k == -1)
                {
                    k = SumSomethingInMeal("phylloquinone", temp.Entity);
                    temp.Entity.VitaminK = k;
                }

                context.SaveChanges();
                return k;
            }
        }
        public double SumOfIRonInMeal(Meal m)
        {
            return SumSomethingInMeal("Iron", m);
        }
        #endregion
        //All the meal in a day
        public double DailyK(DateTime day,string name)
        {
            using (var context = new UserCollection())
            {
                double sum = 0;
                var t = from x in context.Meals where (x.DayDate == day.Date && x.UserName == name) select x;
                foreach (Meal m in t)
                {
                    sum += SumOfVitaminkInMeal(m);
                }
                return sum;
            }
        }
        public double DailyIron(DateTime day, string name)
        {
            using (var context = new UserCollection())
            {
                double sum = 0;
                var t = from x in context.Meals where (x.DayDate == day.Date && x.UserName == name) select x;
                foreach (Meal m in t)
                {
                   // MessageBox.Show("DailyIron: meal we now id:" + m.id);
                    sum += SumOfIRonInMeal(m);
                }
                return sum;
            }
        }
        public void WeeklyK(DateTime finDay, string name)
        {
            using (var context = new UserCollection())
            {
              /*  double sum = 0;
                var t = from x in context.Meals where (x.DayDate >= day.Date.AddDays(-7)&&x.DayDate <= day.Date && x.UserName == name) select x;
                foreach (Meal m in t)
                {
                    sum += SumOfVitaminkInMeal(m);
                }
                return sum;*/
            }
        }
        #region Test methode
        public string AddTest(Test newtest, string user)
        {
            using (var context = new UserCollection())
            {  //check input
                if (newtest.Inr == 0 || newtest.Dosage == 0) return "Inr or Dosage are zero value";
                if (newtest.Weight == 0) newtest.Weight = TheLastWeigh(user);
                newtest.UserName = user;

                //  var found = (from t in context.Testes where (t.Equals(newtest)) select t);

                context.Testes.Add(newtest);
                context.SaveChanges();

                return "Test added";
            }
        }
        public void RemoveTestsOfUser(string UserName)
        {
            using (var context = new UserCollection())
            {
                var TestOfUser = (from f in context.Testes
                                  where (f.UserName == UserName)
                                  select f);
                if (TestOfUser == null) return;
                foreach (Test t in TestOfUser)
                    context.Testes.Remove(t);
                //  SaveChanges();
            }
        }
        public Test TestInDay(DateTime curretnt_day, string name1)
        {
            using (var context = new UserCollection())
            {

                return (from Test i in context.Testes where (i.DayDate == curretnt_day.Date && i.UserName == name1) select i).ToList().LastOrDefault();
            } 
        }
        public Test LastTestfromDay(DateTime curretnt_day, string name1)
        {
            using (var context = new UserCollection())
            {
                if (TestInDay(curretnt_day, name1) != null) return TestInDay(curretnt_day, name1);
                return (from Test i in context.Testes where (i.DayDate <= curretnt_day.Date && i.UserName == name1) select i).ToList().LastOrDefault();
            }
        }
        public double TheLastWeigh(string user)
        {
            using (var context = new UserCollection())
            {
                try
                {
                    return context.Testes.Where(x => x.UserName == user).ToArray().Last().Weight;
                }
                catch (Exception e) { return 0; }
            }
        }
       
        #endregion
        #region Foods methode
        
        public bool AddFoodToMeal(Food foodToAdd, Meal m)
        {
            using (var context = new UserCollection())
            {
                if (foodToAdd == null) return false;
                foodToAdd.MealId = m.id;
                context.FoodSet.Add(foodToAdd);
                context.SaveChanges(); return true;
            }
        }
        public bool RemoveFoodFromMeal(Food foodTorem)
        {
            using (var context = new UserCollection())
            {
                if (foodTorem == null) return false;
                context.FoodSet.Remove(context.FoodSet.Find(foodTorem.name, foodTorem.MealId));
                context.SaveChanges();
                return true;
            }
        }
        
        public string ListFoodToString()
        {
            using (var context = new UserCollection())
            {
                string s = "";
                if (context.FoodSet != null)
                {
                    foreach (Food f in context.FoodSet)
                        s += f.name + ",";
                }
                return s;
            }
        }
        #endregion
        #region DailyDosage
        public void AddDailyDosage(DailyDosage newDosage, string user)
        { 
            using (var context = new UserCollection())
            {  //check input
                if (newDosage.DayDate < DateTime.Now.Date) { MessageBox.Show("cannot add dosag for early date"); return; }
                newDosage.UserName = user; 
                context.DbDailyDosage.Add(newDosage);
                context.SaveChanges();
                 
            }
        }
        public DailyDosage FaindDailyDosageInDay(DateTime curretnt_day, string name1)//find
        {
            using (var context = new UserCollection())
            {
                return (from DailyDosage i in context.DbDailyDosage where (i.DayDate == curretnt_day.Date && i.UserName == name1) select i).FirstOrDefault();
            }
        }
                  
        public void UpdateOrAddDailyDosage(DailyDosage oldd,double newDosage, Nullable<bool> newChek)
        { 
            using (var context = new UserCollection())
            {   
                var a = DbDailyDosage.Find(oldd.DayDate,oldd.UserName);
                if (a == null) {
                   oldd.Dosage = newDosage; oldd.Pilltaken = newChek;
                    context.AddDailyDosage(oldd, oldd.UserName);
                    return; }
                context.DbDailyDosage.Attach(a);
                if (a.Dosage != newDosage)
                {
                    context.Entry(a).Property("Dosage").CurrentValue = newDosage;
                    context.Entry(a).Property("Dosage").IsModified = true;
                }
                if (a.Pilltaken != newChek)
                {
                    context.Entry(a).Property("Pilltaken").CurrentValue = newChek;
                    context.Entry(a).Property("Pilltaken").IsModified = true;
                }
                context.Entry(a).State = System.Data.Entity.EntityState.Modified;
              
                context.SaveChanges();
            }
        }
        public DbEntityEntry<DailyDosage> DailyDosageInDayGetEntry(DateTime curretnt_day, string name1)
        {
            using (var context = new UserCollection())
            {
                try
                {
                    return context.Entry(FaindDailyDosageInDay(curretnt_day, name1));
                }
                catch(Exception e) { return null; }
            }
        }
        public void RemoveDailyDosageOfUser(string UserName)
        {
            using (var context = new UserCollection())
            {
                var DailyDosageOfuser = (from f in context.DbDailyDosage
                                  where (f.UserName == UserName)
                                  select f);
                if (DailyDosageOfuser == null) return;
                foreach (DailyDosage t in DbDailyDosage)
                    context.DbDailyDosage.Remove(t);
            }
        }

        #endregion
      
    }
}




    

   


