using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;
using Component = Medrick.ComponentSystem.Core.Component;

public class gemTilePresenter : MonoBehaviour, Component
{
    // Start is called before the first frame update
    public void setup(TileStack tileStack)
    {
        tileStack.AddComponent(this);
    }
}