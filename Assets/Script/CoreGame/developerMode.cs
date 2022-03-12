using System.Collections;
using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using UnityEngine;

public class developerMode : MonoBehaviour
{
    public void isSwapBackLock()
    {
        ActionUtilites.setIsSwapBack(!ActionUtilites.getIsSwapBack());
    }
}
