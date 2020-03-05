using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


using BE;

namespace DL.Model
{
   public class FoodCollection
    {

        public List<Food> Foods { get; set; }

        public FoodCollection()
        {
            Foods = new List<Food>
            {
                new Food ("avocado", "/images/avocado.png", TypeOfFood.fruits),
                new Food ( "apple", "/images/apple.png" , TypeOfFood.fruits),
                new Food (  "watermelon",  "/images/watermelon.png",  TypeOfFood.fruits),
                new Food (  "blueberries",  "/images/blueberries.png",  TypeOfFood.fruits),
                new Food (  "grapes",  "/images/grapes.png",  TypeOfFood.fruits,"red or green"),//error
                new Food (  "lime",  "/images/lime.png",  TypeOfFood.fruits),
                new Food ("olives",  "/images/olives.png",  TypeOfFood.fruits,"canned"),//error, thers no vitamin k


                new Food (  "lettuce",  "/images/salad.png" ,  TypeOfFood.vegetable),
                new Food (  "cabbage savoy",  "/images/cabbage.png" ,  TypeOfFood.vegetable,"savoy"),
                new Food (  "broccoli",  "/images/broccoli.png" ,  TypeOfFood.vegetable),
                  new Food (  "radish",  "/images/radish.png" ,  TypeOfFood.vegetable),
                  new Food (  "ginger-root",  "/images/ginger.png",  TypeOfFood.vegetable),

                new Food (  "salmon",  "/images/fish.png",  TypeOfFood.protein,"wild, raw","fish"),
                new Food (  "cheese",  "/images/cheese.png" ,  TypeOfFood.protein,"cheddar","Dairy and Egg Products"),
                  new Food (  "soymilk",  "/images/soya-milk.png" ,  TypeOfFood.protein),
                   new Food (  "wine",  "/images/wine.png" ,  TypeOfFood.general,"red","Alcoholic beverage,"),

            };
        }

        public void UpdateFoodName(string name, string NewName)
        {
            var Food = (from f in Foods
                          where f.name == NewName
                        select f).FirstOrDefault<Food>();

            if (Food != null)
                Food.name = NewName;
        }
        public void UpdateFoodImage(string name, string NewUri)
        {
            var Food = (from f in Foods
                        where f.name == name
                        select f).FirstOrDefault<Food>();

            if (Food != null)
                Food.ImageUri = NewUri;
        }
       
        public void RemoveByName(string oldId)
        {
            var Food = (from f in Foods
                          where f.name == oldId
                          select f).FirstOrDefault<Food>();

            Foods.Remove(Food);
        }
        public void RemoveByPic(string pic)
        {
            var Food = (from f in Foods
                        where f.ImageUri == pic
                        select f).FirstOrDefault<Food>(); 
            
                        

            Foods.Remove(Food);
        }

        //simple Search,without cmmnd
        public Food SimpleSearch(string name)
        {
            var find = (from f in Foods
                        where ( f.name == name)
                        select f).FirstOrDefault<Food>();
            
            return find;
        }
        
    }
}
