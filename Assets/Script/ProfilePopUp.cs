using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfilePopUp : MonoBehaviour
{
    public GameObject Profilemodal;

    public GameObject useOnBlur;


    public void Profilepopup()
    {
        Profilemodal.SetActive(!Profilemodal.active);
        useOnBlur.SetActive(Profilemodal.active);
    }

    public void useOnBlurCLickHandler()
    {
        Profilemodal.SetActive(false);
        useOnBlur.SetActive(false);
    }
}