using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Nakama;
using Nakama.TinyJson;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.CoreGame
{
    public class socketLogic : MonoBehaviour
    {
        private ISocket mySocket;
        private String myGameMatchicket;
        private async void Start()
        {
            mySocket = MatchMakingLogic.socket;
            myGameMatchicket = MatchMakingLogic.gameMatchicket;
            
            // Use whatever decoder for your message contents.
            var enc = System.Text.Encoding.UTF8;
            mySocket.ReceivedMatchState += newState =>
            {
                var content = enc.GetString(newState.State);
                Debug.LogFormat("User '{0}'' sent '{1}'", newState.UserPresence.Username, content);
            };
        }
        
        public async void OnBackBtnClick()
        {
      
            var matchId =myGameMatchicket;
       
            await mySocket.LeaveMatchAsync(matchId);
            Debug.Log("Quited");
        
            // SceneManager.LoadScene("MainApp");
        }
        public async void OnReconnectBtnClick()
        {
      
            var matchId =myGameMatchicket;
       
            await mySocket.JoinMatchAsync(matchId);
            Debug.Log("Rejoined");
        
            // SceneManager.LoadScene("MainApp");
        }

        public void sendChat()
        {
            var newState = new Dictionary<string, string> {{"hello", "world"}}.ToJson();
            mySocket.SendMatchStateAsync(myGameMatchicket, 1, newState);
        }
        
       

    }
}