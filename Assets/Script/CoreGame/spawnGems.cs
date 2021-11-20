using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class spawnGems : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] gems;
    public GameObject boardGame;
    private GameObject[] boxSlots;
    void Start()
    {
        boxSlots = GameObject.FindGameObjectsWithTag("boxSlot");
        foreach (var VARIABLE in boxSlots)
        {
            GameObject newObject = Instantiate(gems[UnityEngine.Random.Range(0, 5)], VARIABLE.transform.position, Quaternion.identity) as GameObject;
            newObject.transform.SetParent(VARIABLE.transform);
            newObject.transform.localScale = new Vector3(1, 1, 1);
        }


    }

    void checkMatch()
    {
        boxSlots = GameObject.FindGameObjectsWithTag("boxSlot");
        foreach (var VARIABLE in boxSlots)
        {
            GameObject newObject = Instantiate(gems[UnityEngine.Random.Range(0, 5)], VARIABLE.transform.position, Quaternion.identity) as GameObject;
            newObject.transform.SetParent(VARIABLE.transform);
            newObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
