using System;
using System.Threading.Tasks;
using Script.CoreGame;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public class SystemTurnPresentationAdapter : MonoBehaviour, TurnSystemPresentationPort
    {
        public GameObject hisTurn1;
        public GameObject hisTurn2;
        public GameObject myTurn1;
        public GameObject myTurn2;
        public GameObject blackScreen;


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
    }
}