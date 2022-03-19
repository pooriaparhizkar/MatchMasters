using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.CoreGame
{
    public class resultPageHandler : MonoBehaviour
    {
        public void onOkClick()
        {
            SceneManager.LoadScene("MainApp");
        }
    }
}