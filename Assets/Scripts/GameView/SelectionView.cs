using GameData;
using GameView.Items;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace Menu
{
    public class SelectionView : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private List<GoodView> _goodViews;
        public GlobalGameData gameData;
        public void Construct(
            GlobalGameData data
        )
        {
            

            _button.onClick.AddListener(
                () => { data.GameStage.Value = GameStage.Cashier;
                    var goods = _goodViews.FindAll(x => x.BusketId == 1);
                    foreach (var good in goods)
                    {
                        data.Baskets[0].Goods.Add(good.good);
                    }
                    goods = _goodViews.FindAll(x => x.BusketId == 2);
                    foreach (var good in goods)
                    {
                        data.Baskets[1].Goods.Add(good.good);
                    }
                    goods = _goodViews.FindAll(x => x.BusketId == 3);
                    foreach (var good in goods)
                    {
                        data.Baskets[2].Goods.Add(good.good);
                    }
                }
            );
        }
    }
}
