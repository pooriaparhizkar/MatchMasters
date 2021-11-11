using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public GameObject onooffmodal;

    public GameObject useOnBlur;


    public void Onoff()
    {
        onooffmodal.SetActive(!onooffmodal.active);
        useOnBlur.SetActive(onooffmodal.active);
    }

    public void useOnBlurCLickHandler()
    {
        Settingmodal.SetActive(false);
        useOnBlur.SetActive(false);
    }
}