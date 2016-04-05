using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace niceTweet
{
    public class tweetyMessage : System.IDisposable 
    {

        private DateTime _messageDatetime = DateTime.Now;
        

        public string messageText { get; set; }
        public string user { get; set; }
        public string messageDateProperties 
        {

            get { 
                
                dateProperties mdp = new dateProperties();
                mdp.messagedate = _messageDatetime;
                return mdp.ToString();

            }
            

        }

        

        public tweetyMessage() { }

        public tweetyMessage(string user, string messagetext)
        {
            this.user = user;
            this.messageText = messagetext;
        }

        public void Dispose()
        {
         //so far i don't have any collections to bin here so his can stay blank
        }

        public DateTime messageDateTime 
        { 
            get 
            { 
                return _messageDatetime; 
            }
            set
            {
                _messageDatetime = value;
            }
 
        }
    }
}
