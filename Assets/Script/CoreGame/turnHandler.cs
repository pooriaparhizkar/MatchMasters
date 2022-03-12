namespace Script.CoreGame
{
    public static class turnHandler
    {
        private static int turn = 1; // 1 : host , 2 : client
        private static int remainMove = 4;
        private static string hostName;
        private static string clientName;
        private static bool amIHost;

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