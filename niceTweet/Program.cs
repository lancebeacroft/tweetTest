using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace niceTweet
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                niceTweetApplication nta = new niceTweetApplication(args);
                Console.WriteLine(nta.ToString());
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

        }
    }
}
