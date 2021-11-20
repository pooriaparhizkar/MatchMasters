using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Nakama;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class onDragGems : MonoBehaviour, IDragHandler, IEndDragHandler
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


    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraging)
        {
            int sourceObject = int.Parse(eventData.pointerDrag.name.Split('_')[1]);
            int targetObject = 0;
            //Up :
            if (eventData.delta.y > 0)
            {
                if (sourceObject > 7)
                {
                    targetObject = sourceObject - 7;
                }
            }
            //Down :
            else if (eventData.delta.y < 0)
            {
                if (sourceObject < 43)
                {
                    targetObject = sourceObject + 7;
                }
            }

            //Right:
            else if (eventData.delta.x > 0)
            {
                if (sourceObject != 7 && sourceObject != 14 && sourceObject != 21 && sourceObject != 28 &&
                    sourceObject != 35 && sourceObject != 42 && sourceObject != 49)
                {
                    targetObject = sourceObject + 1;
                }
            }

            //Left:
            else if (eventData.delta.x < 0)
            {
                if (sourceObject != 1 && sourceObject != 8 && sourceObject != 15 && sourceObject != 22 &&
                    sourceObject != 29 && sourceObject != 36 && sourceObject != 43)
                {
                    targetObject = sourceObject - 1;
                }
            }

            if (targetObject != 0)
            {
                GameObject sourceElement = GameObject.Find("p_" + sourceObject);
                Debug.Log(sourceElement.transform.GetChild(0));
                if (sourceElement.transform.GetChild(0))
                {
                    GameObject sourceGem = sourceElement.transform.GetChild(0).gameObject;
                    GameObject targetElement = GameObject.Find("p_" + targetObject);
                    GameObject targetGem = targetElement.transform.GetChild(0).gameObject;
                    targetGem.transform.SetParent(sourceElement.transform);
                    targetGem.transform.position = sourceElement.transform.position;
                    sourceGem.transform.SetParent(targetElement.transform);
                    sourceGem.transform.position = targetElement.transform.position;

                    checkThree(sourceObject);
                    checkThree(targetObject);
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
    public void checkThree(int index)
    {
        checkThreeRow(index);
        checkThreeCol(index);
    }

    public void checkThreeRow(int index)
    {
        string sourceName = getNameGemWithIndex(index);
        //+1 , -1
        if (sourceName == getNameGemWithIndex(index - 1) &&
            sourceName == getNameGemWithIndex(index + 1))
        {
            if (((index - 1) % 7 != 0) && ((index) % 7 != 0))
            {
                destroyGemWithIndex(index);
                isLeftDestroy(index);
                isRightDestroy(index);
            }
        }
        //-1 , -2
        else if (sourceName == getNameGemWithIndex(index - 1) &&
                 sourceName == getNameGemWithIndex(index - 2))
        {
            if (((index - 1) % 7 != 0) && ((index - 2) % 7 != 0))
            {
                destroyGemWithIndex(index);
                isLeftDestroy(index);
            }
        }
        //+1 , +2
        else if (sourceName == getNameGemWithIndex(index + 1) &&
                 sourceName == getNameGemWithIndex(index + 2))
        {
            if (((index) % 7 != 0) && ((index + 1) % 7 != 0))
            {
                destroyGemWithIndex(index);
                isRightDestroy(index);
            }
        }
    }

    public void checkThreeCol(int index)
    {
        string sourceName = getNameGemWithIndex(index);
        //+7 , -7
        if (((index - 7) > 0) && ((index + 7) < 49))
        {
            if (sourceName == getNameGemWithIndex(index - 7) &&
                sourceName == getNameGemWithIndex(index + 7))
            {
                destroyGemWithIndex(index);
                isUpDestroy(index);
                isDownDestroy(index);
            }
        }
        //-7 , -14
        if (index -14 >0)
        {
            if (sourceName == getNameGemWithIndex(index - 7) &&
                sourceName == getNameGemWithIndex(index - 14))
            {
                destroyGemWithIndex(index);
                isUpDestroy(index);
                isDownDestroy(index);
            }
        }
        //+7 , +14
        if (index +14 <49)
        {
            if (sourceName == getNameGemWithIndex(index + 7) &&
                sourceName == getNameGemWithIndex(index + 14))
            {
                destroyGemWithIndex(index);
                isUpDestroy(index);
                isDownDestroy(index);
            }
        }

    }

    public void isLeftDestroy(int index)
    {
        string source = getNameGemWithIndex(index);
        if (getNameGemWithIndex(index - 1) == source && ((index - 1) % 7 != 0))
        {
            destroyGemWithIndex(index - 1);
            isLeftDestroy(index - 1);
        }
    }

    public void isRightDestroy(int index)
    {
        string source = getNameGemWithIndex(index);
        if (getNameGemWithIndex(index + 1) == source && ((index + 1) % 7 != 1))
        {
            destroyGemWithIndex(index + 1);
            isRightDestroy(index + 1);
        }
    }

    public void isUpDestroy(int index)
    {
        string source = getNameGemWithIndex(index);
        if (index-7 >0)
        {
            if (getNameGemWithIndex(index - 7) == source)
            {
                destroyGemWithIndex(index - 7);
                isUpDestroy(index - 7);
            }
        }
    }
    public void isDownDestroy(int index)
    {
        string source = getNameGemWithIndex(index);
        if (index+7 <49)
        {
            if (getNameGemWithIndex(index + 7) == source)
            {
                destroyGemWithIndex(index + 7);
                isDownDestroy(index + 7);
            }
        }
    }

    public string getNameGemWithIndex(int index)
    {
        GameObject element = GameObject.Find("p_" + index);
        GameObject gem = element.transform.GetChild(0).gameObject;
        return gem.name.Split('_')[0];
    }

    public void destroyGemWithIndex(int index)
    {
        GameObject element = GameObject.Find("p_" + index);
        GameObject gem = element.transform.GetChild(0).gameObject;
        Destroy(gem);
    }


}