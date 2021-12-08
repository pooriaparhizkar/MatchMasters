using System;
using System.Threading.Tasks;
using DG.Tweening;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;

namespace Sample
{
    public class SystemPhysicPresentationAdaptor : MonoBehaviour, PhysicSystemPresentationPort
    {

        public async void PlayPhysic(CellStack cellStack1,BasicGameplayMainController gameplayController, Action onCompleted)
        {
            // Here you can play an animation for Physicing the presenters for these CellStacks.
            if (cellStack1.HasTileStack())
            {
                var tileStack1 = cellStack1.CurrentTileStack();
                var presenter1 = tileStack1.GetComponent<gemTilePresenter>();
                // var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
                // var presenter2 = cellStackBoard[new Vector2Int(cellStack1.Position().x, cellStack1.Position().y + 1)].CurrentTileStack()
                //     .GetComponent<gemTilePresenter>();
                Debug.Log("sdadsadad");
                presenter1.transform.DOMove(  new Vector3(presenter1.transform.position.x,presenter1.transform.position.y-0.05f,1), .5f);
                // presenter1.transform.DOScaleY(4, 0.5f);
                // cellStack1.SetCurrnetTileStack(new TileStack());
            }

            // Debug.Log($"Start Physicing {cellStack1.Position()} and {cellStack2.Position()} ");

            await Task.Delay(500);

            // Debug.Log($"Finished Physicing {cellStack1.Position()} and {cellStack2.Position()} ");

            onCompleted.Invoke();
        }
    }
}