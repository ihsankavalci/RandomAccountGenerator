using System;

namespace ConsoleApp3
{
    public class RandomHelper
    {
        public RandomHelper()
        {
            rnd = new Random();
        }

        public Random rnd { get; set; }

        public int RandomInt()
        {
            return rnd.Next(0, 1000);
        }

        public String RandomStr()
        {
            int length = 8;
            var str = "";
            for (var i = 0; i < length; i++)
            {
                str += ((char)(rnd.Next(1, 26) + 96)).ToString();
            }
            return str;
        }

        public String RandomEmail()
        {
            int r = rnd.Next(0, 3);
            if (r == 0)
            {
                return "@gmail.com";
            }
            else if (r == 1)
            {
                return "@hotmail.com";
            }

            return "@yemeksepeti.com";
        }
    }
}
