using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class headerMenu : MonoBehaviour
{
    public GameObject HeaderPopover;

    public GameObject useOnBlur;


    public void onClickHandler()
    {
        HeaderPopover.SetActive(!HeaderPopover.active);
        useOnBlur.SetActive(HeaderPopover.active);
    }

    public void useOnBlurCLickHandler()
    {
        HeaderPopover.SetActive(false);
        useOnBlur.SetActive(false);
    }
}