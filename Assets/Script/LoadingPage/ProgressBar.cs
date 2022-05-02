using System;
using System.Threading.Tasks;
using Nakama;
using Nakama.TinyJson;
using Script.Types;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private static readonly IClient client = new Client("http", "157.119.191.169", 7350, "defaultkey");

    [SerializeField] private Text percantText;

    // Start is called before the first frame update
    [SerializeField] private Slider progressBar;
    private static ISession session;
    private ISocket socket;
    private float value;
    public static string APIAddress { get; set; }
    public static userInfoTypes.userInfoDetail myUserInfoDetail;


    public static async Task getUserInfoDetail()
    {
        string payload = "";
        string rpcid = "userinfo";
        Task<IApiRpc> userInfo;
        userInfo = client.RpcAsync(session, rpcid, payload);
        // Debug.Log(shop.Status);
        IApiRpc searchResult = await userInfo;
        //
        // Debug.Log(searchResult);
        userInfoTypes.userInfoDetail userInfoDetail = searchResult.Payload.FromJson<userInfoTypes.userInfoDetail>();
        myUserInfoDetail = userInfoDetail;
        Debug.Log(userInfoDetail.userinfo.user.metadata.trophy);
        Debug.Log(userInfoDetail.userinfo.user.userId);
    }
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
            // Debug.Log(session)
            Debug.Log(myUserInfoDetail.userinfo.user.metadata.coins);
            Debug.Log(myUserInfoDetail.userinfo.user.metadata.trophy);
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
                   session = await client.AuthenticateDeviceAsync("pooria-pooria-pooria-pooria-pooria-pooria-pooria-pooria-pooria-pooria22341");
                   await getUserInfoDetail();
                   PlayerPrefs.SetString("token", session.AuthToken);
                    PlayerPrefs.SetString("refresh_token", session.RefreshToken);
                    PlayerPrefs.SetString("username", session.Username);
                }
                else if(myUserInfoDetail.userinfo.user.userId != null)
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

    public void aghoojBUtton()
    {
        SceneManager.LoadScene("CoreGame");
    }
}