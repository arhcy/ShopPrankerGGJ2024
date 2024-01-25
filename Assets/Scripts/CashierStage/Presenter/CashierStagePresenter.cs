using CashierStage.Data;
using CashierStage.View;
using GameData;
using UniRx;

namespace CashierStage.Presenter
{
    public class CashierStagePresenter
    {
        private readonly GlobalGameData _globalGameData;
        private readonly CashierStageData _cashierStageData;
        private readonly CashierView _cashierView;

        public CashierStagePresenter(
            GlobalGameData globalGameData,
            CashierStageData cashierStageData,
            CashierView cashierView
        )
        {
            _globalGameData = globalGameData;
            _cashierStageData = cashierStageData;
            _cashierView = cashierView;

            _cashierStageData.CashierState.Subscribe(CashierStateChanged);
            _globalGameData.PlayerLevel.Subscribe(LevelChanged);

            async void CashierStateChanged(CharacterState state)
            {
                _cashierStageData.CurrentAnimation = _cashierView.PlayAnimationAsync(state);
                await _cashierStageData.CurrentAnimation;
            }

            void LevelChanged(int level)
            {
                _cashierView.CharacterId = level;
            }
        }
    }
}