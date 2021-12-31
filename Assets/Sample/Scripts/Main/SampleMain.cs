using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;

namespace Sample
{
    public class SampleMain : MonoBehaviour
    {
        public SystemSwapPresentationAdapter systemTwoPresentationAdapter;

        private SampleGameplayMainController gameplayController;

        private void Awake()
        {
            var cellStackFactory = new MainCellStackFactory();
            var tileStackFactory = new MainTileStackFactory();


            gameplayController = new SampleGameplayMainController(
                CreateLevelBoard(cellStackFactory, tileStackFactory),
                tileStackFactory);

            // It is a good practive to add PresentationPorts before the Start.
            gameplayController.AddPresentationPort(systemTwoPresentationAdapter);

            gameplayController.Start();
        }


        // NOTE: You shoud provide the update for the gameplayController
        private void Update()
        {
            gameplayController.Update(Time.deltaTime);
        }


        // You should define your own system for Creating the LevelBoard base on your level config.
        private LevelBoard CreateLevelBoard(MainCellStackFactory cellStackFactory,
            MainTileStackFactory tileStackFactory)
        {
            var width = 10;
            var height = 5;

            var cellStackBoard = new CellStackBoard(width, height);


            for (var i = 0; i < width; ++i)
            for (var j = 0; j < height; ++j)
            {
                var cellStack = cellStackFactory.Create(i, j);
                var tileStack = tileStackFactory.Create();
                cellStack.SetCurrnetTileStack(tileStack);
                tileStack.SetPosition(cellStack.Position());
                cellStackBoard[i, j] = cellStack;

                // SetupCells(cellStack);
                // SetupTiles(tileStack);
            }

            return new LevelBoard(cellStackBoard);
        }

        // private void SetupCells(CellStack cellStack)
        // {
        //     // You can use cellStack.Push() to push your cells;
        //     // You can use cellStack.Attach() to attach your attachments.
        //
        //     cellStack.Push(new emptyCell());
        // }
        //
        // private void SetupTiles(TileStack tileStack)
        // {
        //     tileStack.Push(new gemTile((gemColors) Random.Range(0, 4)));
        //     // You can use tileStack.Push() to push your tiles;
        // }
    }

    public class emptyCell : Cell
    {
    }

    public enum gemColors
    {

        orange,
        purple,
        red,
        blue,
        yellow,
        green
    }
    public enum gemTypes
    {

        normal,
        arrow
    }

    public class gemTile : Tile
    {
        public gemColors _color;
        public gemTypes _gemTypes;

        public gemTile(gemColors color,gemTypes gemTypes)
        {
            _color = color;
            _gemTypes = gemTypes;
        }
    }
}