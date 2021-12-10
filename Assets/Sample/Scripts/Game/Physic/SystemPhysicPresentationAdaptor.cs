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

        public async void PlayPhysic(CellStack cellStack,CellStack nextCell,BasicGameplayMainController gameplayController, Action onCompleted)
        {
            // Here you can play an animation for Physicing the presenters for these CellStacks.
            if (cellStack.HasTileStack())
            {
                var tileStack1 = cellStack.CurrentTileStack();
                var presenter1 = tileStack1.GetComponent<gemTilePresenter>();
                // var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
                // var presenter2 = cellStackBoard[new Vector2Int(cellStack1.Position().x, cellStack1.Position().y + 1)].CurrentTileStack()
                //     .GetComponent<gemTilePresenter>();
                // Debug.Log("sdadsadad");


                // float yPosition = (ypos-0.7f);
                // presenter1.transform.DOMove(  new Vector3(presenter1.transform.position.x,yPosition,1), .1f);
                //

                for (float i = cellStack.Position().y; i < nextCell.Position().y; i+=0.1f)
                {
                    Debug.Log(i);
                    tileStack1.SetPosition(new Vector2(tileStack1.Position().x,i));
                    await Task.Delay(1);

                }


                // tileStack1.SetPosition(new Vector2(1,1));


                // cellStack1.CurrentTileStack().SetPosition(new Vector2(tileStack1.Position().x,tileStack1.Position().y+1));

                // for (float i = 0; i < 1; i+=0.1f)
                // {
                //     cellStack1.CurrentTileStack().SetPosition(new Vector2(tileStack1.Position().x,tileStack1.Position().y+i));
                //     await Task.Delay(500);
                // }


                // presenter1.transform.DOScaleY(4, 0.5f);
                // cellStack1.SetCurrnetTileStack(new TileStack());
               // presenter1.transform.DOPause();

                // await Task.Delay(5000);

                // Debug.Log($"Finished Physicing {cellStack1.Position()} and {cellStack2.Position()} ");

                onCompleted.Invoke();
            }

            // Debug.Log($"Start Physicing {cellStack1.Position()} and {cellStack2.Position()} ");


        }
    }
}