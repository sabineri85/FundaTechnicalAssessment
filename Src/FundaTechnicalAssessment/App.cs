using FundaTechnicalAssessment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundaTechnicalAssessment
{
    public class App(IPropertiesService propertiesService)
    {
        public async Task RunAsync()
        {
            var isValid = false;
            Console.WriteLine("Please type the name of a city in the Netherlands.");
            ConsoleKeyInfo keyReadCity = System.Console.ReadKey();
            ConsoleKeyInfo keyReadGarden = System.Console.ReadKey();
            do
            {
                Console.WriteLine("With garden?: type y or n");
                keyReadGarden = System.Console.ReadKey();

            } while (!isValid);
            

            var hasGarden = true ? keyReadGarden.ToString().Equals("y") : keyReadGarden.ToString().Equals("n");

            var propertiesResult = await propertiesService.GetRankPropertiesByAgentAsync(keyReadCity.ToString(), hasGarden);
        }
    }

}
