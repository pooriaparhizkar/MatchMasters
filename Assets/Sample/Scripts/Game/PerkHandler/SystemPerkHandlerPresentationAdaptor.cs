using System;
using System.Threading.Tasks;
using DG.Tweening;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;
using UnityEngine.UI;


namespace Sample
{
    public class SystemPerkHandlerPresentationAdaptor : MonoBehaviour, PerkHandlerSystemPresentationPort
    {
        public GameObject hammerPerkBlackScreen;
        public Text hammerPerkText;
        public GameObject hammerPerkL;
        public GameObject hammerPerkLDisable;
        public GameObject shufflePerkL;
        public GameObject shufflePerkLDisable;
        public GameObject hammerPerkR;
        public GameObject hammerPerkRDisable;
        public GameObject shufflePerkR;
        public GameObject shufflePerkRDisable;



        public void PlayPerkHandler(bool isStart, bool isMyPerk, PerkHandlerBlackBoard.PerkHandlerType type, Action onCompleted)
        {
           
            if (type==PerkHandlerBlackBoard.PerkHandlerType.hammer )
            {
                hammerPerkBlackScreen.SetActive(isStart);
                if (isMyPerk)
                    hammerPerkText.text = "ﻦﮐ ﺏﺎﺨﺘﻧﺍ ﺍﺭ ﻩﺮﻬﻣ ﮏﯾ";
                else
                    hammerPerkText.text = "ﺪﯿﻧﺎﻤﺑ ﺮﻈﺘﻨﻣ";
                
                if (!isStart) 
                {
                    if (isMyPerk)
                    {
                        hammerPerkL.SetActive(false);
                        hammerPerkLDisable.SetActive(true);
                    }
                    else
                    {
                        hammerPerkR.SetActive(false);
                        hammerPerkRDisable.SetActive(true);
                    }
                }
             
            }
            else if (type==PerkHandlerBlackBoard.PerkHandlerType.shuffle)
            {
                if (isMyPerk)
                {
                    shufflePerkL.SetActive(false);
                    shufflePerkLDisable.SetActive(true);
                }
                else
                {
                    shufflePerkR.SetActive(false);
                    shufflePerkRDisable.SetActive(true);
                }
            }
            onCompleted.Invoke();
        }
    }
}