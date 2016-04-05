using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace niceTweet
{
    public class TweetRepository
    { /*
       *
       * Class returns the necessary Tweet File
       * 
       */

        

        static public System.IO.FileInfo GetFileRepository()
        {

            string filename = "Tweets.txt";
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            
            filepath +="\\NiceTweet\\";

            DirectoryInfo di = new DirectoryInfo(filepath);

            FileInfo fi = new FileInfo(filepath  + filename);

            
            /*
             * Does the director exist?
             */

            if (di.Exists!=true)
	        {
		        di.Create();
	        }

            if (fi.Exists != true)
            {

                StreamWriter sw = fi.AppendText();
                sw.WriteLine("");
                sw.Close();
            }


            return fi;

        }

        static public void Deleterepository()
        {


            string filename = "Tweets.txt";
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);

            filepath += "\\NiceTweet\\";

            DirectoryInfo di = new DirectoryInfo(filepath);

            FileInfo fi = new FileInfo(filepath + filename);

            if (di.Exists == true)
            {
                //delete everything that directory and everything underneath
                di.Delete(true);
            }
        

        }

        static public System.IO.FileInfo GetUsersFollowerFile(string User)
        {

            string filename = string.Format("{0}_follows.txt", User);
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            filepath += "\\NiceTweet\\";
            FileInfo fi = new FileInfo(filepath  + filename);
            return fi;

        }

        
    }
}

        

  

