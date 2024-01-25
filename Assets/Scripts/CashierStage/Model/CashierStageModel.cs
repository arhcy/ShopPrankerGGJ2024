using CashierStage.Data;
using CashierStage.View;
using GameData;
using UniRx;

namespace CashierStage.Model
{
    public class CashierStageModel
    {
        private readonly GlobalGameData _gameData;
        private readonly CashierStageData _cashierData;

        public CashierStageModel(
            GlobalGameData gameData,
            CashierStageData cashierData
        )
        {
            _gameData = gameData;
            _cashierData = cashierData;

            _gameData.GameStage.Where(a => a == GameStage.Cashier).Subscribe(a => StageEntered());

            void StageEntered()
            {
                _cashierData.CashierState.Value = CharacterState.Idle;
            }
        }
    }
}