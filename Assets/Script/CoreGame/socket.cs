using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Nakama;
using Nakama.TinyJson;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.CoreGame
{
    public class socket : MonoBehaviour
    {
        public string Scheme = "http";
        public string Host = "157.119.191.169";
        public int Port = 7350;
        public string ServerKey = "defaultkey";

        public IClient Client;
        public ISession Session;
        public ISocket Socket;



        private string currentMatchmakingTicket;

        public async Task Connect()
        {
            // Connect to the Nakama server.
            Client = new Client(Scheme, Host, Port, ServerKey, UnityWebRequestAdapter.Instance);

            // Attempt to restore an existing user session.
            var authToken = PlayerPrefs.GetString("token");
            if (!string.IsNullOrEmpty(authToken))
            {
                var session = Nakama.Session.Restore(authToken);
                if (!session.IsExpired)
                {
                    Session = session;
                }
                //refresh token should be restore authtoken !not_important
            }

            // If we weren't able to restore an existing session, authenticate to create a new user session.
            if (Session == null)
            {
                // SceneManager.LoadScene("LoadingScene");
            }

            // Open a new Socket for realtime communication.
            Socket = Client.NewSocket();
            await Socket.ConnectAsync(Session, true);
        }
        private async void Start()
        {
            // Create an empty dictionary to hold references to the currently connected players.
            // players = new Dictionary<string, GameObject>();

            // Get a reference to the UnityMainThreadDispatcher.
            // We use this to queue event handler callbacks on the main thread.
            // If we did not do this, we would not be able to instantiate objects or manipulate things like UI.
            // var mainThread = UnityMainThreadDispatcher.Instance();

            // Connect to the Nakama server.
            await Connect();

            // // Enable the Find A Match button on the main menu.
            // MainMenu.GetComponent<MainMenu>().EnableFindMatchButton();

            // Setup network event handlers.
            // NakamaConnection.Socket.ReceivedMatchmakerMatched += m => mainThread.Enqueue(() => OnReceivedMatchmakerMatched(m));
            // NakamaConnection.Socket.ReceivedMatchPresence += m => mainThread.Enqueue(() => OnReceivedMatchPresence(m));
            Socket.ReceivedMatchState += m =>OnReceivedMatchState(m);

            // Setup in-game menu event handler.
            // InGameMenu.GetComponent<InGameMenu>().OnRequestQuitMatch.AddListener(async () => await QuitMatch());
        }
        private async Task OnReceivedMatchState(IMatchState matchState)
        {
            // Get the local user's session ID.
            var userSessionId = matchState.UserPresence.SessionId;

            // If the matchState object has any state length, decode it as a Dictionary.
            var state = matchState.State.Length > 0 ? System.Text.Encoding.UTF8.GetString(matchState.State).FromJson<Dictionary<string, string>>() : null;

            // Decide what to do based on the Operation Code as defined in OpCodes.
            Debug.Log(matchState);
            // switch(matchState.OpCode)
            // {
            //     case OpCodes.Died:
            //         // Get a reference to the player who died and destroy their GameObject after 0.5 seconds and remove them from our players array.
            //         var playerToDestroy = players[userSessionId];
            //         Destroy(playerToDestroy, 0.5f);
            //         players.Remove(userSessionId);
            //
            //         // If there is only one player left and that us, announce the winner and start a new round.
            //         if (players.Count == 1 && players.First().Key == localUser.SessionId) {
            //             AnnounceWinnerAndStartNewRound();
            //         }
            //         break;
            //     case OpCodes.Respawned:
            //         // Spawn the player at the chosen spawn index.
            //         SpawnPlayer(currentMatch.Id, matchState.UserPresence, int.Parse(state["spawnIndex"]));
            //         break;
            //     case OpCodes.NewRound:
            //         // Display the winning player's name and begin a new round.
            //         await AnnounceWinnerAndRespawn(state["winningPlayerName"]);
            //         break;
            //     default:
            //         break;
            // }
        }

    }
}