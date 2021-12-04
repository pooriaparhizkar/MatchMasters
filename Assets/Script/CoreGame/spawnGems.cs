using Medrick.Match3CoreSystem.Game.Core;
using Sample;
using UnityEngine;

public class spawnGems : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] gems;
    public GameObject boardGame;
    public SystemSwapPresentationAdapter systemSwapPresentationAdapter;
    public SystemDestroyPresentationAdapter systemDestroyPresentationAdapter;

    private readonly gemColors[,] template1 = new gemColors[7, 7]
    {
        {
            gemColors.yellow, gemColors.orange, gemColors.blue, gemColors.yellow, gemColors.orange, gemColors.red,
            gemColors.orange
        },
        {
            gemColors.green, gemColors.purple, gemColors.orange, gemColors.red, gemColors.orange, gemColors.blue,
            gemColors.red
        },
        {
            gemColors.orange, gemColors.red, gemColors.purple, gemColors.blue, gemColors.green, gemColors.purple,
            gemColors.blue
        },
        {
            gemColors.red, gemColors.yellow, gemColors.purple, gemColors.blue, gemColors.yellow, gemColors.purple,
            gemColors.orange
        },
        {
            gemColors.red, gemColors.purple, gemColors.green, gemColors.purple, gemColors.green, gemColors.yellow,
            gemColors.purple
        },
        {
            gemColors.orange, gemColors.blue, gemColors.red, gemColors.yellow, gemColors.orange, gemColors.green,
            gemColors.red
        },
        {
            gemColors.purple, gemColors.green, gemColors.yellow, gemColors.blue, gemColors.yellow, gemColors.blue,
            gemColors.yellow
        }
    };

    private SampleGameplayMainController gameplayController;

    private void Start()
    {
        var cellStackFactory = new MainCellStackFactory();
        var tileStackFactory = new MainTileStackFactory();


        gameplayController = new SampleGameplayMainController(
            CreateLevelBoard(cellStackFactory, tileStackFactory),
            tileStackFactory);

        // It is a good practive to add PresentationPorts before the Start.
        gameplayController.AddPresentationPort(systemSwapPresentationAdapter);
        gameplayController.AddPresentationPort(systemDestroyPresentationAdapter);
        foreach (var cellStack in gameplayController.LevelBoard.leftToRightTopDownCellStackArray)
            if (cellStack.HasTileStack())
            {
                var tileStack = cellStack.CurrentTileStack();
                var gemTile = tileStack.Top() as gemTile;


                switch (gemTile._color)
                {
                    case gemColors.blue:
                        myInctanciate(gems[3], tileStack);
                        break;
                    case gemColors.orange:
                        myInctanciate(gems[0], tileStack);
                        break;
                    case gemColors.purple:
                        myInctanciate(gems[1], tileStack);
                        break;
                    case gemColors.red:
                        myInctanciate(gems[2], tileStack);
                        break;
                    case gemColors.yellow:
                        myInctanciate(gems[4], tileStack);
                        break;
                    case gemColors.green:
                        myInctanciate(gems[5], tileStack);
                        break;
                }
            }

        gameplayController.Start();
    }


    // Update is called once per frame
    private void Update()
    {
        gameplayController.Update(Time.deltaTime);
        foreach (var cellStack in gameplayController.LevelBoard.leftToRightTopDownCellStackArray)
            if (cellStack.HasTileStack())
            {
                var tileStack = cellStack.CurrentTileStack();
                var presenter = tileStack.GetComponent<gemTilePresenter>();
                presenter.transform.localPosition = logicalPositionToPresentation(tileStack.Position());
            }
    }

    private LevelBoard CreateLevelBoard(MainCellStackFactory cellStackFactory, MainTileStackFactory tileStackFactory)
    {
        var width = 7;
        var height = 7;

        var cellStackBoard = new CellStackBoard(width, height);


        for (var i = 0; i < width; ++i)
        for (var j = 0; j < height; ++j)
        {
            var cellStack = cellStackFactory.Create(i, j);
            var tileStack = tileStackFactory.Create();
            cellStack.SetCurrnetTileStack(tileStack);
            tileStack.SetPosition(cellStack.Position());
            cellStackBoard[i, j] = cellStack;

            SetupCells(cellStack);
            SetupTiles(tileStack, template1[j, i]);
        }

        return new LevelBoard(cellStackBoard);
    }

    private void SetupCells(CellStack cellStack)
    {
        // You can use cellStack.Push() to push your cells;
        // You can use cellStack.Attach() to attach your attachments.

        cellStack.Push(new emptyCell());
    }

    private void SetupTiles(TileStack tileStack, gemColors color)
    {
        tileStack.Push(new gemTile(color));
        // You can use tileStack.Push() to push your tiles;
    }

    private Vector3 logicalPositionToPresentation(Vector2 pos)
    {
        return new Vector3((pos.x - 3) * 112, (pos.y - 3.2f) * -98,
            transform.position.z);
    }

    private void myInctanciate(GameObject gemPrefabs, TileStack tileStack)
    {
        GameObject newObject = null;
        newObject = Instantiate(gemPrefabs,
            logicalPositionToPresentation(tileStack.Position()), Quaternion.identity);
        newObject.transform.SetParent(boardGame.transform, false);
        newObject.transform.localScale = new Vector3(1, 1, 1);
        newObject.GetComponent<gemTilePresenter>().setup(tileStack,gameplayController);
    }
}