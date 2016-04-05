using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace niceTweet
{
    public class niceTweetApplication
    {
        private string[] commandlineparameters;
        private StringBuilder sb = new StringBuilder();

        public enum eApplicationState
        {
            posting,
            reading,
            following,
            wall,
            help,
            unknown
        }

        public enum efinalResult { ok, failed, processing }

        private eApplicationState _applicationstate = eApplicationState.unknown;


        public niceTweetApplication(string[] args)
        {
            // TODO: Complete member initialization
            this.commandlineparameters = args;


            if (commandlineparameters.Length == 0)
            {
                _applicationstate = eApplicationState.reading;
                readAllMessages();
                return;
            }

            if (commandlineparameters.Length == 1)
            {
                if (commandlineparameters[0] == "?")
                {

                    ShowHelp();
                    return;
                }
                else
                {
                    _applicationstate = eApplicationState.reading;
                    readMessages();
                    return;
                }
            }

            switch (commandlineparameters[1].ToLower())
            {
                case "posting":
                case "->":
                    _applicationstate = eApplicationState.posting;
                    postMessage();
                    break;

                case "following":
                    _applicationstate = eApplicationState.following;
                    followMessages();
                    break;

                case "wall":
                    _applicationstate = eApplicationState.wall;
                    Wall();
                    break;

                default:
                    throw new Exception("Unrecognised Parameter: Use Posting, Following, Wall, or leave blank ");
            }
        }

        private void ShowHelp()
        {
            sb = new StringBuilder();
            sb.AppendLine("NiceTweet.exe\nLance Beacroft\n0751 546 8216\nLanceBeacroft@Beacroftsoftware.com");
            sb.AppendLine("------------------------------------------------------------------------------");
            sb.AppendLine("Syntax:");
            sb.AppendLine("Read all messages by ascending order of date arrived (oldest at the top!)");
            sb.AppendLine("    nicetweet.exe");
            sb.AppendLine("");
            sb.AppendLine("Read a user's messages and followed users");
            sb.AppendLine("    nicetweet.exe username");
            sb.AppendLine("");
            sb.AppendLine("Post a message");
            sb.AppendLine("    nicetweet.exe username POSTING \"\"Message\"\"");
            sb.AppendLine("");
            sb.AppendLine("Wall");
            sb.AppendLine("    nicetweet.exe username WALL \"\" Text\"\" ");
            sb.AppendLine("");
            sb.AppendLine("Follow");
            sb.AppendLine("    nicetweet.exe username FOLLOWING FollowedUserName");
            sb.AppendLine("");
            sb.AppendLine("Help");
            sb.AppendLine("    nicetweet.exe ?");

        }

        private void postMessage()
        {

            string user = commandlineparameters[0];
            string Message = commandlineparameters[2];
            tweetyMessage newmessage = new tweetyMessage(user, Message);

            TweetMessageWriter tmw = new TweetMessageWriter(newmessage);
            tweetMessageReader tmr = new tweetMessageReader(false);

            tmr.addMessage(newmessage);
            tmw.writeMessage();

            this.sb.AppendLine("New Post!...");
            this.sb.AppendLine(tmr.ToString());

        }

        private void readAllMessages()
        {

            followUser fm = new followUser();
            tweetMessageReader tmr = new tweetMessageReader();

            tmr.ReadAllmessages();
            sb.AppendLine(tmr.ToString());

        }

        private void readMessages()
        {

            followUser fm = new followUser();
            tweetMessageReader tmr = new tweetMessageReader();

            string user = commandlineparameters[0];

            tmr.ReadmessagesForUsers(user);
            sb.AppendLine(tmr.ToString());

            sb.AppendLine("Followed Users:");
            tmr.ReadFollowedUsers(fm.showFollowed(user));
            sb.AppendLine(tmr.ToString());

        }

        private void followMessages()
        {

            string user = commandlineparameters[0];
            string followeduser = commandlineparameters[2];
            followUser fu = new followUser(user, followeduser);

            readMessages();

        }

        private void Wall()
        {

            string walltext = commandlineparameters[2];

            tweetMessageReader tmr = new tweetMessageReader();
            tmr.ReadmessagesWithText(walltext);
            sb.AppendLine("Wall");
            sb.AppendLine(tmr.ToString());

        }

        public eApplicationState ApplicationState
        {
            get { return _applicationstate; }
            set { _applicationstate = value; }
        }

        public override string ToString()
        {
            return this.sb.ToString();
        }

    }
}
