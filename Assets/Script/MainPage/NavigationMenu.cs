using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationMenu : MonoBehaviour
{
    public GameObject FirstPage;
    public GameObject Shop;
    void Start()
    {

        FirstPage.SetActive(true);
        Shop.SetActive(false);
    }

    void Update()
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
