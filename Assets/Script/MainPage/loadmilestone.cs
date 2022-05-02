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
// using Slider = UnityEngine.UIElements.Slider;

public class loadmilestone : MonoBehaviour
{
    // Start is called before the first frame update
        public Text trophy;
        public Text coin;
        public Text selectBoosterCoin;
        public Slider trophySlider;
        public Text trophySliderText;
        // public Slider trophySlider;
    
        public void Start()
        {
            
            trophy.text = ProgressBar.myUserInfoDetail.userinfo.user.metadata.trophy.ToString();
            coin.text = ProgressBar.myUserInfoDetail.userinfo.user.metadata.coins.ToString();
            selectBoosterCoin.text = ProgressBar.myUserInfoDetail.userinfo.user.metadata.coins.ToString();
            int haveTrophy = ProgressBar.myUserInfoDetail.userinfo.user.metadata.trophy;
            int nexLevelTrophy;
            if (haveTrophy < 200)
                nexLevelTrophy = 200;
            else if (haveTrophy >= 200 && haveTrophy < 500)
                nexLevelTrophy = 500;
            else if (haveTrophy >= 500 && haveTrophy < 800)
                nexLevelTrophy = 800;
            else if (haveTrophy >= 800 && haveTrophy < 1100)
                nexLevelTrophy = 1100;
            else
                nexLevelTrophy = 1400;
            trophySlider.value = (float)haveTrophy / nexLevelTrophy;
            trophySliderText.text = haveTrophy.ToString() + '/' + nexLevelTrophy.ToString();
        }

        public void loadmilstone2()
        {
            SceneManager.LoadScene("milestone");
        }

        

       
}
