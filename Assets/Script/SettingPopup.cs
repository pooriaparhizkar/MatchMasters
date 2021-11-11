using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPopup : MonoBehaviour
{
    public GameObject Settingmodal;

    public GameObject useOnBlur;


    public void SEttingpopoup()
    {
        Settingmodal.SetActive(!Settingmodal.active);
        useOnBlur.SetActive(Settingmodal.active);
    }

    public void useOnBlurCLickHandler()
    {
        Settingmodal.SetActive(false);
        useOnBlur.SetActive(false);
    }
}