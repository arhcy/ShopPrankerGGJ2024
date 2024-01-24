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

        private GlobalGameData _gameData;
        private StageManagerSetup _stageManagerSetup;

        private void Awake()
        {
            _gameData = new GlobalGameData();
            _stageManagerSetup = new StageManagerSetup(_stageManager, _gameData);
            _menuView.Construct(_gameData);
        }
    }
}