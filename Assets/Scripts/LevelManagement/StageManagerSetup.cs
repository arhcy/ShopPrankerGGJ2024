using GameData;
using UniRx;

namespace LevelManagement
{
    public class StageManagerSetup
    {
        public StageManagerSetup(
            StageManager manager,
            GlobalGameData globalGameData,
            GameStage? forceStage
        )
        {
            if (forceStage.HasValue)
                globalGameData.GameStage.Value = forceStage.Value;

            globalGameData.GameStage.Subscribe(manager.ShowStage);
        }
    }
}