using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Nakama;
using Nakama.TinyJson;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchMakingLogic : MonoBehaviour
{
    // Start is called before the first frame update
    private readonly IClient client = new Client("http", "157.119.191.169", 7350, "defaultkey");
    private ISocket socket;
    private ISession session;
    private string currentMatchmakingTicket;
    private string currentMatchId;
    private IDictionary<string, GameObject> players;
    private IUserPresence localUser;
    private IMatch currentMatch;
    public Text findingText;
    public GameObject rightLight;
    public GameObject leftLight;
    public Text foundText;
    private string myUsername;
    private string foundedName=null;
    async void Start()
    {
        StartCoroutine(ChangeFindingText());

        var deviceId = PlayerPrefs.GetString("nakama.deviceid");
        if (string.IsNullOrEmpty(deviceId)) {
            deviceId = SystemInfo.deviceUniqueIdentifier;
            PlayerPrefs.SetString("nakama.deviceid", deviceId); // cache device id.
        }
        Debug.Log(deviceId);
        session = await client.AuthenticateDeviceAsync(deviceId);
        //session = await client.AuthenticateDeviceAsync("pooria-pooria-pooria-pooria-pooria-pooria-pooria-pooria-pooria");
        Debug.Log(session);
        socket = client.NewSocket();
        Debug.Log(socket);
        socket.Connected += () => Debug.Log("Socket Connected.");
        socket.Closed += () => Debug.Log("Socket closed");
        await socket.ConnectAsync(session);
        socket.ReceivedMatchmakerMatched += OnReceivedMatchmakerMatched;
        socket.ReceivedMatchPresence += m =>  OnReceivedMatchPresence(m);
        // socket.ReceivedMatchState += m => async ()=> await OnReceivedMatchState(m);
        Debug.Log(session.Username);
        myUsername = session.Username;
        await FindMatch();
    }
    public async void clickMe()
    {

        await CancelMatchmaking();
    }

    private void OnReceivedMatchPresence(IMatchPresenceEvent matchPresenceEvent)
    {
        foreach (var user in matchPresenceEvent.Joins)
        {
            Debug.LogFormat("Connected user: " + user.Username);
            if (user.Username != myUsername)
            {
                Debug.Log("Peidaaaaa shod");
                foundedName = user.Username;
            }
        }

        // For each player that leaves, despawn their player.
        foreach (var user in matchPresenceEvent.Leaves)
        {
            if (players.ContainsKey(user.SessionId))
            {
                Destroy(players[user.SessionId]);
                players.Remove(user.SessionId);
            }
        }
    }
    // Update is called once per frame
    IEnumerator ChangeFindingText()
    {
        findingText.text=findingText.text.Remove(0, 3);
        yield return new WaitForSecondsRealtime(0.5f);
        findingText.text =findingText.text.Insert(0,".");
        yield return new WaitForSecondsRealtime(0.5f);
        findingText.text =findingText.text.Insert(0,".");
        yield return new WaitForSecondsRealtime(0.5f);
        findingText.text =findingText.text.Insert(0,".");
        yield return new WaitForSecondsRealtime(0.5f);
         StartCoroutine(ChangeFindingText());
         // ChangeFindingText();
    }
    IEnumerator RedirectAfterFound()
    {
        yield return new WaitForSecondsRealtime(04);
        SceneManager.LoadScene (sceneName:"CoreGame");
    }
    private bool directRotate = true;
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

        if ( leftLight.transform.rotation.z > 0.32f && directRotate)

            directRotate = false;


        if (leftLight.transform.rotation.z < -0.44f && !directRotate)

            directRotate = true;

        if (foundedName != null)
        {
            Debug.Log("umad in tuuuuuuuuuuuuuuu");
            foundText.text = foundedName;
            StartCoroutine(RedirectAfterFound());
        }


        // rightLight.transform.Rotate(new Vector3(0, 0, -20) * Time.deltaTime);
    }

    async Task FindMatch(int minPlayers = 2)
    {
        var matchmakingProperties = new Dictionary<string, string>
        {
            { "engine", "unity" }
        };
        Debug.Log("Finding match");
        var matchmakerTicket = await socket.AddMatchmakerAsync("*", minPlayers, minPlayers, matchmakingProperties);
        currentMatchmakingTicket = matchmakerTicket.Ticket;
        Debug.Log(matchmakerTicket);
    }
    public async Task CancelMatchmaking()
    {
        await socket.RemoveMatchmakerAsync(currentMatchmakingTicket);
        SceneManager.LoadScene (sceneName:"MainApp");
    }
    private async void OnReceivedMatchmakerMatched(IMatchmakerMatched matched)
    {
        var match = await socket.JoinMatchAsync(matched);
        Debug.LogFormat("Our session id : " + match.Self.SessionId);

        Debug.Log( match.Presences.ToString());
        foreach (var user in match.Presences)
        {
            if (user.Username != myUsername)
            {
                Debug.Log("Peidaaaaa shod");
                foundedName = user.Username;
            }
            Debug.LogFormat("Connected user: " + user.Username);
        }
        currentMatch = match;
    }
}
