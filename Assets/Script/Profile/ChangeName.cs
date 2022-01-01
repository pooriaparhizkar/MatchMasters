using Nakama;
using UnityEngine;
using UnityEngine.UI;

public class ChangeName : MonoBehaviour
{
    public GameObject changenamegmodal;

    public GameObject useOnBlur;

    public GameObject canvasprofile;

    public GameObject darkbar;

    public GameObject BoxTaghirNam;
    private readonly IClient client = new Client("http", "157.119.191.169", 7350, "defaultkey");

    public Text newUsername;


    private void Update()
    {
        if (darkbar && newUsername)
        {
            if (newUsername.text.Length > 3)

                darkbar.SetActive(false);
            else
                darkbar.SetActive(true);
        }
    }

    public void changename()
    {
        changenamegmodal.SetActive(!changenamegmodal.active);
        useOnBlur.SetActive(changenamegmodal.active);
        canvasprofile.SetActive(false);
        Debug.Log(newUsername.text);
    }

    public void useOnBlurCLickHandler()
    {
        changenamegmodal.SetActive(false);
        useOnBlur.SetActive(false);
        canvasprofile.SetActive(false);
    }

    public async void BoxTaghirNamClickHandler()
    {
        Debug.Log("Changed");
        var session = Session.Restore(PlayerPrefs.GetString("token"));
        await client.UpdateAccountAsync(session, newUsername.text, "", "", null, "");
        changenamegmodal.SetActive(false);
        useOnBlur.SetActive(false);
        canvasprofile.SetActive(false);
        PlayerPrefs.SetString("username", newUsername.text);
        Debug.Log("Changed");
    }
}