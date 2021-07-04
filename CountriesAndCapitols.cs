using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hangman
{
    class CountriesAndCapitols : Dictionary <string, String>
    {
        public Tuple<string, string> PickCountry()
        {
            var keys = new List<string>(this.Keys);
            int keysLength = keys.Count();
            int randomIndex = new Random().Next(keysLength);
            string country = keys[randomIndex];
            string capitol = this[country];
            return new Tuple<string, string>(capitol, country);
        }
    }
}
