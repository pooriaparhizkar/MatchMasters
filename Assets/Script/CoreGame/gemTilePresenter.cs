using System;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using Sample;
using Script.CoreGame;
using UnityEngine;
using UnityEngine.EventSystems;
using Component = Medrick.ComponentSystem.Core.Component;

public class gemTilePresenter : SwapBlackBoard, Component, IDragHandler, IEndDragHandler,IPointerClickHandler
{
    private BasicGameplayMainController _gameplayMainController;

    // private OrderSwapSystemPresentationPort presentationPort;
    // Start is called before the first frame update
    private TileStack _tileStack;
    private bool isDraging;

    // public void Start()
    // {
    //     presentationPort = gameplayController.GetPresentationPort<SwapSystemPresentationPort>();
    // }

    public void setup(TileStack tileStack, BasicGameplayMainController sampleGameplayMainController)
    {
        tileStack.AddComponent(this);
        _tileStack = tileStack;
        _gameplayMainController = sampleGameplayMainController;
    }



    public void delete(TileStack tileStack)
    {
       // tileStack.Destroy();
        Destroy(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (turnHandler.isMyTurn())
        {
                if (!_gameplayMainController.LevelBoard.CellStackBoard().isBoardLock())
        {
            // presentationPort = _gameplayMainController.GetPresentationPort<OrderSwapSystemPresentationPort>();
            var firstPos = new Vector2Int((int) _tileStack.Position().x, (int) _tileStack.Position().y);
            var secondPos=new Vector2Int(-100,-100);

            if (!isDraging)
            {
                isDraging = true;
                //Up
                if (eventData.delta.y > 5 && Math.Abs(eventData.delta.y)  > Math.Abs(eventData.delta.x) &&   (int) _tileStack.Position().y > 0)
                    secondPos = new Vector2Int((int) _tileStack.Position().x, (int) _tileStack.Position().y - 1);
                //Down
                else if (eventData.delta.y < -5 && Math.Abs(eventData.delta.y)  > Math.Abs(eventData.delta.x) &&  (int) _tileStack.Position().y < 6)
                    secondPos = new Vector2Int((int) _tileStack.Position().x, (int) _tileStack.Position().y + 1);
                //Left
                else if (eventData.delta.x < -2 && Math.Abs(eventData.delta.y)  < Math.Abs(eventData.delta.x) &&  (int) _tileStack.Position().x > 0)
                    secondPos = new Vector2Int((int) _tileStack.Position().x - 1, (int) _tileStack.Position().y);
                //Right
                else if (eventData.delta.x > 2 && Math.Abs(eventData.delta.y)  < Math.Abs(eventData.delta.x) && (int) _tileStack.Position().x < 6)
                    secondPos = new Vector2Int((int) _tileStack.Position().x + 1, (int) _tileStack.Position().y);

                // presentationPort.OrderSwap(firstPos, secondPos, () => Debug.Log("finished"));
                if (secondPos.x != -100)
                {
                    _gameplayMainController.FrameBasedBlackBoard.GetComponent<SwapBlackBoard>().requestedSwaps
                        .Add(new SwapData(firstPos, secondPos,true));
                    socketLogic.sendChat("1", firstPos.ToString(), secondPos.ToString());

                }

            }
        }
        }


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        perkHandler.hammerPerkGemClicked(new Vector2Int((int) _tileStack.Position().x, (int) _tileStack.Position().y));

    }
}