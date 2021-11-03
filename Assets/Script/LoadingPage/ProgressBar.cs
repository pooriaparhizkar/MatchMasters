using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Nakama;
using UnityEngine.SceneManagement;
public class ProgressBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider progressBar;
    [SerializeField] Text percantText;
    private float value;
    private readonly IClient client = new Client("http", "157.119.191.169", 7350, "defaultkey");
    private ISocket socket;
    private ISession session;

    void Start()
    {
        value = 0.01f;
        // var deviceId = System.Guid.NewGuid().ToString();
        session = null;

        // client.WriteStorageObjectsAsync(session, new[] {new WriteStorageObject()});
    }

    private void ContinueProgess()
    {
        progressBar.value = value;
        percantText.text = (Math.Round(value * 100, 0)).ToString() + '%';
        value += 0.001f;
    }

    // Update is called once per frame
    async void Update()
    {
        if ((int) value * 100 < 100)
        {
            Debug.Log(session);
            if (value > 0.5f)
            {
                if (session == null)
                    session = await client.AuthenticateDeviceAsync("pooria-pooria-pooria-pooria-pooria-pooria");
                else
                    ContinueProgess();
            }
            else if (session == null)
            {
                ContinueProgess();
            }
        }
        else
        {
            SceneManager.LoadScene (sceneName:"MainApp");
        }
    }
}