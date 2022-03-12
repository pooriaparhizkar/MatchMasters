using UnityEngine;
using UnityEngine.SceneManagement;

public class BackBTNOnClick : MonoBehaviour
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

    public void BackBtn()
    {
        FirstPage.SetActive(true);
        SelectBooster.SetActive(false);
    }
}