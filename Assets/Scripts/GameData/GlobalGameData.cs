using UniRx;

namespace GameData
{
    public class GlobalGameData
    {
        public ReactiveProperty<GameStage> GameStage = new ReactiveProperty<GameStage>();
        public ReactiveProperty<int> PlayerLevel = new ReactiveProperty<int>();
        public ReactiveProperty<int> PlayerScore = new ReactiveProperty<int>();
    }
}