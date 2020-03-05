using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
/*  int id { get;} //the constractor will give everybody enother id
       
        #region kind of Const proparties
        string name { get; set; }
        double Heigh { get; set; }
        string ImageUri { get; set; }
        FamilyStatus familyStatus { get; set; }
        DayOfWeek TestDay { get; set; }
        #endregion

      public  List<Meal> Meals { get; set; } //one for day
      public List<Test> Testes { get; set; }//one for week*/
namespace DL.Model
{
    public class UserCollection //BL
    {
        public List<User> Users { get; set; }
        public UserCollection()
        {
            Users = new List<User>();
        }

        public bool AddUser(User newuser)
        {
            if (Users.Exists(u => u.Equals(newuser))) return false;
            Users.Add(newuser);
            return true;
        }
        public void UpdateUserName(int id, string NewName)
        {
            User user = SimpleSearch(id);

            if (user != null)
                user.Name = NewName;
        }
       
        public void RemoveUser(string name,int id)
        {
            var userToremove = (from f in Users
                        where (f.Name == name&&f.id==id)
                        select f).FirstOrDefault<User>();
            Users.Remove(userToremove);
        }

       


        #region simple Search,without cmmnd
        public IEnumerable<User> SimpleSearch(string name)
        {
            var find = (from f in Users
                        where (f.Name == name)
                        select f);
            return find;
        }
        public User SimpleSearch(int id)
        {
            var find = (from f in Users
                        where (f.id == id)
                        select f).FirstOrDefault<User>();
            return find;
        }
        public string SearchNameByID(int id)
        {
            return SimpleSearch(id).Name;
        }
        #endregion
    }




}

