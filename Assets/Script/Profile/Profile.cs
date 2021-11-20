using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Profile : MonoBehaviour
{
    public Text username;// Start is called before the first frame update
    // void Start()
    // {
    //     username.text = PlayerPrefs.GetString("username");
    // }
    void Update(){
           username.text = PlayerPrefs.GetString("username");
    }

    // Update is called once per frame
   
}
