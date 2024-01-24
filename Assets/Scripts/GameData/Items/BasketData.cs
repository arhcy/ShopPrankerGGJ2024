using System.Linq;
using Settings;
using UniRx;

namespace GameData.Items
{
    public class BasketData
    {
        public readonly int Id;
        public ReactiveCollection<GoodData> Goods;
        public ReadOnlyReactiveProperty<int> Points;

        public BasketData(int id)
        {
            Id = id;
            Goods = new ReactiveCollection<GoodData>();
            Points = Goods.ObserveEveryValueChanged(CollectionChanged).ToReadOnlyReactiveProperty();

            int CollectionChanged(ReactiveCollection<GoodData> goods)
            {
                var found = goods.GroupBy(a => a.GroupName).FirstOrDefault(a => a.Count() >= GameSettings.ItemsToWin);

                if (found == default) return 0;

                var bonus = GameSettings.WinBonus;

                foreach (var item in found)
                    if (item.IsFunny)
                        bonus += GameSettings.AdditionalBonus;

                return bonus;
            }
        }
    }
}