using System;
using System.Threading.Tasks;
using DG.Tweening;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;


namespace Sample
{
    public class SystemPerkHandlerPresentationAdaptor : MonoBehaviour, PerkHandlerSystemPresentationPort
    {
        public GameObject hammerPerkBlackScreen;
        public void PlayPerkHandler(bool isStart, Action onCompleted)
        {
            hammerPerkBlackScreen.SetActive(isStart);
            onCompleted.Invoke();
        }
    }
}