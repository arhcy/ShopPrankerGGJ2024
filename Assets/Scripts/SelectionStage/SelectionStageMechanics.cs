using System.Linq;
using Cysharp.Threading.Tasks;
using GameData;
using GameView.Items;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace SelectionStage
{
    public class SelectionStageMechanics : MonoBehaviour
    {
        [SerializeField]
        private ShelvesSet _shelvesSet;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private BasketView[] _basketViews;

        [SerializeField]
        private GameObject _reenterHint;


        public void Construct(GlobalGameData globalGameData)
        {
            globalGameData.PlayerLevel.Subscribe(_shelvesSet.Setup);
            globalGameData.GameStage.Where(a => a == GameStage.Selection).Subscribe(a => StageEnter());

            foreach (var good in Object.FindObjectsOfType<GoodView>(true))
                good.OnEndDragHappen += UpdateBasketsData;

            UpdateBasketsData();

            async void StageEnter()
            {
                _shelvesSet.Setup(globalGameData.PlayerLevel.Value);
                UpdateBasketsData();

                if (globalGameData.Attempts > 0 && globalGameData.PlayerLevel.Value == 0)
                {
                    _reenterHint.gameObject.SetActive(true);
                    await UniTask.Delay(1000);
                    await UniTask.WaitUntil(() => Input.GetMouseButton(0));
                    _reenterHint.gameObject.SetActive(false);
                }
            }

            void UpdateBasketsData()
            {
                for (var i = 0; i < 3; i++)
                {
                    globalGameData.Baskets[i].Goods.Clear();
                    globalGameData.Baskets[i].Goods.AddRange(_basketViews[i].GetComponentsInChildren<GoodView>().Select(a => a.good));
                }

                _button.interactable = globalGameData.Baskets.All(a => a.Goods.Count() != 0);
            }
        }
    }
}