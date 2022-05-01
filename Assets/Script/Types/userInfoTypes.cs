namespace Script.Types
{
    public class userInfoTypes
    {
        public struct Boosters
        {
            public string name;
            public int count;
        }
        public struct MetaData
        {
            public int win_games;
            public int match_played;
            public int average_game_score;
            public int current_win_streak;
            public int trophy;
            public Boosters[] boosters;
            public int coins;
        }
        public struct User
        {
            public string username;
            public string userId;
            public MetaData metadata;
        }
        public struct UserInfo
        {
            public User user;
        }
        public struct userInfoDetail
        {
            public UserInfo userinfo;
        }
    }
}