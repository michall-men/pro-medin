using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DL.Model;

namespace DL.ViewModel
{
    class VMDictonary 
    {
        public IDictionary<string,double> CurrentDictionnary { get; set; }
        public ObservableDictionary<string,double> NutrientsObserver { get; set; }
    

        //func
        public VMDictonary(IDictionary<string, double> CurrentDictionnary1)
        {
            // Search = new SearchCommand();
            CurrentDictionnary = CurrentDictionnary1;
            NutrientsObserver = new ObservableDictionary<string, double>(CurrentDictionnary);
            // שיטה ראשונה DI
            //AddFoodCmd = new AddFoodCmd(this);

         //   AddFoodCmd = new AddFoodCmd();
         //   AddFoodCmd.AddFoodNeeded += AddFoodCmd_AddFoodNeeded;
            //  Search.SearchFood += Search_SearchFood;
        //    FoodObserver.CollectionChanged += Foods_CollectionChanged;

        }
    }
}
