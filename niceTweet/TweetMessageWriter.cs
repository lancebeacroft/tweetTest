using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace niceTweet
{
    public class TweetMessageWriter
    {
        private tweetyMessage tm;

        public TweetMessageWriter(tweetyMessage tm)
        {
            // TODO: Complete member initialization
            this.tm = tm;
        }

        public TweetMessageWriter()
        {
            // TODO: Complete member initialization
        }

        

        public void writeMessage()
        {

            /*
             * if the save file does not exist then create it. otherwise write the contents of the message
             * to the file...
             * 
             */

            FileInfo fi = TweetRepository.GetFileRepository();
           
            string MessageToWrite = String.Format("{0}\t{1}\t{2}\n", tm.user , tm.messageText, tm.messageDateTime);

            if (fi.Exists)
            {
                System.IO.File.AppendAllText(fi.FullName, MessageToWrite);
            }
            else 
            {
                System.IO.File.WriteAllText(fi.FullName, MessageToWrite);
            }
        }

        public void writeMessage(tweetyMessage tweetyMessage)
        {
            this.tm = tweetyMessage;
            writeMessage();
 
        }
    }
}
