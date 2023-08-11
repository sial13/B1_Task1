using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B1_Task1.Controllers
{
    public class RandomString
    {
        Random random = new Random();

        public string RandomDate()
        {
            DateTime today = DateTime.Today;
            DateTime fiveYearsAgo = today.AddYears(-5);
            TimeSpan range = today - fiveYearsAgo;

            int randomDays = random.Next(range.Days);

            DateTime randomDate = fiveYearsAgo.AddDays(randomDays);

            return randomDate.ToShortDateString();
        }

        public string RandomLetter(string allowedChars)
        {
            char[] randomChars = new char[10];

            for (int i = 0; i < 10; i++)
            {
                randomChars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(randomChars);
        }

        public int RandomInt()
        {
            int min = 1;
            int max = 100000000;

            int randomNumber = random.Next(min, max + 1);

            return randomNumber;
        }

        public string RandomFloat()
        {
            double min = 1.0;
            double max = 20.0;

            double randomNumber = min + random.NextDouble() * (max - min);

            return randomNumber.ToString("F8");
        }
    }
}
