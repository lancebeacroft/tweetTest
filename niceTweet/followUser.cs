using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace niceTweet
{
    public class followUser
    {
        /*
         *
         * Follow user class creates a follow relationship betwen one user and n other users.
         * 
         */

        private string user;
        private string followeduser;

        public followUser(string p1, string p2)
        {
            // TODO: Complete member initialization
            this.user = p1;
            this.followeduser = p2;

            //

            FileInfo fi = TweetRepository.GetUsersFollowerFile(this.user);
            StreamWriter sw = new StreamWriter(fi.FullName,true);
            
            //write this followed user to the follows file.
            sw.WriteLine(followeduser);
            sw.Close();

            //tell the world who the user is following
            tweetyMessage fm = new tweetyMessage(user, string.Format("{0} is following {1}", user, followeduser));
            TweetMessageWriter tm = new TweetMessageWriter(fm);
            tm.writeMessage();

        }

        public followUser()
        {
            // TODO: Complete member initialization
        }

        public string showFollowed(string User)
        {

            FileInfo fi = TweetRepository.GetUsersFollowerFile(User);
            string FollowedUsers = "";

            if (fi.Exists==false)
            {
                //nobody is being followed here...
                return "";
            }

            using (StreamReader sr = fi.OpenText())
            {
                // read all the elments and bind them into a CSV string
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    FollowedUsers += ("," + s);
                }

                sr.Close();
            }

            return FollowedUsers;

        }
    }
}
