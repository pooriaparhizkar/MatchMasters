﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nakama;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Text foundText;
    private IUserPresence localUser;
    private string myUsername;
    private IDictionary<string, GameObject> players;
    private ISession session;
    private ISocket socket;

    private async void Start()
    {
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
            Debug.Log("umad in tuuuuuuuuuuuuuuu");
            foundText.text = foundedName;
            StartCoroutine(RedirectAfterFound());
        }


        // rightLight.transform.Rotate(new Vector3(0, 0, -20) * Time.deltaTime);
    }

    public async void clickMe()
    {
        await CancelMatchmaking();
    }

    private void OnReceivedMatchPresence(IMatchPresenceEvent matchPresenceEvent)
    {
        Debug.Log(matchPresenceEvent);
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

    private IEnumerator RedirectAfterFound()
    {
        yield return new WaitForSecondsRealtime(04);
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
                Debug.Log("Peidaaaaa shod");
                foundedName = user.Username;
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