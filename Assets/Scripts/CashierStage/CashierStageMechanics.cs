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
            Debug.Log($"Mechanics start, pass {_data.Pass}");
            _cashier.PlayAnimation(CharacterState.Idle, true);
            
            await UniTask.Delay((int)(_settings.StartDelay + 1000));
            await _baskets[_data.Pass].MoveToPresenting();
            await _goods.PresentGoods(_baskets[_data.Pass].Goods);
            await _cashier.PlayAnimationAsync(CharacterState.Looking, _settings.CashierLookTime);
            await _cashier.PlayAnimationAsync(CharacterState.Laugh2, _settings.CashierLaughTime);
            await _baskets[_data.Pass].MoveOut();
        }

        private void SetupMocks()
        {
            //mocks
            var good = _repository.goods[0];

            _globalGameData.Baskets = new[]
            {
                new BasketData(1) { Goods = new ReactiveCollection<GoodData>(new List<GoodData> { good }) },
                new BasketData(2) { Goods = new ReactiveCollection<GoodData>(new List<GoodData> { good, good }) },
                new BasketData(3) { Goods = new ReactiveCollection<GoodData>(new List<GoodData> { good, good, good }) }
            };
            //endmocks

        }
    }
}