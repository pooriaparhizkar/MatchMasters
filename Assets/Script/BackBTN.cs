using UnityEngine;

public class BackBTN : MonoBehaviour
{
    public GameObject backbtngmodal;

    public GameObject useOnBlur;


    public void backbtn()
    {
        backbtngmodal.SetActive(!backbtngmodal.active);
        useOnBlur.SetActive(backbtngmodal.active);
    }

    public void useOnBlurCLickHandler()
    {
        backbtngmodal.SetActive(false);
        useOnBlur.SetActive(false);
    }
}