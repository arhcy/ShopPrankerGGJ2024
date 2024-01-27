using CashierStage;
using CashierStage.Data;
using CashierStage.Model;
using CashierStage.Presenter;
using CashierStage.View;
using GameData;
using LevelManagement;
using Menu;
using SelectionStage;
using UnityEngine;

namespace Core
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField]
        private StageManager _stageManager;

        [SerializeField]
        private MenuView _menuView;

        [SerializeField]
        private SelectionView _selectionView;

        [SerializeField]
        private Good_Repository _goodRepository;

        [SerializeField]
        private IntroPresenter _introPresenter;

        [SerializeField]
        private CashierView _cashierView;

        [SerializeField]
        private CashierStageMechanics _cashierStageMechanics;
        
        [SerializeField]
        private SelectionStageMechanics _selectionStageMechanics;

        private GlobalGameData _gameData;
        private StageManagerSetup _stageManagerSetup;
        private CashierStageData _cashierStageData;
        private CashierStageModel _cashierStageModel;
        private CashierStagePresenter _cashierStagePresenter;


        [SerializeField]
        private GameStage InitialStage;

        [SerializeField]
        private bool InitFromStage;


        private void Awake()
        {
            _gameData = new GlobalGameData();
            _stageManagerSetup = new StageManagerSetup(_stageManager, _gameData, InitFromStage ? InitialStage : null);
            _menuView.Construct(_gameData);
            _introPresenter.Construct(_gameData);
            _cashierStageData = new CashierStageData();
            _selectionView.Construct(_gameData);
            _cashierStageMechanics.Construct(_gameData, _cashierStageData);
            _selectionStageMechanics.Construct(_gameData);
        }
    }
}