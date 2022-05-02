using Script.CoreGame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class playBtnClickOnBoosterSelected: MonoBehaviour,IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(eventData.pointerEnter.transform.parent.parent.name);
            turnHandler.setMyBoosterName(eventData.pointerEnter.transform.parent.parent.name);
            SceneManager.LoadScene("MatchMaking");
        }
    }
}