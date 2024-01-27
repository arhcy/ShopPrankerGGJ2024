using System.Threading.Tasks;
using CashierStage.View;
using UniRx;

namespace CashierStage.Data
{
    public class CashierStageData
    {
        public ReactiveProperty<CharacterState> CashierState = new ReactiveProperty<CharacterState>();
        public Task CurrentAnimation;
        public int Pass;
        public ReactiveProperty<int> Wons = new ReactiveProperty<int>();
        public bool Loose;
    }
}