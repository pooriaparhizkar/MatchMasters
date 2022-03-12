using UnityEngine;
using UnityEngine.SceneManagement;

public class playBtn : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public GameObject SelectBooster;
    public GameObject FirstPage;

    public void onPlayBtnClick()
    {
        SceneManager.LoadScene("MatchMaking");
    }
    public void homePlayBtn()
    {
        SelectBooster.SetActive(true);
        FirstPage.SetActive(false);
    }
}

