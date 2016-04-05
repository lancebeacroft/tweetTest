using niceTweet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace niceTweet
{
    public class tweetMessageReader :System.Collections.IEnumerable
    {
        private StringBuilder sb = new StringBuilder();
        public List<tweetyMessage> messages= new List<tweetyMessage>();
        
        public tweetMessageReader()
        {
            /*
             * 
             * automatically load message unless otherwise requested
             * 
             */
            LoadMessages();
        }

        public tweetMessageReader(Boolean loadmessages)
        {
            /* 
             * we might not want to load messages every time this object is loaded, so give the 
             * client a constructor to stop this happening
             * 
             */

            if (loadmessages)
            {
                LoadMessages();
            }       
        }
        
        private void LoadMessages()
        {

            /*
             * all tweets are stored in a central file. This will help with ordering by time of arrival 
             * the default is the logged in users documents directory. All usrs are assumed to be logging on the same terminal 
             * and using the same id...
             */

            
            FileInfo fi = TweetRepository.GetFileRepository();

            if (fi.Exists==false)
            {
                return;
            }

            TextFieldParser messageParser = new TextFieldParser(fi.FullName);

            messageParser.Delimiters = new string[] { "\t" };
            messageParser.HasFieldsEnclosedInQuotes = false;

            while (messageParser.EndOfData == false)
            {
                string[] Messageparts = messageParser.ReadFields();
                tweetyMessage newmessage = new tweetyMessage();

                newmessage.user = Messageparts[0];
                newmessage.messageText = Messageparts[1];
                newmessage.messageDateTime  = Convert.ToDateTime(Messageparts[2]);

                messages.Add(newmessage);
            }

            messageParser.Close();
        }

      
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return messages.GetEnumerator();
        }

        public IEnumerable<tweetyMessage> ReadAllmessages()
        {

            IEnumerable<tweetyMessage> results = from tweetymessage in messages
                                                 select tweetymessage ;

            createOutputText(results);
            return results;

        }

        public IEnumerable<tweetyMessage> ReadmessagesForUsers(string Users)
        {

            IEnumerable<tweetyMessage> results = from tweetymessage in messages
                                                 where Users.Contains(tweetymessage.user)
                                                 select tweetymessage;

          createOutputText(results);
          return results;

        }

        public IEnumerable<tweetyMessage> ReadFollowedUsers(string Users)
        {

            IEnumerable<tweetyMessage> results = from tweetymessage in messages
                                                 where Users.Contains(tweetymessage.user)
                                                 select tweetymessage;
            createOutputText(results);
            return results;

        }
        public IEnumerable<tweetyMessage> ReadmessagesWithText(string Text)
        {

            IEnumerable<tweetyMessage> results = from tweetymessage in messages
                                                 where tweetymessage.messageText.Contains(Text) 
                                                 select tweetymessage;
            createOutputText(results);
            return results;

        }

        private void createOutputText(IEnumerable<tweetyMessage> tmr)
        {
            
            //After every action, the state of this objects is committed to text for easy viewing and display by the client.
            sb = null;
            sb = new StringBuilder();
            string lastuser = "";

            foreach (tweetyMessage item in tmr)
            {
                

                if (lastuser == item.user)
                {
                    sb.AppendLine(string.Format("{0}\t{1}\t{2}", " .. ", item.messageText, item.messageDateProperties));
                }
                else 
                {
                    sb.AppendLine(string.Format("{0}\t{1}\t{2}", item.user,item.messageText, item.messageDateProperties));
                    lastuser = item.user;
                }
            }

        }

        public override string ToString()
        {
            return sb.ToString();
        }

        internal void addMessage(tweetyMessage newmessage)
        {
            messages.Add(newmessage);
            createOutputText(messages);
        }
    }
}
