using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nakama;
using Script.CoreGame;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class MatchMakingLogic : MonoBehaviour
{
    public GameObject rightLight;

    public GameObject leftLight;

    // Start is called before the first frame update
    private readonly IClient client = new Client("http", "157.119.191.169", 7350, "defaultkey");
    private IMatch currentMatch;
    private string currentMatchId;
    private string currentMatchmakingTicket;
    private bool directRotate = true;
    public Text findingText;
    private string foundedName;
    private string foundSessionID;
    public Text foundText;
    private IUserPresence localUser;
    private string myUsername;
    private IDictionary<string, GameObject> players;
    private ISession session;
    public static ISocket socket;
    public static String gameMatchicket;
    public static string[] usersInGame;
    public static string mySessionID;
    private bool isRedirecting;

    private async void Start()
    {
        isRedirecting = false;
        StartCoroutine(ChangeFindingText());

        // var deviceId = PlayerPrefs.GetString("nakama.deviceid");
        // if (string.IsNullOrEmpty(deviceId)) {
        //     deviceId = SystemInfo.deviceUniqueIdentifier;
        //     PlayerPrefs.SetString("nakama.deviceid", deviceId); // cache device id.
        // }
        // Debug.Log(deviceId);
        //session = await client.AuthenticateDeviceAsync(deviceId);
        var session = Session.Restore(PlayerPrefs.GetString("token"));
        //session = await client.AuthenticateDeviceAsync("pooria-pooria-pooria-pooria-pooria-pooria-pooria-pooria-pooria");
        Debug.Log(session);
        socket = client.NewSocket();
        Debug.Log(socket);
        socket.Connected += () => Debug.Log("Socket Connected.");
        socket.Closed += () => Debug.Log("Socket closed");
        await socket.ConnectAsync(session);
        socket.ReceivedMatchmakerMatched += OnReceivedMatchmakerMatched;
        socket.ReceivedMatchPresence += m => OnReceivedMatchPresence(m);
        // socket.ReceivedMatchState += m => async ()=> await OnReceivedMatchState(m);
        Debug.Log(session.Username);
        myUsername = session.Username;
        mySessionID = session.UserId;
        await FindMatch();
    }

    private void Update()
    {
        if (directRotate)
        {
            leftLight.transform.Rotate(new Vector3(0, 0, 20) * Time.deltaTime);
            rightLight.transform.Rotate(new Vector3(0, 0, -20) * Time.deltaTime);
        }
        else
        {
            leftLight.transform.Rotate(new Vector3(0, 0, -20) * Time.deltaTime);
            rightLight.transform.Rotate(new Vector3(0, 0, 20) * Time.deltaTime);
        }

        if (leftLight.transform.rotation.z > 0.32f && directRotate)

            directRotate = false;


        if (leftLight.transform.rotation.z < -0.44f && !directRotate)

            directRotate = true;

        if (foundedName != null)
        {
            // Debug.Log("umad in tuuuuuuuuuuuuuuu");

            foundText.text = foundedName;
            if (!isRedirecting)
                RedirectAfterFound();
        }


        // rightLight.transform.Rotate(new Vector3(0, 0, -20) * Time.deltaTime);
    }

    public async void clickMe()
    {
        await CancelMatchmaking();
    }

    private void OnReceivedMatchPresence(IMatchPresenceEvent matchPresenceEvent)
    {
        gameMatchicket = matchPresenceEvent.MatchId;
        foreach (var user in matchPresenceEvent.Joins)
        {
            Debug.LogFormat("Connected user: " + user.Username);
            if (user.Username != myUsername)
            {
                Debug.Log("Peidaaaaa shod11111");
                foundSessionID = user.SessionId;
                foundedName = user.Username;
            }
        }

        // For each player that leaves, despawn their player.
        foreach (var user in matchPresenceEvent.Leaves)
            if (players.ContainsKey(user.SessionId))
            {
                Destroy(players[user.SessionId]);
                players.Remove(user.SessionId);
            }
    }

    // Update is called once per frame
    private IEnumerator ChangeFindingText()
    {
        findingText.text = findingText.text.Remove(0, 3);
        yield return new WaitForSecondsRealtime(0.5f);
        findingText.text = findingText.text.Insert(0, ".");
        yield return new WaitForSecondsRealtime(0.5f);
        findingText.text = findingText.text.Insert(0, ".");
        yield return new WaitForSecondsRealtime(0.5f);
        findingText.text = findingText.text.Insert(0, ".");
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(ChangeFindingText());
        // ChangeFindingText();
    }

    private async void RedirectAfterFound()
    {
        isRedirecting = true;
        usersInGame = new string[2] {myUsername, foundedName};
        Array.Sort(usersInGame, StringComparer.InvariantCulture);
        Debug.Log(usersInGame[0]);
        Debug.Log(usersInGame[1]);
        Debug.Log(PlayerPrefs.GetString("username"));
        if (usersInGame[0] == PlayerPrefs.GetString("username"))
        {
            Random generateRandomSeed = new Random();
            int numberGenrateRandomSeed = generateRandomSeed.Next();
            int templateNo = generateRandomSeed.Next(1, 3);
            spawnGems.setTemplateNo(templateNo);
            spawnGems.setRandomSeed(numberGenrateRandomSeed);
            socketLogic.sendChat("0", "(" + numberGenrateRandomSeed.ToString() + ", 1)",
                "(" + templateNo.ToString() + ", 1)");
        }

        await Task.Delay(4000);
        SceneManager.LoadScene("CoreGame");
    }

    private async Task FindMatch(int minPlayers = 2)
    {
        var matchmakingProperties = new Dictionary<string, string>
        {
            {"engine", "unity"}
        };
        Debug.Log("Finding match");
        var matchmakerTicket = await socket.AddMatchmakerAsync("*", minPlayers, minPlayers, matchmakingProperties);
        currentMatchmakingTicket = matchmakerTicket.Ticket;
        Debug.Log(matchmakerTicket);
    }

    public async Task CancelMatchmaking()
    {
        await socket.RemoveMatchmakerAsync(currentMatchmakingTicket);
        SceneManager.LoadScene("MainApp");
    }

    private async void OnReceivedMatchmakerMatched(IMatchmakerMatched matched)
    {
        var match = await socket.JoinMatchAsync(matched);
        Debug.LogFormat("Our session id : " + match.Self.SessionId);

        Debug.Log(match.Presences.ToString());
        foreach (var user in match.Presences)
        {
            if (user.Username != myUsername)
            {
                foundSessionID = user.SessionId;
                foundedName = user.Username;
                Debug.Log("Peidaaaaa shod22222");
            }

            Debug.LogFormat("Connected user: " + user.Username);
        }

        currentMatch = match;
    }

    public void goCore()
    {
        SceneManager.LoadScene("CoreGame");
    }
}