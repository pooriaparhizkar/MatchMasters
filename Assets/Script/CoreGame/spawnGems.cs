using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medrick.Match3CoreSystem.Game.Core;
using Nakama;
using Nakama.TinyJson;
using Sample;
using UnityEngine;
using Random = System.Random;

public class spawnGems : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] gems;
    public GameObject boardGame;
    public SystemSwapPresentationAdapter systemSwapPresentationAdapter;
    public SystemDestroyPresentationAdapter systemDestroyPresentationAdapter;
    public SystemPhysicPresentationAdaptor systemPhysicPresentationAdapter;
    public SystemTopIntancePresentationAdaptor systemTopInstancePresentationAdaptor;
    public SystemInGameBoosterInstancePresentationAdaptor systemInGameBoosterInstancePresentationAdaptor;
    public SystemPerkHandlerPresentationAdaptor systemPerkHandlerPresentationAdaptor;
    public SystemScorePresentationAdapter systemScorePresentationAdapter;

    public static Random randomSeed;
    private ISession session;
    private readonly IClient client = new Client("http", "157.119.191.169", 7350, "defaultkey");
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

    //done
    private readonly gemColors[,] template2 = new gemColors[7, 7]
    {
        {
            gemColors.orange, gemColors.green, gemColors.yellow, gemColors.purple, gemColors.red, gemColors.blue,
            gemColors.purple
        },
        {
            gemColors.yellow, gemColors.blue, gemColors.orange, gemColors.green, gemColors.purple, gemColors.orange,
            gemColors.red
        },
        {
            gemColors.yellow, gemColors.blue, gemColors.yellow, gemColors.red, gemColors.yellow, gemColors.purple,
            gemColors.yellow
        },
        {
            gemColors.red, gemColors.green, gemColors.blue, gemColors.yellow, gemColors.purple, gemColors.green,
            gemColors.blue
        },
        {
            gemColors.blue, gemColors.red, gemColors.blue, gemColors.orange, gemColors.red, gemColors.purple,
            gemColors.red
        },
        {
            gemColors.red, gemColors.red, gemColors.purple, gemColors.orange, gemColors.red, gemColors.yellow,
            gemColors.red
        },
        {
            gemColors.green, gemColors.purple, gemColors.green, gemColors.yellow, gemColors.orange, gemColors.green,
            gemColors.blue
        }
    };

    //done
    private readonly gemColors[,] template3 = new gemColors[7, 7]
    {
        {
            gemColors.green, gemColors.purple, gemColors.orange, gemColors.blue, gemColors.purple, gemColors.red,
            gemColors.yellow
        },
        {
            gemColors.red, gemColors.blue, gemColors.orange, gemColors.green, gemColors.purple, gemColors.orange,
            gemColors.green
        },
        {
            gemColors.yellow, gemColors.red, gemColors.blue, gemColors.orange, gemColors.blue, gemColors.red,
            gemColors.orange
        },
        {
            gemColors.blue, gemColors.yellow, gemColors.purple, gemColors.orange, gemColors.red, gemColors.yellow,
            gemColors.red
        },
        {
            gemColors.yellow, gemColors.purple, gemColors.red, gemColors.yellow, gemColors.blue, gemColors.yellow,
            gemColors.red
        },
        {
            gemColors.purple, gemColors.green, gemColors.purple, gemColors.yellow, gemColors.purple, gemColors.green,
            gemColors.blue
        },
        {
            gemColors.purple, gemColors.orange, gemColors.yellow, gemColors.purple, gemColors.green, gemColors.blue,
            gemColors.yellow
        }
    };

    //done
    private readonly gemColors[,] template4 = new gemColors[7, 7]
    {
        {
            gemColors.green, gemColors.purple, gemColors.orange, gemColors.blue, gemColors.purple, gemColors.red,
            gemColors.yellow
        },
        {
            gemColors.red, gemColors.blue, gemColors.orange, gemColors.green, gemColors.purple, gemColors.orange,
            gemColors.green
        },
        {
            gemColors.yellow, gemColors.red, gemColors.blue, gemColors.orange, gemColors.blue, gemColors.red,
            gemColors.orange
        },
        {
            gemColors.blue, gemColors.yellow, gemColors.purple, gemColors.orange, gemColors.red, gemColors.yellow,
            gemColors.red
        },
        {
            gemColors.yellow, gemColors.purple, gemColors.red, gemColors.yellow, gemColors.blue, gemColors.yellow,
            gemColors.red
        },
        {
            gemColors.purple, gemColors.green, gemColors.purple, gemColors.yellow, gemColors.purple, gemColors.green,
            gemColors.blue
        },
        {
            gemColors.purple, gemColors.orange, gemColors.yellow, gemColors.purple, gemColors.green, gemColors.blue,
            gemColors.yellow
        }
    };

    public static SampleGameplayMainController gameplayController;

    private void Start()
    {
        // getsomeData();
        randomSeed = new Random(10);
        var cellStackFactory = new MainCellStackFactory();
        var tileStackFactory = new MainTileStackFactory();


        gameplayController = new SampleGameplayMainController(
            CreateLevelBoard(cellStackFactory, tileStackFactory),
            tileStackFactory);

        // It is a good practive to add PresentationPorts before the Start.
        gameplayController.AddPresentationPort(systemSwapPresentationAdapter);
        gameplayController.AddPresentationPort(systemDestroyPresentationAdapter);
        gameplayController.AddPresentationPort(systemPhysicPresentationAdapter);
        gameplayController.AddPresentationPort(systemTopInstancePresentationAdaptor);
        gameplayController.AddPresentationPort(systemInGameBoosterInstancePresentationAdaptor);
        gameplayController.AddPresentationPort(systemPerkHandlerPresentationAdaptor);
        gameplayController.AddPresentationPort(systemScorePresentationAdapter);
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
                presenter.transform.localPosition = logicalPositionToPresentation(tileStack.Position(), true);
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
            SetupTiles(tileStack, template1[j, i], gemTypes.normal);
        }

        return new LevelBoard(cellStackBoard);
    }

    private void SetupCells(CellStack cellStack)
    {
        // You can use cellStack.Push() to push your cells;
        // You can use cellStack.Attach() to attach your attachments.

        cellStack.Push(new emptyCell());
    }

    private void SetupTiles(TileStack tileStack, gemColors color, gemTypes gemTypes)
    {
        tileStack.Push(new gemTile(color, gemTypes));
        // You can use tileStack.Push() to push your tiles;
    }

    private Vector3 logicalPositionToPresentation(Vector2 pos, bool isStart)
    {
        if (isStart)
        {
            return new Vector3((pos.x - 3) * 112, (pos.y - 3.7f) * -98,
                transform.position.z);
        }
        else
        {
            return new Vector3((pos.x - 3) * 112, (pos.y) * -98,
                transform.position.z);
        }

    }

    private void myInctanciate(GameObject gemPrefabs, TileStack tileStack)
    {
        GameObject newObject = null;
        newObject = Instantiate(gemPrefabs,
            logicalPositionToPresentation(tileStack.Position(), true), Quaternion.identity);
        newObject.transform.SetParent(boardGame.transform, false);
        newObject.transform.localScale = new Vector3(1, 1, 1);
        newObject.GetComponent<gemTilePresenter>().setup(tileStack, gameplayController);
    }
// }
    // private async void getsomeData()
    // {
    //     var session = Session.Restore(PlayerPrefs.GetString("token"));
    //     var myusername = session.Username;
    //     var oppenetuser=PlayerPrefs.GetString("opponentUser");
    //     Debug.Log(myusername);
    //     Debug.Log(oppenetuser);
    //     var searchRequest = new Dictionary<string, string>() { { "user1", myusername },{"user2",oppenetuser} };
    //     string payload = searchRequest.ToJson();
    //     string rpcid = "testload";
    //     Task<IApiRpc> load;
    //     load = client.RpcAsync(session,rpcid,payload);
    //     IApiRpc searchResult = await load;
    //     DataStruct testres = searchResult.Payload.FromJson<DataStruct>();
    //     Debug.Log(testres.templateId);
    //     Debug.Log(testres.seednumber);
    //
    // }
    // private struct DataStruct
    // {
    //     public int templateId;
    //     public float seednumber;
    //     public string host;
    // } 
}
