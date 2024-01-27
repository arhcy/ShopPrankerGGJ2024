using GameData;
using GameView.Items;
using System.Collections.Generic;
using Settings;
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

        public void Construct(
            GlobalGameData data
        )
        {
            _button.onClick.AddListener(
                () =>
                {
                    for (var i = 0; i < 3; i++)
                    {
                        var goods = _goodViews.FindAll(x => x.BusketId == i + 1);

                        data.Baskets[i].Goods.Clear();

                        foreach (var good in goods)
                            data.Baskets[i].Goods.Add(good.good);
                    }

                    foreach (var good in _goodViews)
                        good.BusketId = 0;

                    data.GameStage.Value = GameStage.Cashier;
                }
            );
        }
    }
}