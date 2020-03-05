using System;
using System.Collections.Generic;
using System.Linq;
using DL.Model;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BE;
using OxyPlot;
using System.Text;
using System.Threading.Tasks;
using DL.Command;
using Command;
using OxyPlot.Series;
using OxyPlot.Axes;



namespace DL.ViewModel
{
   public class VMTests: INotifyPropertyChanged
    {
        string name;
        public UserCollection UsersCollection { get; set; }
        public ObservableCollection<Test> TestsObserver { get; set; }

     //   PlotModel MyModel { get; set; }

        public VMTests()
        {
            UsersCollection = new UserCollection();
            TestsObserver = new ObservableCollection<Test>();
        }
        public VMTests(string Nname)
        {
            name = Nname;
            UsersCollection = new UserCollection();
            TestsObserver = new ObservableCollection<Test>(UsersCollection.Testes.Where(x=>x.UserName==name).ToList<Test>());
        }
        public VMTests(string Nname,DateTime UntilHear)
        {
            name = Nname;
            UsersCollection = new UserCollection();
            TestsObserver = new ObservableCollection<Test>(UsersCollection.Testes.Where(x => x.UserName == name&&x.DayDate>= UntilHear).ToList<Test>());
        }
        public List<DataPoint> KbuyAnyDayFrom(DateTime UntilHear)
        {
          
            var v = UsersCollection.Meals.Where(m => m.UserName == name && m.DayDate >= UntilHear);
            List<DataPoint> KeyforEveryDay = new List<DataPoint>();
            for (DateTime dt= UntilHear; dt <= DateTime.Now; dt=dt.AddDays(1))
            {
                KeyforEveryDay.Add(new DataPoint(Convert.ToDouble(dt.Day), UsersCollection.DailyK(dt,name)));
            }

            return KeyforEveryDay;
        }
        public List<DataPoint> KbuyAnyDay()
        {
           
            var v = UsersCollection.Meals.Where(m => m.UserName == name);
            if (!v.Any()) return null;
            List<DataPoint> KeyforEveryDay = new List<DataPoint>();

            for (DateTime dt = v.First().DayDate; dt <= DateTime.Now;dt= dt.AddDays(1))
            {
                KeyforEveryDay.Add(new DataPoint(Convert.ToDouble(dt.Day), UsersCollection.DailyK(dt, name)));
            }

            return KeyforEveryDay;
        }
        public List<DataPoint> IronbuyAnyDay()
        {
             
            var v = UsersCollection.Meals.Where(m => m.UserName == name);
            if (!v.Any()) return null;
            List<DataPoint> KeyforEveryDay = new List<DataPoint>();
            for (DateTime dt = v.First().DayDate; dt <= DateTime.Now; dt = dt.AddDays(1))
            {
                KeyforEveryDay.Add(new DataPoint(Convert.ToDouble(dt.Day), UsersCollection.DailyIron(dt, name)));
            }

            return KeyforEveryDay;
        }
        public List<DataPoint> IronbuyAnyDayFrom(DateTime UntilHear)
        {
           
            var v = UsersCollection.Meals.Where(m => m.UserName == name && m.DayDate >= UntilHear);
            List<DataPoint> KeyforEveryDay = new List<DataPoint>();
            for (DateTime dt = UntilHear; dt <= DateTime.Now; dt = dt.AddDays(1))
            {
                KeyforEveryDay.Add(new DataPoint(Convert.ToDouble(dt.Day), UsersCollection.DailyK(dt, name)));
            }

            return KeyforEveryDay;
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
    public class OxyTestPlotModel
    {
       // public string Title { get; private set; }

        public IList<DataPoint> InrByTime { get; private set; }
        public IList<DataPoint> InrByDay { get; private set; }
        public IList<DataPoint> DosagByTime { get; private set; }
        public IList<DataPoint> DosagByDay { get; private set; }
        public IList<DataPoint> VitaminKBuyDay { get; private set; }
        public IList<DataPoint> IronByDay { get; private set; }

        public OxyTestPlotModel(List<Test> listSorse, List<DataPoint> listKSorse, List<DataPoint> listIronSorse)
        {
           
            //this.Title = "Inr By Time";
            
            #region InrBy..
            this.InrByTime = new List<DataPoint>();
            foreach (var v in listSorse)
                this.InrByTime.Add(new DataPoint(v.id, v.Inr));

            this.InrByDay = new List<DataPoint>();
            foreach (var v in listSorse)
                this.InrByDay.Add(new DataPoint(Convert.ToDouble(v.DayDate.Day), v.Inr));
            #endregion
            
            #region DosagByTime          
            this.DosagByTime = new List<DataPoint>();
            foreach (var v in listSorse)
                this.DosagByTime.Add(new DataPoint(v.id, v.Dosage));

            this.DosagByDay = new List<DataPoint>();
            foreach (var v in listSorse)
                this.DosagByTime.Add(new DataPoint(Convert.ToDouble(v.DayDate.Day), v.Dosage));
            #endregion

            if (listKSorse!=null)
            this.VitaminKBuyDay = listKSorse;
            if (listIronSorse != null)
                this.IronByDay = listIronSorse;
            //new DataPoint(Convert.ToDouble(v.DayDate.Day), v.Inr)
        }



    }
}

