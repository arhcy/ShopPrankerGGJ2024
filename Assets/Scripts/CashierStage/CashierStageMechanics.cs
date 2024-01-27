using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashierStage.Data;
using CashierStage.View;
using Cysharp.Threading.Tasks;
using GameData;
using GameData.Items;
using Settings;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace CashierStage
{
    public class CashierStageMechanics : MonoBehaviour
    {
        [SerializeField]
        private CashierView _cashier;

        [SerializeField]
        private Good_Repository _repository;

        [SerializeField]
        private MovingBasketView[] _baskets;

        [SerializeField]
        private GoodsPresentingView _goods;

        [SerializeField]
        private CashierStageSettings _settings;

        private GlobalGameData _globalGameData;
        private CashierStageData _data;

        private bool CanContinue =>
            _data.Pass <= GameSettings.TotalCashierPasses && !_data.Loose;

        private int Pass =>
            _data.Pass;

        private CharacterState[] AnimToPassIdBind = new[] { CharacterState.Laugh, CharacterState.Laugh2, CharacterState.Laugh3 };


        public void Construct(GlobalGameData globalGameData, CashierStageData cashierStageData)
        {
            _globalGameData = globalGameData;
            _data = cashierStageData;

            _globalGameData.GameStage.Where(a => a == GameStage.Cashier).Subscribe(a => RunMechanics());
        }

        private async void RunMechanics()
        {
            SetupMocks();

            _data.Loose = false;
            _cashier.CharacterId = _globalGameData.PlayerLevel.Value;

            for (var i = 0; i < _baskets.Length; i++)
            {
                var basket = _baskets[i];
                basket.PlaceStart();
                basket.SetGoods(_globalGameData.Baskets[i].Goods.ToArray());
            }


            while (CanContinue)
            {
                await MechanicsPass();

                if (!_data.Loose)
                    _data.Pass++;
            }

            _globalGameData.GameStage.Value = GameStage.Intro;
        }

        private async Task MechanicsPass()
        {
            Debug.Log($"Mechanics start, pass {Pass}");
            _cashier.PlayAnimation(CharacterState.Idle, true);

            await UniTask.Delay((int)(_settings.StartDelay + 1000));
            await _baskets[_data.Pass].MoveToPresenting();
            await _goods.PresentGoods(_baskets[Pass].Goods);
            await _cashier.PlayAnimationAsync(CharacterState.Looking, _settings.CashierLookTime);
            await SmileStage();
            _cashier.PlayAnimation(CharacterState.Idle, true);
            await _baskets[_data.Pass].MoveOut();


            async Task SmileStage()
            {
                var points = _globalGameData.Baskets[_data.Pass].Points.Value;
                Debug.Log($"Basket points: {points}");

                if (points >= GameSettings.WinBonus)
                {
                    if (Pass == GameSettings.TotalCashierPasses - 1)
                        await _cashier.PlayAnimationAsync(CharacterState.Laugh3_in);

                    await _cashier.PlayAnimationAsync(AnimToPassIdBind[Pass], _settings.CashierLaughTime);
                }
                else
                {
                    await _cashier.PlayAnimationAsync(CharacterState.Cancel);
                }
            }
        }

        private void SetupMocks()
        {
            //mocks
            var good = _repository.goods[0];
            _globalGameData.Baskets = new[] { new BasketData(1), new BasketData(2), new BasketData(3) };
            _globalGameData.Baskets[0].Goods.AddRange(new []{good});
            _globalGameData.Baskets[1].Goods.AddRange(new []{good, good, good});
            _globalGameData.Baskets[2].Goods.AddRange(new []{good, good});
            
            
            //endmocks
        }
    }
}