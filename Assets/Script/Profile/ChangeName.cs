using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nakama;
using Nakama.TinyJson;

public class ChangeName : MonoBehaviour
{
    public GameObject changenamegmodal;

    public GameObject useOnBlur;

    public GameObject canvasprofile;

    public GameObject darkbar;

    public Text newUsername;

    public GameObject BoxTaghirNam;

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
    private readonly IClient client = new Client("http", "157.119.191.169", 7350, "defaultkey");
    public async void BoxTaghirNamClickHandler()
        {
             Debug.Log("Changed");
        var session = Nakama.Session.Restore(PlayerPrefs.GetString("token"));
         await client.UpdateAccountAsync(session,newUsername.text, "", "", null, "");
         changenamegmodal.SetActive(false);
         useOnBlur.SetActive(false);
         canvasprofile.SetActive(false);
         PlayerPrefs.SetString("username",newUsername.text);
         Debug.Log("Changed");
        }
    

    void Update ()
    {
        if(darkbar && newUsername){
if(newUsername.text.Length > 3) 

            darkbar.SetActive(false);
        else 
            darkbar.SetActive(true);
        }
        
    }

}
