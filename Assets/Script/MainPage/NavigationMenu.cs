using UnityEngine;

public class NavigationMenu : MonoBehaviour
{
    public GameObject FirstPage;
    public GameObject Shop;

    private void Start()
    {
        FirstPage.SetActive(true);
        Shop.SetActive(false);
    }

    private void Update()
    {
    }

    public void shopClicked()
    {
        FirstPage.SetActive(false);
        Shop.SetActive(true);
    }

    public void homeClicked()
    {
        FirstPage.SetActive(true);
        Shop.SetActive(false);
    }
}