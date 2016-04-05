using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace niceTweet_test
{
    class Output
    {
        static public void WriteLine(string text)
        {
            Trace.WriteLine(text);
        }
    }
}
