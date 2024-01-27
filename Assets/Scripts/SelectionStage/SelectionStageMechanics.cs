using System.Linq;
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


        public void Construct(GlobalGameData globalGameData)
        {
            globalGameData.PlayerLevel.Subscribe(_shelvesSet.Setup);
            globalGameData.GameStage.Where(a => a == GameStage.Selection).Subscribe(a => _shelvesSet.Setup(globalGameData.PlayerLevel.Value));


            foreach (var good in Object.FindObjectsOfType<GoodView>(true))
                good.OnEndDragHappen += UpdateBasketsData;
            
            UpdateBasketsData();
            

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
