using System;
using System.Collections;
using System.Collections.Generic;
using Script.Types;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class selectBooster : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ufo;
    public GameObject paintBucket;
    public GameObject crazyRocket;
    public GameObject fireCracker;
    public GameObject mysteryHat;
    public GameObject laserBeam;
    public GameObject magicWand;
    public GameObject highVoltage;
    public GameObject colonelMcquack;
    public GameObject selectBoosterPage;
    public GameObject fistMainPage;
    private void activeBooster(GameObject gameObject,int count)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>().text = count.ToString();

    }
    private void deActiveBooster(GameObject gameObject)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }
    private void Start()
    {
        foreach (var VARIABLE in ProgressBar.myUserInfoDetail.userinfo.user.metadata.boosters)
        {
            if (VARIABLE.name.Equals("ufo"))
            {
                if (VARIABLE.count > 0) //active
                    activeBooster(ufo, VARIABLE.count);
                else //deActive
                    deActiveBooster(ufo);
            }
            if (VARIABLE.name.Equals("paintBucket"))
            {
                if (VARIABLE.count > 0) //active
                    activeBooster(paintBucket, VARIABLE.count);
                else //deActive
                    deActiveBooster(paintBucket);
            }
            if (VARIABLE.name.Equals("crazyRocket"))
            {
                if (VARIABLE.count > 0) //active
                    activeBooster(crazyRocket, VARIABLE.count);
                else //deActive
                    deActiveBooster(crazyRocket);
            }
            if (VARIABLE.name.Equals("fireCracker"))
            {
                if (VARIABLE.count > 0) //active
                    activeBooster(fireCracker, VARIABLE.count);
                else //deActive
                    deActiveBooster(fireCracker);
            }
            if (VARIABLE.name.Equals("mysteryHat"))
            {
                if (VARIABLE.count > 0) //active
                    activeBooster(mysteryHat, VARIABLE.count);
                else //deActive
                    deActiveBooster(mysteryHat);
            }
            if (VARIABLE.name.Equals("laserBeam"))
            {
                if (VARIABLE.count > 0) //active
                    activeBooster(laserBeam, VARIABLE.count);
                else //deActive
                    deActiveBooster(laserBeam);
            }
            if (VARIABLE.name.Equals("magicWand"))
            {
                if (VARIABLE.count > 0) //active
                    activeBooster(magicWand, VARIABLE.count);
                else //deActive
                    deActiveBooster(magicWand);
            }
            if (VARIABLE.name.Equals("highVoltage"))
            {
                if (VARIABLE.count > 0) //active
                    activeBooster(highVoltage, VARIABLE.count);
                else //deActive
                    deActiveBooster(highVoltage);
            }
            if (VARIABLE.name.Equals("colonelMcquack"))
            {
                if (VARIABLE.count > 0) //active
                    activeBooster(colonelMcquack, VARIABLE.count);
                else //deActive
                    deActiveBooster(colonelMcquack);
            }
        }

    }

    public void onPlayButtonClick()
    {
        SceneManager.LoadScene("MatchMaking");
    }
    public void onBackSelectBoosterClick()
    {
        selectBoosterPage.SetActive(false);
        fistMainPage.SetActive(true);
    }
}
