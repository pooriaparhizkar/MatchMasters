using System;
using Nakama;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private readonly IClient client = new Client("http", "157.119.191.169", 7350, "defaultkey");

    [SerializeField] private Text percantText;

    // Start is called before the first frame update
    [SerializeField] private Slider progressBar;
    private ISession session;
    private ISocket socket;
    private float value;
    public static string APIAddress { get; set; }

    private void Start()
    {
        PlayerPrefs.SetString("API", "http://157.119.191.169:7351/v2/console/api/endpoints");
        value = 0.01f;
        // var deviceId = System.Guid.NewGuid().ToString();
        session = null;

        // client.WriteStorageObjectsAsync(session, new[] {new WriteStorageObject()});
    }

    // Update is called once per frame
    private async void Update()
    {
        if ((int) value * 100 < 100)
        {
            Debug.Log(session);
            if (value > 0.5f)
            {
                if (session == null)
                {
                    var deviceId = PlayerPrefs.GetString("nakama.deviceid");
                    if (string.IsNullOrEmpty(deviceId))
                    {
                        deviceId = SystemInfo.deviceUniqueIdentifier;
                        PlayerPrefs.SetString("nakama.deviceid", deviceId); // cache device id.
                    }

                   // session = await client.AuthenticateDeviceAsync(deviceId);
                   session = await client.AuthenticateDeviceAsync("pooria-pooria-pooria-pooria-pooria-pooria-pooria-pooria-pooria-pooria2");
                    PlayerPrefs.SetString("token", session.AuthToken);
                    PlayerPrefs.SetString("username", session.Username);
                }
                else
                {
                    ContinueProgess();
                }
            }
            else if (session == null)
            {
                ContinueProgess();
            }
        }
        else
        {
            SceneManager.LoadScene("MainApp");
        }
    }

    private void ContinueProgess()
    {
        progressBar.value = value;
        percantText.text = Math.Round(value * 100, 0).ToString() + '%';
        value += 0.1f * Time.deltaTime;
    }
}