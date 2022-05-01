using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nakama;
using Nakama.TinyJson;
using Script.Types;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadmilestone : MonoBehaviour
{
    // Start is called before the first frame update
        public ISession session;
        public readonly IClient client = new Client("http", "157.119.191.169", 7350, "defaultkey");
        public Text trophy;
        public Text coin;
    
        public void Start()
        {
            trophy.text = ProgressBar.myUserInfoDetail.userinfo.user.metadata.trophy.ToString();
            coin.text = ProgressBar.myUserInfoDetail.userinfo.user.metadata.coins.ToString();
        }

        public void loadmilstone2()
        {
            SceneManager.LoadScene("milestone");
        }

        

       
}
