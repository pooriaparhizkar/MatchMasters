using System;
using System.Threading.Tasks;
using DG.Tweening;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;

namespace Sample
{
    public class SystemSwapPresentationAdapter : MonoBehaviour, SwapSystemPresentationPort
    {
        public async void PlaySwap(CellStack cellStack1, CellStack cellStack2, Action onCompleted)
        {
            // Here you can play an animation for swaping the presenters for these CellStacks.
            if (cellStack1.HasTileStack() && cellStack2.HasTileStack())
            {
                var tileStack1 = cellStack1.CurrentTileStack();
                var presenter1 = tileStack1.GetComponent<gemTilePresenter>();
                var tileStack2 = cellStack2.CurrentTileStack();
                var presenter2 = tileStack2.GetComponent<gemTilePresenter>();
                presenter1.transform.DOMove(presenter2.transform.position, 0.4f);
                presenter2.transform.DOMove(presenter1.transform.position, 0.4f);
            }

            // Debug.Log($"Start Swaping {cellStack1.Position()} and {cellStack2.Position()} ");

            await Task.Delay(200);

            // Debug.Log($"Finished Swaping {cellStack1.Position()} and {cellStack2.Position()} ");

            onCompleted.Invoke();




        }
    }
}