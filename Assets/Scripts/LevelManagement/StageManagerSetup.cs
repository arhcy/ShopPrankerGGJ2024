using GameData;
using UniRx;

namespace LevelManagement
{
    public class StageManagerSetup
    {
        public StageManagerSetup(
            StageManager manager,
            GlobalGameData globalGameData
        )
        {
            globalGameData.GameStage.Subscribe(manager.ShowStage);
        }
    }
}