namespace Script.CoreGame
{
    public static class turnHandler
    {
        private static int turn = 1; // 1 : host , 2 : client
        private static int remainMove = 4;
        private static string hostName;
        private static string clientName;
        private static bool amIHost;
        private static int myScore=0;
        private static int hisScore=0;
        private static int myBoosterProgressBar=0;
        private static int hisBoosterProgressBar=0;
        private static string hisName;

        public static void setHisName(string localHisName)
        {
            hisName = localHisName;
        }

        public static string getHisName()
        {
            return hisName;
        }

        public static void setMyBoosterProgressBar(int localBoosterProgressBar)
        {
            myBoosterProgressBar = localBoosterProgressBar;
        }
        public static void setHisBoosterProgressBar(int localBoosterProgressBar)
        {
            hisBoosterProgressBar = localBoosterProgressBar;
        }

        public static int getMyBoosterProgressBar()
        {
            return myBoosterProgressBar;
        }

        public static int getHisBoosterProgressBar()
        {
            return hisBoosterProgressBar;
        }

        public static void setMyScore(int localScore)
        {
            myScore = localScore;
        }
        public static void setHisScore(int localScore)
        {
            hisScore = localScore;
        }

        public static int getMyScore()
        {
            return myScore;
        }

        public static int getHisScore()
        {
            return hisScore;
        }

        public static bool isMyTurn()
        {
            return (getAmIHost() && getTurn() == 1) ||
                   (!getAmIHost() && getTurn() == 2);
        }
        public static void setAmIHost(bool localAmIHost)
        {
            amIHost = localAmIHost;
        }

        public static bool getAmIHost()
        {
            return amIHost;
        }

        public static void setHostName(string localHostName)
        {
            hostName = localHostName;
        }

        public static void setClientName(string localClientName)
        {
            clientName = localClientName;
        }

        public static string getHostName()
        {
            return hostName;
        }

        public static string getClientName()
        {
            return clientName;
        }

        public static void setTurn(int localTurn)
        {
            turn = localTurn;
        }

        public static int getTurn()
        {
            return turn;
        }

        public static void resetRemainMove()
        {
            remainMove = 4;
        }

        public static void setRemainMove(int localRemainMove)
        {
            remainMove = localRemainMove;

        }

        // public static void increaseRemainMove()
        // {
        //     if (remainMove != 2)
        //         remainMove++;
        // }

        public static int getRemainMove()
        {
            return remainMove;
        }
    }
}