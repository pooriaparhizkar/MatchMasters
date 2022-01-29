using System.Collections;
using System.Collections.Generic;
using Sample;
using Script.CoreGame;
using UnityEngine;
using UnityEngine.UIElements;

public class perkHandler : spawnGems
{
    // Start is called before the first frame update

    private static bool perkHandlerClicked;

    public void hammerPerkClick()
    {

        perkHandlerClicked = true;
        gameplayController.FrameBasedBlackBoard.GetComponent<PerkHandlerBlackBoard>().requestedPerkHandlers
            .Add(new PerkHandlerBlackBoard.PerkHandlerData(PerkHandlerBlackBoard.PerkHandlerType.hammer,new Vector2Int(-100,-100),1));
        socketLogic.sendChat("2", "(1, 1)", "(1, 1)");

    }

    public static void hammerPerkGemClicked(Vector2Int position)
    {
        if (perkHandlerClicked)
        {
            // gameplayController.FrameBasedBlackBoard.GetComponent<DestroyBlackBoard>().requestedDestroys
            //     .Add(new DestroyBlackBoard.DestroyData(position));

            gameplayController.FrameBasedBlackBoard.GetComponent<PerkHandlerBlackBoard>().requestedPerkHandlers
                .Add(new PerkHandlerBlackBoard.PerkHandlerData(PerkHandlerBlackBoard.PerkHandlerType.hammer,position));


            perkHandlerClicked = false;
            // gameplayController.setPerkHandlerFalse();
            gameplayController.FrameBasedBlackBoard.GetComponent<PerkHandlerBlackBoard>().requestedPerkHandlers
                .Add(new PerkHandlerBlackBoard.PerkHandlerData(PerkHandlerBlackBoard.PerkHandlerType.hammer,new Vector2Int(-100,-100),0));

            socketLogic.sendChat("3", position.ToString(), "(1, 1)");

        }

    }


    void Start()
    {
        perkHandlerClicked = false;
    }
}
