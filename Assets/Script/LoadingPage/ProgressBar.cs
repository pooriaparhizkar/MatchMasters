using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ProgressBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text loginText;
    [SerializeField] GameObject loading;
    [SerializeField] Text Percant;

    void Start()
    {
        loginText.enabled = false;
        loading.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(((RectTransform) transform).offsetMax.x < 1*120)
        {
            ((RectTransform) transform).offsetMax = new Vector2(((RectTransform) transform).offsetMax.x + 0.1f,
                ((RectTransform) transform).offsetMax.y);
            Percant.text = Math.Round((((((RectTransform) transform).offsetMax.x + 31 )*100/151)),2).ToString()+'%';
            //Percant.text = ((RectTransform) transform).offsetMax.x.ToString()+'%';
        }
        else
        {
            loginText.enabled = true;
            loading.SetActive(false);
        }
    }
}
