using System;
using System.Threading.Tasks;
using DG.Tweening;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;

namespace Sample
{
    public class emptyTile : Tile
    {
    }
    public class SystemDestroyPresentationAdapter : MonoBehaviour, DestroySystemPresentationPort
    {
        public async void PlayDestroy(CellStack cellStack1, Action onCompleted)
        {
            // Here you can play an animation for Destroying the presenters for these CellStacks.
            if (cellStack1.HasTileStack())
            {
                var tileStack1 = cellStack1.CurrentTileStack();
                var presenter1 = tileStack1.GetComponent<gemTilePresenter>();
                // presenter1.transform.DOMove(new Vector3(.7f,1.2f,1), 1);
                presenter1.transform.DOScaleY(0, 0.5f);
                // cellStack1.SetCurrnetTileStack(new TileStack());
            }

            // Debug.Log($"Start Destroying {cellStack1.Position()} and {cellStack2.Position()} ");

            await Task.Delay(500);

            // Debug.Log($"Finished Destroying {cellStack1.Position()} and {cellStack2.Position()} ");

            onCompleted.Invoke();
        }

    }

}