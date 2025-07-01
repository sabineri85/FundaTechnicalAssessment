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
        private string keyReadGarden;
        public async Task RunAsync()
        {            
            var isValid = false;
            Console.WriteLine("Please type the name of a city in the Netherlands. If no value entered then Amsterdam will be used.");
            string lineReadCity = Console.ReadLine();
            
            var lineReadGardenString = string.Empty;
            
            do
            {
                Console.WriteLine("With garden?: type y or n");
                keyReadGarden = Console.ReadLine();
                if (keyReadGarden is null)
                    continue;
                lineReadGardenString = keyReadGarden.ToString().ToLower();

                if(lineReadGardenString.Equals("y") || lineReadGardenString.Equals("n"))
                {
                    isValid = true;
                }

            } while (!isValid);
            

            var hasGarden = true ? lineReadGardenString.Equals("y") : keyReadGarden.Equals("n");
            if (String.IsNullOrEmpty(lineReadCity))
                lineReadCity = "Amsterdam";
            var propertiesResult = await propertiesService.GetRankPropertiesByAgentAsync(lineReadCity.ToString(), hasGarden);
            Console.WriteLine("");
        }
    }

}
