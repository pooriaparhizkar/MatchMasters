using System;
using System.Threading.Tasks;
using Script.CoreGame;
using Script.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public class SystemTurnPresentationAdapter : MonoBehaviour, TurnSystemPresentationPort,RoundSystemPresentationPort,FinishGameSystemPresentationPort
    {
        public GameObject hisTurn1;
        public GameObject hisTurn2;
        public GameObject myTurn1;
        public GameObject myTurn2;
        public GameObject blackScreen;
        public GameObject round1_done;
        public GameObject round1_active;
        public GameObject round1_remaining;
        public GameObject round2_done;
        public GameObject round2_active;
        public GameObject round2_remaining;
        public GameObject round3_done;
        public GameObject round3_active;
        public GameObject round3_remaining;
        public GameObject round4_done;
        public GameObject round4_active;
        public GameObject round4_remaining;
        public Text resultWinText;
        public Text resultMyScore;
        public Text resultHisScore;
        public Text resultHistName;
        public Text trophyText;
        public Text coinText;
        public GameObject coreGameCanvas;
        public GameObject resultPageCanvas;

        public async void PlayTurn(bool isMyTurn, int remainMove, Action onCompleted)
        {
            if (!isMyTurn) blackScreen.SetActive(true);
            else blackScreen.SetActive(false);

            if (turnHandler.getAmIHost())
            {
                switch (remainMove)
                {
                    case 4:
                        myTurn1.SetActive(true);
                        myTurn2.SetActive(true);
                        hisTurn1.SetActive(true);
                        hisTurn2.SetActive(true);
                        break;
                    case 3:
                        myTurn1.SetActive(false);
                        myTurn2.SetActive(true);
                        hisTurn1.SetActive(true);
                        hisTurn2.SetActive(true);
                        break;
                    case 2:
                        myTurn1.SetActive(false);
                        myTurn2.SetActive(false);
                        hisTurn1.SetActive(true);
                        hisTurn2.SetActive(true);
                        break;
                    case 1:
                        myTurn1.SetActive(false);
                        myTurn2.SetActive(false);
                        hisTurn1.SetActive(false);
                        hisTurn2.SetActive(true);
                        break;
                    case 0:
                        myTurn1.SetActive(false);
                        myTurn2.SetActive(false);
                        hisTurn1.SetActive(false);
                        hisTurn2.SetActive(false);
                        break;
                }

            }
            else
            {
                switch (remainMove)
                {
                    case 4:
                        hisTurn1.SetActive(true);
                        hisTurn2.SetActive(true);
                        myTurn1.SetActive(true);
                        myTurn2.SetActive(true);
                        break;
                    case 3:
                        hisTurn1.SetActive(false);
                        hisTurn2.SetActive(true);
                        myTurn1.SetActive(true);
                        myTurn2.SetActive(true);
                        break;
                    case 2:
                        hisTurn1.SetActive(false);
                        hisTurn2.SetActive(false);
                        myTurn1.SetActive(true);
                        myTurn2.SetActive(true);
                        break;
                    case 1:
                        hisTurn1.SetActive(false);
                        hisTurn2.SetActive(false);
                        myTurn1.SetActive(false);
                        myTurn2.SetActive(true);
                        break;
                    case 0:
                        hisTurn1.SetActive(false);
                        hisTurn2.SetActive(false);
                        myTurn1.SetActive(false);
                        myTurn2.SetActive(false);
                        break;
                }
            }
            onCompleted.Invoke();
        }

        public void PlayRoundSystem(int whichRound, Action onCompleted)
        {
            switch (whichRound)
            {
                case 1:
                    round1_remaining.SetActive(false);
                    round1_active.SetActive(true);
                    round1_done.SetActive(false);
                    round2_remaining.SetActive(true);
                    round2_active.SetActive(false);
                    round2_done.SetActive(false);
                    round3_remaining.SetActive(true);
                    round3_active.SetActive(false);
                    round3_done.SetActive(false);
                    round4_remaining.SetActive(true);
                    round4_active.SetActive(false);
                    round4_done.SetActive(false);
                    break;
                case 2:
                    round1_remaining.SetActive(false);
                    round1_active.SetActive(false);
                    round1_done.SetActive(true);
                    round2_remaining.SetActive(false);
                    round2_active.SetActive(true);
                    round2_done.SetActive(false);
                    round3_remaining.SetActive(true);
                    round3_active.SetActive(false);
                    round3_done.SetActive(false);
                    round4_remaining.SetActive(true);
                    round4_active.SetActive(false);
                    round4_done.SetActive(false);
                    break;
                case 3:
                    round1_remaining.SetActive(false);
                    round1_active.SetActive(false);
                    round1_done.SetActive(true);
                    round2_remaining.SetActive(false);
                    round2_active.SetActive(false);
                    round2_done.SetActive(true);
                    round3_remaining.SetActive(false);
                    round3_active.SetActive(true);
                    round3_done.SetActive(false);
                    round4_remaining.SetActive(true);
                    round4_active.SetActive(false);
                    round4_done.SetActive(false);
                    break;
                case 4:
                    round1_remaining.SetActive(false);
                    round1_active.SetActive(false);
                    round1_done.SetActive(true);
                    round2_remaining.SetActive(false);
                    round2_active.SetActive(false);
                    round2_done.SetActive(true);
                    round3_remaining.SetActive(false);
                    round3_active.SetActive(false);
                    round3_done.SetActive(true);
                    round4_remaining.SetActive(false);
                    round4_active.SetActive(true);
                    round4_done.SetActive(false);
                    break;
            }
        }

        public void PlayFinishGameSystem(int myScore, int hisScore, Action onCompleted)
        {
            resultHisScore.text = hisScore.ToString();
            resultMyScore.text = myScore.ToString();
            resultHistName.text = turnHandler.getHisName();
            string localWinText;
            int winStreak = ProgressBar.myUserInfoDetail.userinfo.user.metadata.current_win_streak;
            if (myScore > hisScore)
            {
                localWinText = PlayerPrefs.GetString("username") + "WIN!";
                if (  winStreak<=1)
                {
                    trophyText.text = "+25";
                    coinText.text = "+20";
                }
                else if (  winStreak==2)
                {
                    trophyText.text = "+30";
                    coinText.text = "+30";
                }
                else
                {
                    trophyText.text = "+30 x 2";
                    coinText.text = "+20 x 2";
                }
            }
            else if(myScore < hisScore)
            {
                localWinText = turnHandler.getHisName() + "WIN!";
                trophyText.text = "-24";
                coinText.text = "0";
            }
            else
            {
                localWinText = "TIE!";
                trophyText.text = "0";
                coinText.text = "0";
            }
            
            resultWinText.text = localWinText;
            resultPageCanvas.SetActive(true);
            coreGameCanvas.SetActive(false);
        }
    }
}