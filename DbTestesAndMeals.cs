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

        /*public List<BE.User> Users { get; set; }*/
        public UserCollection()
        {
            Database.SetInitializer<UserCollection>(new DropCreateDatabaseIfModelChanges<UserCollection>());
        }

        /*  public bool AddUserToList(BE.User newuser)
         {
             if (Users.Exists(u => u.Equals(newuser))) return false;
             Users.Add(newuser);
             return true;
         }*/

        #region User Functions
        public string AddUserToDB(BE.User u)
        {
            if (!CheckUser(u.Name))
            { DbUsers.Add(u); this.SaveChanges(); return "המשתמש נוסף בהצלחה"+"\n"+u.Name+"\n"+u.Password; }
             return " שם משתמש קיים במערכת. בחר שם אחר";
        }
        
        public void RemoveUser(string UserName)
        {
            var userToremove = (from f in DbUsers
                                where (f.Name == UserName)
                                select f).FirstOrDefault<BE.User>();
            DbUsers.Remove(userToremove);
            this.SaveChanges();
        }
        
        public void UpdateUserName(BE.User old, string NewName)
        {
            BE.User user = SimpleSearch(old.Name);

            if (user != null)
                user.Name = NewName;
            this.SaveChanges();
        }

        public bool CheckUser(string UserName) // checks if the user already exist
        {
            var userToCheck = (from usr in DbUsers where (usr.Name == UserName) select usr).Any<BE.User>();

            return userToCheck; // this user already exists or not
        }  

        /*public IEnumerable<BE.User> SimpleSearch(string name)
          {
              var find = (from f in DbUsers
                          where (f.Name == name)
                          select f);
              return find;
          }
       public IEnumerable<BE.User> SimpleSearch(string name,string paswword)
        {
            var find = (from f in Users
                        where (f.Name == name&&f.Password== paswword)
                        select f);
            return find;
        }*/
        public BE.User SimpleSearch(string name)
        {
            try
            {
                return DbUsers.Find(name);
            }
            catch (Exception e)
            { return null; }

        }
        public BE.User SimpleSearch(string name,string password)
        {
            try
            {
                return DbUsers.Find(name,password);
            }
            catch (Exception e)
            { return null; }

        }
        public DbEntityEntry<BE.User> SearchAndGetEntity(string name, string password)
        { 
                try
            { 
                return this.Entry( DbUsers.Find(name, password));
            }
            catch (Exception e)
            { MessageBox.Show("Not Exist! check tour detailse");
                return null; }

        }
        #endregion
    }

}


    

   


