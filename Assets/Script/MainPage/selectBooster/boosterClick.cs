using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class boosterClick : MonoBehaviour,IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            
                foreach (var VARIABLE in  GameObject.FindGameObjectsWithTag("BoosterSelect"))
                {
                    VARIABLE.gameObject.SetActive(false);
                }
                if ( eventData.pointerEnter.transform.parent.GetChild(0).gameObject.activeSelf)
                    eventData.pointerEnter.transform.parent.GetChild(1).gameObject.SetActive(true);
        }
    }
}