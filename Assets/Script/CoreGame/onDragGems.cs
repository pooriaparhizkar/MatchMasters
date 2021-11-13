using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class onDragGems : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    // Start is called before the first frame update
    private bool isDraging = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(isDraging);
        if (!isDraging)
        {
            int sourceObject = int.Parse(eventData.pointerDrag.name.Split('_')[1]);
                   int targetObject=0;
                   //Up :
                   if (eventData.delta.y > 0)
                   {
                       if (sourceObject>7)
                       {
                           targetObject = sourceObject - 7;
                       }
                   }
                   //Down :
                   else if (eventData.delta.y < 0)
                   {
                       if (sourceObject<43)
                       {
                           targetObject = sourceObject + 7;
                       }
                   }

                   //Right:
                   else if (eventData.delta.x > 0)
                   {
                       if (sourceObject!= 7 &&sourceObject!= 14 &&sourceObject!= 21 &&sourceObject!= 28 &&sourceObject!= 35 &&sourceObject!= 42 &&sourceObject!= 49 )
                       {
                           targetObject = sourceObject + 1;
                       }
                   }

                   //Left:
                   else if (eventData.delta.x < 0)
                   {
                       if (sourceObject!= 1 &&sourceObject!= 8 &&sourceObject!= 15 &&sourceObject!= 22 &&sourceObject!= 29 &&sourceObject!= 36 &&sourceObject!= 43 )
                       {
                           targetObject = sourceObject - 1;
                       }
                   }

                   if (targetObject !=0)
                   {
                       GameObject sourceElement = GameObject.Find("p_" + sourceObject.ToString());
                       Debug.Log(sourceElement.transform.GetChild (0));
                       if (sourceElement.transform.GetChild (0))
                       {
                           GameObject  sourceGem = sourceElement.transform.GetChild (0).gameObject;
                           GameObject targetElement = GameObject.Find("p_" + targetObject.ToString());
                           GameObject  targetGem = targetElement.transform.GetChild (0).gameObject;
                            targetGem.transform.SetParent(sourceElement.transform);
                            targetGem.transform.position = sourceElement.transform.position;
                           sourceGem.transform.SetParent(targetElement.transform);
                           sourceGem.transform.position = targetElement.transform.position;
                       }


                       //  Destroy(targetGem);
                   }
                   isDraging = true;
        }



    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
    }
}
