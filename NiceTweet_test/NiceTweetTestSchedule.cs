using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using niceTweet;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace niceTweet_test
{


    [TestClass]
    public class NiceTweetTestSchedule
    {
        [TestMethod]
        public void Testdateproperties()
        {
            //test for the message date parameters....
            dateProperties dp = new dateProperties();
            dp.messagedate  = new DateTime(2016, 3, 1, 0, 0, 00);

            Trace.WriteLine(string.Format("{0};{1}",DateTime.Now.ToLongDateString(),DateTime.Now.ToLongTimeString()));
            Trace.WriteLine(dp.ToString()); 

        }

        [TestMethod]
        public void GetTweetFile() 
        {
           
            FileInfo fi = TweetRepository.GetFileRepository();
            Trace.WriteLine(fi.FullName);

        }


        [TestMethod]
        public void DeleteTweetFile() 
        {

            try
            {
                TweetRepository.Deleterepository();
            }
            catch (Exception e)
            {

                Assert.Fail(e.Message);
            }
            
        }


    

        [TestMethod]
        public void createMessage()
        {

            string Message ="Here is my message";
            string user = "Lance"; 

            tweetyMessage tm = new tweetyMessage();
            tm.messageText= Message;
            tm.user = user;

            if (tm.messageText != Message)
            {
                Assert.Fail("The input does not match the output for the message");
                
            }
            else
            {
                Trace.WriteLine("pass message set and retrieval");
            }
            
            if (tm.user != user)
            {
                 Assert.Fail("The input does not match the output for the user");
            }
            else
            {
                Trace.WriteLine("pass user set and retrieval");
            }

            Trace.WriteLine(string.Format("message date and time: {0}:{1}", tm.messageDateTime.ToLongDateString(), tm.messageDateTime.ToLongTimeString()));
            
        }

        [TestMethod]
        public void WriteMessageToDb()
        {
            createalogofMessages();

            tweetyMessage tm = new tweetyMessage("Lance", "This is a test message");
            TweetMessageWriter wt = new TweetMessageWriter(tm);
            try
            {
                wt.writeMessage();
                Trace.WriteLine("Passed Write Message");
            }
            catch (Exception e)
            {
                
                 Assert.Fail(e.Message);
            }

        }

        //[TestMethod]
        public void createalogofMessages()
        {


            /*
             * All tests start with a clean file to work against to ensure that no test affects any other.
             * This method creates the tweets file, removes the following files and creates a clean slate
             * for the subsequent test.
             * 
             */

            //get rid of the server/file if it exists..
            TweetRepository.Deleterepository();
          
            //Set up an array to contain a set of messages
            TweetMessageWriter tmw = new TweetMessageWriter();

            //create those message and add them to the collection
            tmw.writeMessage(new tweetyMessage("Alice","I love the weather today"));
            tmw.writeMessage(new tweetyMessage("Bob" ,"Damn! We lost!"));
            tmw.writeMessage(new tweetyMessage("Bob" ,"Good game though."));
            tmw.writeMessage(new tweetyMessage("Charlie", "I'm in New York today! Anyone want to have a coffee?"));

            tweetMessageReader tmr = new tweetMessageReader();
            Trace.WriteLine(tmr.ToString()); 
           

        }


        [TestMethod]
        public void followTest()
        {
            //This will create a file called UserName_follows and return all the messages for that User
            //it will also create a message from lance indicating that he is following bob

            createalogofMessages();

            followUser followAlice = new followUser("Bob", "Alice");
            followUser followCharlie = new followUser("Bob", "Charlie");
            followUser followed = new followUser();

            tweetMessageReader tmr = new tweetMessageReader();

            tmr.ReadFollowedUsers(followed.showFollowed("Bob"));

            Trace.WriteLine(tmr.ToString());
            }

             
        [TestMethod]
        public void ApplicationTest_Post()
        {
            createalogofMessages();

            string[] args = { "Charlie", "Posting", "and here is sit broken hearted, spent a penny only farted!" };
            niceTweetApplication nta = new niceTweet.niceTweetApplication(args);
            Trace.WriteLine(nta.ToString());
        }

        [TestMethod]
        public void ApplicationTest_Post_alternative_method()
        {

            //this method will not work in live on the console as the > char is the commend to redirect to a file...
            createalogofMessages();

            string[] args = { "Charlie", "->", "Alternative method using ->" };
            niceTweetApplication nta = new niceTweet.niceTweetApplication(args);
            Trace.WriteLine(nta.ToString());
 
        }

        [TestMethod]
        public void ApplicationTest_Readall()
        {
            createalogofMessages();
            string[] args= {};
            niceTweetApplication nta = new niceTweet.niceTweetApplication(args);
            Trace.WriteLine(nta.ToString());
        }

        [TestMethod]
        public void ApplicationTest_ReadUser()
        {

            createalogofMessages();

            string[] args = {"Alice"};
            niceTweetApplication nta = new niceTweet.niceTweetApplication(args);
            Trace.WriteLine(nta.ToString());
        }

        [TestMethod]
        public void Applicationtest_Follow()
        {

            createalogofMessages();

            string[] args = { "Bob","Following", "Charlie" };
            niceTweetApplication nta = new niceTweet.niceTweetApplication(args);
            Trace.WriteLine(nta.ToString());
        }

        [TestMethod]
        public void Applicationtest_Wall()
        {

            createalogofMessages();

            string[] args = { "Bob", "Wall", "New York" };
            niceTweetApplication nta = new niceTweet.niceTweetApplication(args);
            Trace.WriteLine(nta.ToString());
        }

        [TestMethod]
        public void Applicationtest_ShowHelp()
        {

            createalogofMessages();

            string[] args = { "?"};
            niceTweetApplication nta = new niceTweet.niceTweetApplication(args);
            Trace.WriteLine(nta.ToString());
        }
          
        }

       

     
    }

