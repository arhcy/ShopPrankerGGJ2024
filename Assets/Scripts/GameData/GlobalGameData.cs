using GameData.Items;
using UniRx;

namespace GameData
{
    public class GlobalGameData
    {
        public ReactiveProperty<GameStage> GameStage = new ReactiveProperty<GameStage>();
        public ReactiveProperty<int> PlayerLevel = new ReactiveProperty<int>();
        public ReactiveProperty<int> PlayerScore = new ReactiveProperty<int>();
        public BasketData[] Baskets = new[] { new BasketData(0), new BasketData(1), new BasketData(2) };
        public int Attempts;
    }
}