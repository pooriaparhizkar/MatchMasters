using System;
using System.Threading.Tasks;
using DG.Tweening;
using Medrick.Match3CoreSystem.Game.Core;
using Script.CoreGame;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public class SystemDevKitPresentationAdapter : MonoBehaviour, DevkitSystemPresentationPort
    {
        public Text devKitTextArea;
        public void PlayDevkit(string localContent, Action onCompleted)
        {
            devKitTextArea.text = localContent;
        }
    }
}