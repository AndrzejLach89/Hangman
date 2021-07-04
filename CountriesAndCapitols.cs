using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hangman
{
    class CountriesAndCapitols : Dictionary <string, String>
    {
        //public Tuple<string, string> PickCountry()
        public string[] PickCountry()
        {
            var keys = new List<string>(this.Keys);
            int keysLength = keys.Count();
            int randomIndex = new Random().Next(keysLength-1);
            string country = keys[randomIndex];
            string capitol = this[country];
            //return new Tuple<string, string>(capitol, country);
            string[] output = new string[2];
            output[0] = country;
            output[1] = capitol;
            return output;
        }
    }
}
