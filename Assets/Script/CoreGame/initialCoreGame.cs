using UnityEngine;
using UnityEngine.UI;

namespace Script.CoreGame
{
    public class initialCoreGame : MonoBehaviour
    {
        public Text myName;
        public Text hisName;
        public GameObject myBoosterPaintBucket;
        public GameObject myBoosterUfo;
        public GameObject myBoosterCrazyRocket;
        public GameObject myBoosterFireCracker;
        public GameObject myBoosterMystryHat;
        public GameObject myBoosterLaserBeam;
        public GameObject myBoosterMagicWand;
        public GameObject myBoosterHighVoltage;
        public GameObject myBoosterColoneMcquack;
        public GameObject hisBoosterPaintBucket;
        public GameObject hisBoosterUfo;
        public GameObject hisBoosterCrazyRocket;
        public GameObject hisBoosterFireCracker;
        public GameObject hisBoosterMystryHat;
        public GameObject hisBoosterLaserBeam;
        public GameObject hisBoosterMagicWand;
        public GameObject hisBoosterHighVoltage;
        public GameObject hisBoosterColoneMcquack;
        
        private void Start()
        {
            switch (turnHandler.getMyBoosterName())
            {
                case "ufo":
                    myBoosterUfo.SetActive(true);
                    break;
                case "paintBucket":
                    myBoosterPaintBucket.SetActive(true);
                    break;
                case "crazyRocket":
                    myBoosterCrazyRocket.SetActive(true);
                    break;
                case "fireCracker":
                    myBoosterFireCracker.SetActive(true);
                    break;
                case "mysteryHat":
                    myBoosterMystryHat.SetActive(true);
                    break;
                case "laserBeam":
                    myBoosterLaserBeam.SetActive(true);
                    break;
                case "magicWand":
                    myBoosterMagicWand.SetActive(true);
                    break;
                case "highVoltage":
                    myBoosterHighVoltage.SetActive(true);
                    break;
                case "colonelMcquack":
                    myBoosterColoneMcquack.SetActive(true);
                    break;
            }
            switch (turnHandler.getHisBoosterName())
            {
                case "ufo":
                    hisBoosterUfo.SetActive(true);
                    break;
                case "paintBucket":
                    hisBoosterPaintBucket.SetActive(true);
                    break;
                case "crazyRocket":
                    hisBoosterCrazyRocket.SetActive(true);
                    break;
                case "fireCracker":
                    hisBoosterFireCracker.SetActive(true);
                    break;
                case "mysteryHat":
                    hisBoosterMystryHat.SetActive(true);
                    break;
                case "laserBeam":
                    hisBoosterLaserBeam.SetActive(true);
                    break;
                case "magicWand":
                    hisBoosterMagicWand.SetActive(true);
                    break;
                case "highVoltage":
                    hisBoosterHighVoltage.SetActive(true);
                    break;
                case "colonelMcquack":
                    hisBoosterColoneMcquack.SetActive(true);
                    break;
            }
            if (turnHandler.isMyTurn())
            {
                myName.text = turnHandler.getHostName();
                hisName.text = turnHandler.getClientName();
            }

            if (!turnHandler.isMyTurn())
            {
                hisName.text = turnHandler.getHostName();
                myName.text = turnHandler.getClientName();
            }
        }
    }
}