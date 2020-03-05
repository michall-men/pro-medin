using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Threading;

using System.ComponentModel.DataAnnotations;
namespace BE
{
    public enum TypeOfFood
    {
        protein,
        fruits,
        vegetable,
        general
    }
    // public static string[] Group = { "Fruits and Fruit Juices", "Finfish and Shellfish Products" };

    public class Food
    {
         
        [Key, Column(Order = 0)]
        public string name { get; set; }
        [Key,Column(Order = 1)]
       public int MealId { get; set; }
  
      
        public TypeOfFood Kind = new TypeOfFood(); //"type" my confuse with c# proparty //{ get; set; }
        public string ImageUri { get; set; }

        //thingh to the server-ask, conculeded in the ctor
       public string Measure = "slice";//מידה
        //is public, but not use like public
       public string keyFood { get; set; }//מספר מותג
       public string group = "Branded Food Products Database";
       public string More { get; set; }//עוד פרטים
        
        #region ctor
        public Food() {
           
        }

        public Food(Food p) //cpy ctr
        {
            ImageUri = p.ImageUri;
            keyFood = p.keyFood;
            Measure = p.Measure;
            More = p.More;
            name = p.name;

            MealId = p.MealId;
        }
        public Food(string Cname, string CImageUri, TypeOfFood CKind, string Cmore = " ", string groupstring = "Branded Food Products Database") // new Food {name="avocado",ImageUri="/images/avocado.png",Kind=TypeOfFood.fruits},
        {
            name = Cname;
            ImageUri = CImageUri;
            Kind = CKind;

            if (Kind == TypeOfFood.fruits) group = "Fruits and Fruit Juices";
            if (Kind == TypeOfFood.vegetable) group = "Vegaatebles and Vegaatebles Products";
            if (Kind == TypeOfFood.fruits || Kind == TypeOfFood.vegetable)
            {
                Measure = "medium";
                More = "raw"; // +" , "+Cmore;
            }
            else
            {
                group = groupstring;
                More = Cmore;
            }
        }
        #endregion
        #region בקשת שירות 
        public string KeyFood()
        {
            if (this.keyFood != null) return this.keyFood;
             switch (name)
            {
                case "cheese": return "01270";
                case "soymilk": return "16120";
                case "broccoli": return "11090";
                case "wine": return "14096";
                case "olives": return "09193";
                //  case "grapes": return "09132/09131"; salmon-15081
               case "salmon": return "15081";
                case "avocado": return "09037";
            }
            string foodq = More + " , " + name;

            #region part one- find the number key of food
            Stream dataStream;
            WebResponse response;
            try
            {
                string AsckFoodKey = @"https://api.nal.usda.gov/ndb/search/?format=JSON&q=" + foodq + "&sort=n&max=25&offset=0&api_key=xjkHskmKWXGFzYR2O2czyY5jWhmeaD7puagyHI5L";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AsckFoodKey);
                response = request.GetResponse();
                dataStream = response.GetResponseStream();
            }
            catch (Exception e) { return null; }
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd(); //Json format

            if (responsereader.Contains("error")) { throw new Exception(); }
           
            JObject responsJ = JObject.Parse(responsereader);
            
            IEnumerable<JToken> responsJ2 = responsJ["list"]["item"].Where(temp => temp["group"].ToString().Contains(group)).Select(temp => temp);
            if (responsJ2 == null || !responsJ2.Any())
            {
                responsJ2 = responsJ["list"]["item"];
            }
            
            responsJ2 = responsJ2.FirstOrDefault<JToken>()["ndbno"];
            
            this.keyFood = responsJ2.ToString();
            //i save that in a proparty, but the usda can chage it!
            response.Close();
            #endregion
          
            return keyFood;
        }

        public double Nutrient(string nutrient_name) //מעטפת- פותחת תהליכון חדש
        {
            double answer = 0;
           var thread = new Thread(() =>
                   {
                answer = PrivateNutrient(nutrient_name); }); // Publish the return value
            thread.Start();
            thread.Join();
            return answer;
        }
        double PrivateNutrient(string nutrient_name)//spesific nutrient
        {
            if (keyFood == "error") return 0;// TO FIX ERRORS!!!!!!!!!
             #region part two- ask the components of food by his key
             string AsckFoodComponents = @" https://api.nal.usda.gov/ndb/V2/reports?ndbno=" + KeyFood() + "&type=b&format=JSON&api_key=xjkHskmKWXGFzYR2O2czyY5jWhmeaD7puagyHI5L";
         
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AsckFoodComponents);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();//Json format
            JObject responsJ = JObject.Parse(responsereader);
             
            IEnumerable<JToken> responsJNutr =
                    responsJ["foods"][0]["food"]["nutrients"].Where(temp =>
                    temp["name"].ToString().Contains(nutrient_name)).Select(temp =>
                    temp["measures"]);
          //  if (!responsJNutr.Any()) return 0;

            responsJNutr = responsJNutr.First<JToken>();
             IEnumerable<JToken> responsJNutrMesure =
                responsJNutr.Where(temp =>
                temp["label"].ToString().Contains(Measure) || temp["label"].ToString().Contains(Measure.ToUpper())).Select(temp =>
                 temp["value"]).FirstOrDefault();

            if (responsJNutrMesure == null || !responsJNutrMesure.Any())
            {
                Measure = "cup";
                responsJNutrMesure =
                 responsJNutr.Where(temp =>
                  temp["label"].ToString().Contains(Measure) || temp["label"].ToString().Contains(Measure.ToUpper())).Select(temp =>
                temp["value"]).FirstOrDefault();
            }
            if (responsJNutrMesure == null || !responsJNutrMesure.Any())
            {
                Measure = responsJNutr.First()["label"].ToString();
                responsJNutrMesure =
                    responsJNutr.Select(temp => temp["value"]).FirstOrDefault();
            }
            response.Close();
            #endregion
            return double.Parse(responsJNutrMesure.ToString());
        }
      
        #region All
        public IDictionary<string, double> AllNutrients() //מעטפת- פותחת תהליכון חדש
        {
            IDictionary<string, double> b = null;
            var thread = new Thread(() =>
            { b = PrivatAllNutrients(); }); // Publish the return value
            thread.Start();
            thread.Join();
            return b;
        }
               IDictionary<string, double> PrivatAllNutrients()//All the nutrients of the food
        {
            Dictionary<string, double> nutrients = new Dictionary<string, double>();//new Dictionary<string, double>();
            string nutName = "";
         

            string AsckFoodComponents = @" https://api.nal.usda.gov/ndb/V2/reports?ndbno=" + KeyFood() + "&type=b&format=JSON&api_key=xjkHskmKWXGFzYR2O2czyY5jWhmeaD7puagyHI5L";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AsckFoodComponents);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();//Json format

            JObject responsJ = JObject.Parse(responsereader);

            IEnumerable<JToken> responsJNutr =
                    responsJ["foods"][0]["food"]["nutrients"];
            foreach (JToken i in responsJNutr)
            {
                nutName = i["name"].ToString();
              
                IEnumerable<JToken> responsJNutrMesure = i["measures"].Where(temp =>
                temp["label"].ToString().Contains(Measure) || temp["label"].ToString().Contains(Measure.ToUpper())).Select(temp =>
                temp["value"]).FirstOrDefault();

                if (responsJNutrMesure == null || !responsJNutrMesure.Any())
                {
                    Measure = i["measures"].First()["label"].ToString() ;
                    responsJNutrMesure = i["measures"].First()["value"];
                   // i["measures"].Where(temp =>
                   // temp["label"].ToString().Contains(Measure) || temp["label"].ToString().Contains(Measure.ToUpper())).Select(temp =>
                   // temp["value"]).FirstOrDefault();
                }
                
                if (responsJNutrMesure == null || !responsJNutrMesure.Any())
                {
                    responsJNutrMesure =
                    responsJNutr.Select(temp => temp["value"]).FirstOrDefault();
                }
                nutrients.Add(nutName, double.Parse(responsJNutrMesure.ToString()));// responsJNutrMesure containts {}. what to do?
            }
            response.Close();

            IDictionary<string, double> h = nutrients;
            //  return nutrients;
            return h;
        }
        #endregion
        #region  not in used
        public IDictionary<string, double> Nutrients(params string[] ps)//spesifiic many nurients
        {
            Dictionary<string, double> nutrients =new Dictionary <string, double>();
            foreach (string s in ps)
            {
                nutrients.Add(s, Nutrient(s));
            }
          //  IDictionary<string, double> h = nutrients;
            return nutrients;
        }//phylloquinone= vitamiv k
       
        public IDictionary<string, double> KandIron()
        {
            return Nutrients("phylloquinone", "Iron");
        }
        #endregion
        #endregion

        public bool Equals(Food p)
        {
            if (name != p.name || Kind != p.Kind || ImageUri != p.ImageUri) //||id!=p.id)
                return false;
            return true;
        }
        public override string ToString() 
        {
            return name;
        }

        #region Operators
        /*  public static bool operator==(Food f,Food p)
          {  
              if (f.name != p.name || f.Kind != p.Kind || f.ImageUri != p.ImageUri)
                  return false;
              return true;
          }
          public static bool operator !=(Food f, Food p)
          { if (f == p) return false;
              return true;
          }*/
        
        
        
        #endregion

    }

}

