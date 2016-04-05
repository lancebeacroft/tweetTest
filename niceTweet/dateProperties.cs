using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace niceTweet
{
    public class dateProperties
    {
        public DateTime messagedate { get; set; }
        private string calculatedateproperties()
        {
            string result = "";
            DateTime currentdate = new DateTime();
            currentdate = DateTime.Now;
            

            TimeSpan ts = currentdate - messagedate ;

            if (ts.Days == 0 && ts.Hours ==0 && ts.Minutes <= 5)
            {
            
                result = "just now...";
            }

            if (ts.Days == 0 && ts.Hours == 0 && ts.Minutes <= 10 && ts.Minutes > 5)
            {
            
                result = "just now...";
            }

            if (ts.Days == 0 && ts.Days == 0 && ts.Hours == 0 && ts.Minutes > 10)
	        {
                result = string.Format("{0} minutes ago...", ts.Minutes.ToString());
	        }

            if (ts.Days == 0 && ts.Hours == 1)
            {
                result = string.Format("{0} hour {1} minutes ago...",ts.Hours, ts.Minutes  );
            }

            if (ts.Days == 0 && ts.Hours >= 1)
            {
                result = string.Format("{0} hours {1} minutes ago...", ts.Hours,ts.Minutes );
            }

            if (ts.Days == 1)
            {
                result = string.Format("{0} day ago", ts.Hours, ts.Minutes);
            }

            if (ts.Days >= 1)
            {
                result = string.Format("{0} days ago", ts.Days);

            }

            if (result=="")
            {
                throw new Exception("no valid time description found!");
            }

            return result;
        }

        public override string ToString()
        {
            return calculatedateproperties();
        }

    }
}
