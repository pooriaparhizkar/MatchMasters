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

    public void OnDrag(PointerEventData eventData)
    {
        StartCoroutine(OnDragHandler(eventData));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
    }

    IEnumerator OnDragHandler(PointerEventData eventData)
    {
        if (!isDraging)
        {
            isDraging = true;
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
                swapGems(sourceObject, targetObject);
                yield return new WaitForSecondsRealtime(0.5f);
                if (!checkThree(sourceObject, targetObject))
                {
                    yield return new WaitForSecondsRealtime(0.5f);
                    swapGems(targetObject, sourceObject);
                }
            }
        }
    }


    public void swapGems(int sourceIndex, int targetIndex)
    {
        GameObject sourceElement2 = GameObject.Find("p_" + sourceIndex);
        if (sourceElement2.transform.GetChild(0))
        {
            GameObject sourceGem2 = sourceElement2.transform.GetChild(0).gameObject;
            GameObject targetElement2 = GameObject.Find("p_" + targetIndex);
            GameObject targetGem2 = targetElement2.transform.GetChild(0).gameObject;
            targetGem2.transform.SetParent(sourceElement2.transform);
            targetGem2.transform.position = sourceElement2.transform.position;
            sourceGem2.transform.SetParent(targetElement2.transform);
            sourceGem2.transform.position = targetElement2.transform.position;
        }
    }
    public void moveGems(int sourceIndex, int targetIndex)
    {
        GameObject sourceElement2 = GameObject.Find("p_" + sourceIndex);
        Debug.Log(sourceIndex);
        if (sourceElement2.transform.childCount!= 0 &&sourceElement2.transform.GetChild(0))
        {
            GameObject sourceGem2 = sourceElement2.transform.GetChild(0).gameObject;
            GameObject targetElement2 = GameObject.Find("p_" + targetIndex);
            //GameObject targetGem2 = targetElement2.transform.GetChild(0).gameObject;
            //targetGem2.transform.SetParent(sourceElement2.transform);
            //targetGem2.transform.position = sourceElement2.transform.position;
            sourceGem2.transform.SetParent(targetElement2.transform);
            sourceGem2.transform.position = targetElement2.transform.position;
        }
    }


    public bool checkThree(int sourceIndex, int targetIndex)
    {
        bool sourceRow = checkThreeRow(sourceIndex);
        bool sourceCol = checkThreeCol(sourceIndex);
        bool targetRow = checkThreeRow(targetIndex);
        bool targetCol = checkThreeCol(targetIndex);
        if (!sourceRow && !sourceCol && !targetRow && !targetCol)
        {
            return false;
        }

        return true;
    }

    public bool checkThreeRow(int index)
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
                return true;
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
                return true;
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
                return true;
            }
        }

        return false;
    }

    public bool checkThreeCol(int index)
    {
        string sourceName = getNameGemWithIndex(index);
        //+7 , -7

        if (sourceName == getNameGemWithIndex(index - 7) &&
            sourceName == getNameGemWithIndex(index + 7) && index - 7 > 0 && index + 7 < 49)
        {
            destroyGemWithIndex(index);
            isUpDestroy(index);
            isDownDestroy(index);
            return true;
        }
        else if (sourceName == getNameGemWithIndex(index - 7) &&
                 sourceName == getNameGemWithIndex(index - 14) && index - 14 > 0)
        {
            destroyGemWithIndex(index);
            isUpDestroy(index);
            isDownDestroy(index);
            return true;
        }

        //+7 , +14
        else if (sourceName == getNameGemWithIndex(index + 7) &&
                 sourceName == getNameGemWithIndex(index + 14) && index + 14 < 49)
        {
            destroyGemWithIndex(index);
            isUpDestroy(index);
            isDownDestroy(index);
            return true;
        }

        return false;
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
        if (index - 7 > 0)
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
        if (index + 7 < 49)
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
        if (index > 0 && index < 49)
        {
            GameObject element = GameObject.Find("p_" + index);
            GameObject gem = element.transform.GetChild(0).gameObject;
            return gem.name.Split('_')[0];
        }

        return "";
    }

    public void destroyGemWithIndex(int index)
    {

        GameObject element = GameObject.Find("p_" + index);
        GameObject gem = element.transform.GetChild(0).gameObject;
        Destroy(gem);
        //fillAfterDestroy(index);

    }

    public void fillAfterDestroy(int index)
    {
        if (index>7)
        {
            moveGems(index - 7, index);
            fillAfterDestroy(index - 7);
        }
    }
}