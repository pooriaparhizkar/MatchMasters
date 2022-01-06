using System;
using System.Threading.Tasks;
using DG.Tweening;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sample
{
    public class SystemInGameBoosterInstancePresentationAdaptor : MonoBehaviour, InGameBoosterInstanceSystemPresentationPort
    {
        public GameObject[] leftRightArrows;
        public GameObject[] upDownArrows;
        public GameObject[] bomb;
        public GameObject[] lightning;
        public GameObject boardGame;

        private Vector3 logicalPositionToPresentation(Vector2 pos)
        {
            return new Vector3((pos.x - 3) * 112, (pos.y + 1000) * -98,
                transform.position.z);
        }

        public void PlayInGameBoosterInstance(InGameBoosterInstanceBlackBoard.InGameBoosterInstanceData inGameBoosterInstanceData, BasicGameplayMainController gameplayController,
            Action onCompleted)
        {
            // Here you can play an animation for TopInstanceing the presenters for these CellStacks.
            var tileStackFactory = new MainTileStackFactory();
            var tileStack = tileStackFactory.Create();
            CellStack cellStack = (gameplayController.LevelBoard.CellStackBoard())[
                (int) inGameBoosterInstanceData.position.x, (int) inGameBoosterInstanceData.position.y];
            Destroy(cellStack.CurrentTileStack().GetComponent<gemTilePresenter>().gameObject);
            cellStack.SetCurrnetTileStack(tileStack);
            tileStack.SetPosition(new Vector2(cellStack.Position().x,cellStack.Position().y));
            tileStack.Push(new gemTile(inGameBoosterInstanceData.color,gemTypes.booster));


            GameObject newObject = null;
            if (inGameBoosterInstanceData.type == InGameBoosterInstanceBlackBoard.InGameBoosterType.leftRightArrow)
            {
                switch (inGameBoosterInstanceData.color)
                {
                    case gemColors.blue:
                        newObject = Instantiate(leftRightArrows[0]);
                        break;
                    case gemColors.green:
                        newObject = Instantiate(leftRightArrows[1]);
                        break;
                    case gemColors.orange:
                        newObject = Instantiate(leftRightArrows[2]);
                        break;
                    case gemColors.purple:
                        newObject = Instantiate(leftRightArrows[3]);
                        break;
                    case gemColors.red:
                        newObject = Instantiate(leftRightArrows[4]);
                        break;
                    case gemColors.yellow:
                        newObject = Instantiate(leftRightArrows[5]);
                        break;
                }
            }
            else if (inGameBoosterInstanceData.type == InGameBoosterInstanceBlackBoard.InGameBoosterType.upDownarrow)
            {
                switch (inGameBoosterInstanceData.color)
                {
                    case gemColors.blue:
                        newObject = Instantiate(upDownArrows[0]);
                        break;
                    case gemColors.green:
                        newObject = Instantiate(upDownArrows[1]);
                        break;
                    case gemColors.orange:
                        newObject = Instantiate(upDownArrows[2]);
                        break;
                    case gemColors.purple:
                        newObject = Instantiate(upDownArrows[3]);
                        break;
                    case gemColors.red:
                        newObject = Instantiate(upDownArrows[4]);
                        break;
                    case gemColors.yellow:
                        newObject = Instantiate(upDownArrows[5]);
                        break;
                }
            }
            else if (inGameBoosterInstanceData.type == InGameBoosterInstanceBlackBoard.InGameBoosterType.lightning)
            {
                switch (inGameBoosterInstanceData.color)
                {
                    case gemColors.blue:
                        newObject = Instantiate(lightning[0]);
                        break;
                    case gemColors.green:
                        newObject = Instantiate(lightning[1]);
                        break;
                    case gemColors.orange:
                        newObject = Instantiate(lightning[2]);
                        break;
                    case gemColors.purple:
                        newObject = Instantiate(lightning[3]);
                        break;
                    case gemColors.red:
                        newObject = Instantiate(lightning[4]);
                        break;
                    case gemColors.yellow:
                        newObject = Instantiate(lightning[5]);
                        break;
                }
            }
            else if (inGameBoosterInstanceData.type == InGameBoosterInstanceBlackBoard.InGameBoosterType.bomb)
            {
                switch (inGameBoosterInstanceData.color)
                {
                    case gemColors.blue:
                        newObject = Instantiate(bomb[0]);
                        break;
                    case gemColors.green:
                        newObject = Instantiate(bomb[1]);
                        break;
                    case gemColors.orange:
                        newObject = Instantiate(bomb[2]);
                        break;
                    case gemColors.purple:
                        newObject = Instantiate(bomb[3]);
                        break;
                    case gemColors.red:
                        newObject = Instantiate(bomb[4]);
                        break;
                    case gemColors.yellow:
                        newObject = Instantiate(bomb[5]);
                        break;
                }
            }

            // else if(inGameBoosterInstanceData.type==InGameBoosterInstanceBlackBoard.InGameBoosterType.bomb)
            //     newObject = Instantiate(bomb[1]);
            // else
            //     newObject = Instantiate(lightning[2]);

            newObject.transform.SetParent(boardGame.transform, false);
            newObject.transform.localScale = new Vector3(1, 1, 1);
            newObject.GetComponent<gemTilePresenter>().setup(tileStack, gameplayController);


            // Debug.Log($"Start TopInstanceing {cellStack1.Position()} and {cellStack2.Position()} ");

            // await Task.Delay(500);

            // Debug.Log($"Finished TopInstanceing {cellStack1.Position()} and {cellStack2.Position()} ");

            onCompleted.Invoke();
        }
    }
}