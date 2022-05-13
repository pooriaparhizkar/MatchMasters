using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Medrick.Match3CoreSystem.Game;
using Nakama;
using Nakama.TinyJson;
using Sample;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Script.CoreGame
{
    [System.Serializable]
    public class sendMessageInfo
    {
        public string opcode;
        public string sourcePosition;
        public string targetPosition;
        public string number;
        public string boosterSelected;
        public string clientID;
     

        public static sendMessageInfo CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<sendMessageInfo>(jsonString);
        }
    }

    public class socketLogic : MonoBehaviour
    {
        private static ISocket mySocket;
        private static String myGameMatchicket;
        private static int counterSocketMessage;
        private static string[] lastSocketMessage;
        private static bool isContinueChecking;

        public static void setIsContinueCheckingTrue()
        {
            isContinueChecking = true;
        }
        public static void setIsContinueCheckingFalse()
        {
            isContinueChecking = false;
        }

        public async static void checkMessageSend()
        {
            if (isContinueChecking && !turnHandler.isMyTurn())
            {
                sendChat("8", "(1, 1)", "(1, 1)","","", (counterSocketMessage).ToString());
            }
            await Task.Delay(3000);
            checkMessageSend();
        }
        private void Start()
        {
            checkMessageSend();
            counterSocketMessage = 0;
            mySocket = MatchMakingLogic.socket;
            myGameMatchicket = MatchMakingLogic.gameMatchicket;

            // Use whatever decoder for your message contents.
            var enc = System.Text.Encoding.UTF8;
            mySocket.ReceivedMatchState += newState =>
            {
                
              
                // var content = newState.State.ToString();
                string content = enc.GetString(newState.State).ToString();


                var jsonContent = sendMessageInfo.CreateFromJSON(content);
                lastSocketMessage.Append( content);
                Vector2Int sourcePosiiton = new Vector2Int(
                    Int32.Parse(jsonContent.sourcePosition.Split(',')[0].Remove(0, 1)),
                    Int32.Parse(jsonContent.sourcePosition.Split(',')[1].Remove(0, 1).Substring(0, 1)));

                Vector2Int targetPosition = new Vector2Int(
                    Int32.Parse(jsonContent.targetPosition.Split(',')[0].Remove(0, 1)),
                    Int32.Parse(jsonContent.targetPosition.Split(',')[1].Remove(0, 1).Substring(0, 1)));
                
                Debug.Log(jsonContent.opcode);
                // if (jsonContent.opcode!="0")
                // {
                //     spawnGems.gameplayController.FrameBasedBlackBoard.GetComponent<DevkitBlackBoard>().requestedDevkits
                //         .Add(new DevkitBlackBoard.DevkitData(content));
                //
                // }

                counterSocketMessage = Int32.Parse(jsonContent.number);
                
                //opCodes :
                //0 : Initial ( sourcePos : randomSeed , TargetPos : templateID )
                //1 : swap
                //2 : Click on Hammer perk
                //3 : Activate Hammer perk with position
                //4 : Shuffle perk
                //5 : Booster
                //6 : Exit
                //7 : send clientID and boosterClient
                //8 : check missing message
                switch (jsonContent.opcode)
                {
                    case "0":
                        Debug.Log("initial maaap sockeeeeeet");
                        Debug.Log("dsakjhdvajkdhabvdhakhdbjva bkasdhj jsakdb ");
                        turnHandler.setHisBoosterName(jsonContent.boosterSelected);
                        turnHandler.setHisClientId(jsonContent.clientID);
                        spawnGems.setRandomSeed(sourcePosiiton.x);
                        spawnGems.setTemplateNo(targetPosition.x);
                        MatchMakingLogic.setIsClientReady();
                        // SceneManager.LoadScene("CoreGame");
                        break;

                    case "1":
                        spawnGems.gameplayController.FrameBasedBlackBoard.GetComponent<SwapBlackBoard>().requestedSwaps
                            .Add(new SwapBlackBoard.SwapData(
                                sourcePosiiton, targetPosition,
                                true));
                        break;

                    case "2":
                        //open the blackScreen of hammer perk
                        Debug.Log("perk 2222");
                        spawnGems.gameplayController.FrameBasedBlackBoard.GetComponent<PerkHandlerBlackBoard>()
                            .requestedPerkHandlers
                            .Add(new PerkHandlerBlackBoard.PerkHandlerData(PerkHandlerBlackBoard.PerkHandlerType.hammer,
                                new Vector2Int(-100, -100), false,1));
                        break;

                    case "3":
                        //Destroy position clicked
                        spawnGems.gameplayController.FrameBasedBlackBoard.GetComponent<PerkHandlerBlackBoard>()
                            .requestedPerkHandlers
                            .Add(new PerkHandlerBlackBoard.PerkHandlerData(PerkHandlerBlackBoard.PerkHandlerType.hammer,
                                sourcePosiiton,false));

                        //Close the blackScreen of hammer perk
                        spawnGems.gameplayController.FrameBasedBlackBoard.GetComponent<PerkHandlerBlackBoard>()
                            .requestedPerkHandlers
                            .Add(new PerkHandlerBlackBoard.PerkHandlerData(PerkHandlerBlackBoard.PerkHandlerType.hammer,
                                new Vector2Int(-100, -100), false,0));
                        break;
                    case "4":
                        perkHandler.doShuffle();
                        spawnGems.gameplayController.FrameBasedBlackBoard.GetComponent<PerkHandlerBlackBoard>()
                            .requestedPerkHandlers
                            .Add(new PerkHandlerBlackBoard.PerkHandlerData(PerkHandlerBlackBoard.PerkHandlerType.shuffle,
                                new Vector2Int(-100, -100), false,0));
                        break;
                    case "7":
                        Debug.Log("dsakjhdvajkdhabvdhakhdbjva bkasdhj jsakdb ");
                        turnHandler.setHisBoosterName(jsonContent.boosterSelected);
                        turnHandler.setHisClientId(jsonContent.clientID);
                        break;
                    case "8":
                        if (counterSocketMessage!=Int32.Parse(jsonContent.number))
                        {
                            for (int i = Int32.Parse(jsonContent.number)+1; i <= counterSocketMessage; i++)
                            {
                                sendChat("8", "(1, 1)", "(1, 1)", "", "", "", i);
                            }
                       }
                        break;

                }
            };
        }

        public async void OnBackBtnClick()
        {
            var matchId = myGameMatchicket;

            await mySocket.LeaveMatchAsync(matchId);
            Debug.Log("Quited");

            SceneManager.LoadScene("MainApp");
        }

        public async void OnReconnectBtnClick()
        {
            var matchId = myGameMatchicket;

            await mySocket.JoinMatchAsync(matchId);
            Debug.Log("Rejoined");

            // SceneManager.LoadScene("MainApp");
        }

        public static void sendChat(string opCode, string sourcePosition, string targetPosition,string boosterSelected = "",string clientID="",string expectedNumber="",int resendMessageNumber=-1)
        {
            mySocket = MatchMakingLogic.socket;
            myGameMatchicket = MatchMakingLogic.gameMatchicket;
            
            var newState="";
            if (resendMessageNumber!=-1) //resend message
                newState = lastSocketMessage[resendMessageNumber];
            else // send new message
            {
                newState = new Dictionary<string, string>
                    {{"opcode", opCode},{"boosterSelected", boosterSelected},{"clientID", clientID},{"number",counterSocketMessage.ToString() }, {"sourcePosition", sourcePosition}, {"targetPosition", targetPosition},{"expectedNumber",expectedNumber}}.ToJson();

                counterSocketMessage++;
            }
            lastSocketMessage.Append( newState);
            Debug.Log("message sent");
            mySocket.SendMatchStateAsync(myGameMatchicket, 1, newState);
        }
    }
}