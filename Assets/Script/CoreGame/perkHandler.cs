using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sample;
using Script.CoreGame;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public class perkHandler : spawnGems
{
    // Start is called before the first frame update

    private static bool perkHandlerClicked;
    

    public void hammerPerkClick()
    {
        perkHandlerClicked = true;
        gameplayController.FrameBasedBlackBoard.GetComponent<PerkHandlerBlackBoard>().requestedPerkHandlers
            .Add(new PerkHandlerBlackBoard.PerkHandlerData(PerkHandlerBlackBoard.PerkHandlerType.hammer,
                new Vector2Int(-100, -100), true,1));
        socketLogic.sendChat("2", "(1, 1)", "(1, 1)");
    }

    public static void hammerPerkGemClicked(Vector2Int position)
    {
        if (perkHandlerClicked)
        {
            // gameplayController.FrameBasedBlackBoard.GetComponent<DestroyBlackBoard>().requestedDestroys
            //     .Add(new DestroyBlackBoard.DestroyData(position));

            gameplayController.FrameBasedBlackBoard.GetComponent<PerkHandlerBlackBoard>().requestedPerkHandlers
                .Add(new PerkHandlerBlackBoard.PerkHandlerData(PerkHandlerBlackBoard.PerkHandlerType.hammer, position,true));


            perkHandlerClicked = false;
            // gameplayController.setPerkHandlerFalse();
            gameplayController.FrameBasedBlackBoard.GetComponent<PerkHandlerBlackBoard>().requestedPerkHandlers
                .Add(new PerkHandlerBlackBoard.PerkHandlerData(PerkHandlerBlackBoard.PerkHandlerType.hammer,
                    new Vector2Int(-100, -100), true,0));
            

            socketLogic.sendChat("3", position.ToString(), "(1, 1)");
        }
    }

    public static void doShuffle()
    {
      /*
       Stack<int> myStack = new Stack<int>();
        for (int i = 0; i < 49; i++)
            myStack.Push(i);
        List<int> list = myStack.ToList();
        var shuffled = list.OrderBy(x => spawnGems.randomSeed.Next()).ToList();
       myStack = new Stack<int>(shuffled);
        while (myStack.Count() != 1)
        {
            int number1 = myStack.Pop();
            int number2 = myStack.Pop();
            Vector2Int sourcePosiiton = new Vector2Int(number1 / 7, number1 % 7);
            Vector2Int targetPosition = new Vector2Int(number2 / 7, number2 % 7);
            gameplayController.FrameBasedBlackBoard.GetComponent<SwapBlackBoard>().requestedSwaps
                .Add(new SwapBlackBoard.SwapData(
                    sourcePosiiton, targetPosition,
                    false));
        }
        */
      gameplayController.FrameBasedBlackBoard.GetComponent<SwapBlackBoard>().requestedSwaps
          .Add(new SwapBlackBoard.SwapData(
              new Vector2Int(0, 0),  new Vector2Int(0, 1),
              false));
    }

    public void shuffleClicked()
    {
        doShuffle();
        gameplayController.FrameBasedBlackBoard.GetComponent<PerkHandlerBlackBoard>().requestedPerkHandlers
            .Add(new PerkHandlerBlackBoard.PerkHandlerData(PerkHandlerBlackBoard.PerkHandlerType.shuffle,
                new Vector2Int(-100, -100), true,0));
        socketLogic.sendChat("4", "(1, 1)", "(1, 1)");
    }


    void Start()
    {
        perkHandlerClicked = false;
    }
}