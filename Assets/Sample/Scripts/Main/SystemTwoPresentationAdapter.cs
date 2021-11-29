using Medrick.Match3CoreSystem.Game.Core;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Sample
{
    public class SystemTwoPresentationAdapter : MonoBehaviour, SystemTwoPresentationPort
    {
        public async void PlaySwap(CellStack cellStack1, CellStack cellStack2, Action onCompleted)
        {
            // Here you can play an animation for swaping the presenters for these CellStacks.

            UnityEngine.Debug.Log($"Start Swaping {cellStack1.Position()} and {cellStack2.Position()} ");

            await Task.Delay(1000);

            UnityEngine.Debug.Log($"Finished Swaping {cellStack1.Position()} and {cellStack2.Position()} ");

            onCompleted.Invoke();
        }
    }
}