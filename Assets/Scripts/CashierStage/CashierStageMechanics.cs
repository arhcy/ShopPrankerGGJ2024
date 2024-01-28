using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashierStage.Data;
using CashierStage.View;
using Cysharp.Threading.Tasks;
using GameData;
using GameData.Items;
using Other;
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

        [SerializeField]
        private CameraZoomController _cameraZoomController;

        [SerializeField]
        private FunSlider _slider;

        [SerializeField]
        private EndLevelView _endLevelView;

        [SerializeField]
        private GameObject _totalWinView;

#if UNITY_EDITOR
        [SerializeField]
        private bool MockWin;
#endif

        private GlobalGameData _globalGameData;
        private CashierStageData _data;

        private bool CanContinue =>
            _data.Pass <= GameSettings.TotalCashierPasses - 1 && !_data.Loose;

        private int Pass =>
            _data.Pass;

        private CharacterState[] AnimToPassIdBind = new[] { CharacterState.Laugh, CharacterState.Laugh2, CharacterState.Laugh3 };


        public void Construct(GlobalGameData globalGameData, CashierStageData cashierStageData)
        {
            _globalGameData = globalGameData;
            _data = cashierStageData;

            _globalGameData.GameStage.Where(a => a == GameStage.Cashier).Subscribe(a => RunMechanics());
            _data.Wons.Subscribe(async wons => await _slider.SlideAsync(wons, wons == 0));
        }

        private async void RunMechanics()
        {
            ResetData();

#if UNITY_EDITOR
            if (MockWin)
                SetupMocks();
#endif


            _data.Loose = false;
            _cashier.CharacterId = _globalGameData.PlayerLevel.Value;
            _globalGameData.Attempts++;

            for (var i = 0; i < _baskets.Length; i++)
            {
                var basket = _baskets[i];
                basket.PlaceStart();
                basket.SetGoods(_globalGameData.Baskets[i].Goods.ToArray());
            }

            while (CanContinue)
            {
                await MechanicsPass();

                _data.Pass++;
            }

            await ProcessFinalData();
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

            if (_data.Wons.Value < GameSettings.TotalCashierPasses)
            {
                _cashier.PlayAnimation(CharacterState.Idle, true);

                await _baskets[_data.Pass].MoveOut();
            }


            async Task SmileStage()
            {
                var points = _globalGameData.Baskets[_data.Pass].Points.Value;
                var won = points >= GameSettings.WinBonus;
                Debug.Log($"Basket points: {points}");

                await _cameraZoomController.ZoomTween(_settings.CashierZoomLevel, _settings.CashierZoomDuraiton);

                if (won)
                {
                    _data.Wons.Value = _data.Wons.Value + 1;
                    var wons = _data.Wons.Value;

                    Debug.Log($"{Pass} {GameSettings.TotalCashierPasses - 1}");

                    if (wons == GameSettings.TotalCashierPasses)
                        await _cashier.PlayAnimationAsync(CharacterState.Laugh3_in);

                    await _cashier.PlayAnimationAsync(AnimToPassIdBind[wons - 1], _settings.CashierLaughTime);

                    if (wons == GameSettings.TotalCashierPasses)
                        _cashier.PlayAnimation(CharacterState.Laugh3, true);
                }
                else
                {
                    await _cashier.PlayAnimationAsync(CharacterState.Cancel);
                }

                if (!won)
                {
                    await UniTask.Delay((int)(_settings.ZoomLooseDelay * 1000));
                }

                await _cameraZoomController.ZoomTween(1, _settings.CashierZoomDuraiton);
            }
        }

        private void ResetData()
        {
            _data.Pass = 0;
            _data.Wons.Value = 0;
        }

        private async Task ProcessFinalData()
        {
            await UniTask.Delay((int)(_settings.DelayBeforeFinal * 1000));

            var won = _data.Wons.Value == GameSettings.TotalCashierPasses;
            Debug.Log($"won: {won}");

            if (won)
            {
                _globalGameData.PlayerLevel.Value += 1;

                if (_globalGameData.PlayerLevel.Value >= GameSettings.TotalLevels)
                {
                    _totalWinView.gameObject.SetActive(true);

                    return;
                }
            }

            await _endLevelView.Play(won);
            _globalGameData.GameStage.Value = GameStage.Selection;
        }

        private void SetupMocks()
        {
            //mocks
            var good = _repository.goods[0];
            var good1 = _repository.goods[1];
            var good2 = _repository.goods[2];
            //_data.Pass = 2;
            //_data.Wons.Value = 2;
            _globalGameData.Baskets = new[] { new BasketData(1), new BasketData(2), new BasketData(3) };
            //_globalGameData.Baskets[0].Goods.AddRange(new[] { good });
            //_globalGameData.Baskets[1].Goods.AddRange(new[] { good, good, good });
            //_globalGameData.Baskets[2].Goods.AddRange(new[] { good, good });
            _globalGameData.Baskets[0].Goods.AddRange(new[] { good, good, good });
            _globalGameData.Baskets[1].Goods.AddRange(new[] { good, good, good });
            _globalGameData.Baskets[2].Goods.AddRange(new[] { good, good, good });

            //endmocks
        }
    }
}