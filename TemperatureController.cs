using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TemperatureControllerEvents
{
    public class EventsHandler
    {
        public delegate void Temperaturehandler(string s,int t);
        public event Temperaturehandler tempHandler;
        public string datetime;
        public int temp;
        public List<int> currenttemperatures = new List<int>();
        public EventsHandler()
        {
            this.tempHandler += displayAlert;
            this.tempHandler += storecurrenttemperature;
        }

        public void Temperaturethresholdcheck(int currentTemperature, int min, int max)
        {

            if (currentTemperature < min)
            {
                temp = currentTemperature;
                if (tempHandler != null)
                    datetime = (DateTime.Now).ToString();
                tempHandler.Invoke("Temperature is low than minimum threshold",currentTemperature);
                Console.WriteLine("Current temperature is :{0}\n Date and Time : {1}", currentTemperature, datetime);

                
            }
            if (currentTemperature > max)
            {
                temp = currentTemperature;
                if (tempHandler != null)
                    datetime = (DateTime.Now).ToString();
                tempHandler.Invoke("Temperature is higher than maximum threshold",currentTemperature);
               
                Console.WriteLine("Current temperature is :{0}\n Date and Time : {1}", currentTemperature, datetime);
               
            }

        }
        public void displayAlert(string s,int t)
        {
            Console.WriteLine("\nCaution:{0}  your current temperature is {1} " , s,t);

        }

        public void storecurrenttemperature(string d,int t)
        {
            currenttemperatures.Add(t);
            
        }

        public void TemperatureVariationReport()

        {
            Console.WriteLine("Temperature controller report\n");
            
                var temperatureDetails = from temperatureval in currenttemperatures

                                         select temperatureval;

                Console.WriteLine("Average temperature: {0}\n Max temperature: {1}\n Min temperature: {2}", temperatureDetails.Average(), temperatureDetails.Max(), temperatureDetails.Min());

                Console.ReadKey();

        }


        public class TemperatureController
        {
            public static void Main(string[] args)
            {
                List<int> Currenttemperaturevalues = new List<int>();
                int currenttemperature;
                int i = 0;
                
                EventsHandler eventsHandler = new EventsHandler();
                Console.WriteLine("Enter the minimum threshold values of temperature");
                int min = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the maximum threshold values of temperature");
                int max = Convert.ToInt32(Console.ReadLine());

                while (i < 10)
                {
                    Random random = new Random();

                    currenttemperature = random.Next(min - 5, max + 5);

                    eventsHandler.Temperaturethresholdcheck(currenttemperature, min, max);
                    
                    i++;
                    
                }
                eventsHandler.TemperatureVariationReport();
              
            }

        }
    }
}









   