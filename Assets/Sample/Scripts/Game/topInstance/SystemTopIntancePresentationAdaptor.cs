using System;
using System.Threading.Tasks;
using DG.Tweening;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sample
{
    public class SystemTopIntancePresentationAdaptor : MonoBehaviour, TopInstanceSystemPresentationPort
    {
        public GameObject[] gems;
        public GameObject boardGame;

        private Vector3 logicalPositionToPresentation(Vector2 pos)
        {
            return new Vector3((pos.x - 3) * 112, (pos.y + 1000) * -98,
                transform.position.z);
        }

        public async void PlayTopInstance(CellStack cellStack, BasicGameplayMainController gameplayController,
            Action onCompleted)
        {
            // Here you can play an animation for TopInstanceing the presenters for these CellStacks.
            var tileStackFactory = new MainTileStackFactory();
            var tileStack = tileStackFactory.Create();
            cellStack.SetCurrnetTileStack(tileStack);
            // tileStack.SetPosition(cellStack.Position());
            tileStack.SetPosition(new Vector2(cellStack.Position().x,cellStack.Position().y));
            int random = Random.Range(0, 6);
            tileStack.Push(new gemTile((gemColors) random,gemTypes.normal));

            GameObject newObject = null;
            // newObject = Instantiate(gems[Random.Range(0, 6)],
            //     logicalPositionToPresentation(tileStack.Position()), Quaternion.identity);
            newObject = Instantiate(gems[random]);
            newObject.transform.SetParent(boardGame.transform, false);
            newObject.transform.localScale = new Vector3(1, 1, 1);
            newObject.GetComponent<gemTilePresenter>().setup(tileStack, gameplayController);


            // Debug.Log($"Start TopInstanceing {cellStack1.Position()} and {cellStack2.Position()} ");

            // await Task.Delay(500);

            // Debug.Log($"Finished TopInstanceing {cellStack1.Position()} and {cellStack2.Position()} ");

            onCompleted.Invoke();
        }
    }
}