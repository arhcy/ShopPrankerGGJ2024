using System;
using Cinemachine;
using GameData;
using LevelManagement;
using Menu;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField]
        private StageManager _stageManager;

        [SerializeField]
        private MenuView _menuView;

        [SerializeField]
        private Good_Repository _goodRepository;

        [SerializeField]
        private IntroPresenter _introPresenter;

        private GlobalGameData _gameData;
        private StageManagerSetup _stageManagerSetup;


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
        }
    }
}