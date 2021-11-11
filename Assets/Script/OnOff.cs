using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{

    public AudioSource audioSource;
    public GameObject onBtn;
    public GameObject offBtn;
    

   void Start(){
       Debug.Log(PlayerPrefs.GetInt("music")==1);
        if(PlayerPrefs.GetInt("music")==1) {
             onBtn.SetActive(false);
            offBtn.SetActive(true);
            audioSource.mute=true;
            }
        else{ 
             onBtn.SetActive(true);
            offBtn.SetActive(false);
            audioSource.mute=false;
            }

        
    }

    public void Onoff()
    {
        
        //Debug.Log(audioSource.mute);
        if(!audioSource.mute){
            onBtn.SetActive(false);
            offBtn.SetActive(true);
            PlayerPrefs.SetInt("music", 1);
        }
        else{
            onBtn.SetActive(true);
            offBtn.SetActive(false);
             PlayerPrefs.SetInt("music", 0);
        }
	    PlayerPrefs.Save();
        audioSource.mute = !audioSource.mute;
    
    
    }
    


}