using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectBooster : MonoBehaviour
{
    // Start is called before the first frame update
    public void onPlayButtonClick()
    {
        SceneManager.LoadScene("MatchMaking");
    }
}
