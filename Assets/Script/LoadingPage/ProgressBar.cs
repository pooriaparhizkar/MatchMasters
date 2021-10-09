using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(((RectTransform) transform).offsetMax.x < 1*120)
        ((RectTransform)transform).offsetMax = new Vector2(((RectTransform)transform).offsetMax.x + 0.1f, ((RectTransform)transform).offsetMax.y);
    }
}
