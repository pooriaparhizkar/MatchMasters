using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nakama;
using Nakama.TinyJson;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.CoreGame
{
    public class resultPageHandler : MonoBehaviour
    {
        private ISession session;
        private readonly IClient client = new Client("http", "157.119.191.169", 7350, "defaultkey");
        private string winner;
        private bool isFinish;
        private async void Start()
        {
            session = Session.Restore(PlayerPrefs.GetString("token"));
            if (turnHandler.getMyScore() > turnHandler.getHisScore()) // i win
                winner = ProgressBar.myUserInfoDetail.userinfo.user.username;
            else if(turnHandler.getMyScore() < turnHandler.getHisScore()) // he win
                winner =turnHandler.getHisName();
            else winner = ProgressBar.myUserInfoDetail.userinfo.user.username; // draw
            
            if (turnHandler.getAmIHost())
            {
                var payload = new Dictionary<string, string>
                    {{"hostId", ProgressBar.myUserInfoDetail.userinfo.user.userId},{"clientId", turnHandler.getHisClientId()},
                        {"winUser", winner},{"hostBooster",turnHandler.getMyBoosterName()},
                        {"clientBooster", turnHandler.getHisBoosterName() }, {"starNum", "3"}}.ToJson();
                Debug.Log(payload);
                string rpcid = "saveresult";
                Task<IApiRpc> result;
                result = client.RpcAsync(session, rpcid, payload);
                // Debug.Log(shop.Status);
                IApiRpc searchResult = await result;
                isFinish = true;
                Debug.Log(searchResult.ToString());
                //
                // Debug.Log(searchResult);
            }
           
        }

        public async void onOkClick()
        {
            // if (isFinish)
            // {
                await ProgressBar.getUserInfoDetail();
                SceneManager.LoadScene("MainApp");
            // }
           
        }
    }
}