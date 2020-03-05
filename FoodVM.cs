using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Command;
using DL.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BE;
using Command;

namespace DL.ViewModel
{
  public  class FoodVM : INotifyPropertyChanged
    {
        public FoodCollection CurrentModel { get; set; }
        public ObservableCollection<Food> FoodObserver { get; set; }


        public AddFoodCmd AddFoodCmd { get; set; }
        public SearchCommand Search { get; set; }
         
        //func
        public FoodVM()
        {
            Search = new SearchCommand();
            CurrentModel = new FoodCollection();
            FoodObserver = new ObservableCollection<Food>(CurrentModel.Foods);
            // שיטה ראשונה DI
            //AddFoodCmd = new AddFoodCmd(this);
           

            AddFoodCmd = new AddFoodCmd();
            AddFoodCmd.AddFoodNeeded += AddFoodCmd_AddFoodNeeded;
          //  Search.SearchFood += Search_SearchFood;
            FoodObserver.CollectionChanged += Foods_CollectionChanged;

        }

        private void Search_SearchFood(string obj)
        {
            throw new NotImplementedException();
           

        }

        private void AddFoodCmd_AddFoodNeeded(string obj)
        {
           // FoodObserver.Add(new Food { name = obj  });
        }
        

       public event PropertyChangedEventHandler PropertyChanged;

        private void Foods_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                //הוספה
                CurrentModel.Foods.Add(e.NewItems[0] as Food);
            }
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                //הסרה
                var oldname = (e.OldItems[0] as Food).name;
                CurrentModel.RemoveByName(oldname);

            }

        }

        //simple things
        public Food SimpleSearch(string name)
        {
            return CurrentModel.SimpleSearch(name);     
        }
    
    }
}
