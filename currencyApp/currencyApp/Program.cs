using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace currencyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AzurepriceCurrencyEntities cs = new AzurepriceCurrencyEntities();

            Console.WriteLine("Enter the Region");
            string Region = Console.ReadLine();
            
            
            StreamReader sr = new StreamReader("E:\\West Europe.txt");
            var line = sr.ReadLine();


           // line = Regex.Split(line, "</option>");

            List<string> str = new List<string>();

            while (line != null)
            {
                
                string pattern = "(<option.*>)(.*)(<\\/option>)";
               
                MatchCollection matches = Regex.Matches(line, pattern);
                
                // string[] value = line.Split(',');
                string[] v = line.Split(':');
                // foreach( string s1)
                
                if (matches.Count > 0)
                    foreach (Match m in matches)
                    {
                        str.Add(m.Groups[2].ToString());

                    }

                line = sr.ReadLine();

            }

            foreach (var item in str)
            {
                AzureAKScurrency aks = new AzureAKScurrency();
                string multiCharString = item.ToString();
                // Split authors separated by a comma followed by space
                var multiArray1 = multiCharString.Split(':');
                var otherdetails = multiArray1[1].Split(',');
                //string Instance = multiArray1[0].ToString();
                aks.Instance = multiArray1[0].Split('>').Last();
                aks.Core = otherdetails[0].Replace("Cores","");
                aks.Ram = otherdetails[1].Replace("RAM","");
                aks.TemporaryStorage = otherdetails[2].Replace("Temporary storage", "");
                aks.PayAsYouGo = (Convert.ToDouble(otherdetails[3].Replace("SAR&nbsp;", "").Replace("/hour", "")) * Convert.ToDouble(730)).ToString();

 

                aks.NodeInstance = multiCharString;
                aks.OneYearReserved = "N/A";
                aks.Region = Region;
                aks.ThreeYearReserved = "N/A";
                aks.ThreeYearReservedWithAzureHybridBenefit = "N/A";
                aks.Currency = "SAR";
                aks.EntryDate = DateTime.Today;

 

                cs.AzureAKScurrencies.Add(aks);
                cs.SaveChanges();
            }

 

            Console.WriteLine("complete");
            Console.ReadKey();

 


        }
    }
}