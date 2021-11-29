using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;

namespace Sample
{

    public class SampleMain : MonoBehaviour
    {
        public SystemTwoPresentationAdapter systemTwoPresentationAdapter;

        SampleGameplayMainController gameplayController;

        void Awake()
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
        private LevelBoard CreateLevelBoard(MainCellStackFactory cellStackFactory, MainTileStackFactory tileStackFactory)
        {
            int width = 10;
            int height = 5;

            var cellStackBoard = new CellStackBoard(width, height);


            for(int i = 0; i < width; ++i)
                for(int j = 0; j < height; ++j)
                {
                    var cellStack = cellStackFactory.Create(i, j);
                    var tileStack = tileStackFactory.Create();
                    cellStack.SetCurrnetTileStack(tileStack);
                    tileStack.SetPosition(cellStack.Position());
                    cellStackBoard[i, j] = cellStackFactory.Create(i, j);

                    SetupCells(cellStack);
                    SetupTiles(tileStack);
                }

            return new LevelBoard(cellStackBoard);
        }

        private void SetupCells(CellStack cellStack)
        {
            // You can use cellStack.Push() to push your cells;
            // You can use cellStack.Attach() to attach your attachments.
        }

        private void SetupTiles(TileStack tileStack)
        {
            // You can use tileStack.Push() to push your tiles;
        }
    }
}