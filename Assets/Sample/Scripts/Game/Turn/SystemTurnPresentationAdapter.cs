using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public class SystemTurnPresentationAdapter : MonoBehaviour, TurnSystemPresentationPort
    {
        public Text herTurn;
        public Text myTurn;

        public async void PlayTurn(bool isMyTurn, Action onCompleted)
        {
            if (isMyTurn)
                myTurn.text = (int.Parse(myTurn.text) + 1).ToString();
            else
                herTurn.text = (int.Parse(myTurn.text) + 1).ToString();

            await Task.Delay(100);

            onCompleted.Invoke();
        }
    }
}