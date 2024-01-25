using System;
using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;


public class StageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _menuStage;

    [SerializeField]
    private GameObject _introStage;

    [SerializeField]
    private GameObject _selectionStage;

    [SerializeField]
    private GameObject _cashierStage;

    public void ShowStage(GameStage stage)
    {
        _menuStage.SetActive(stage == GameStage.Menu);
        _introStage.SetActive(stage == GameStage.Intro);
        _selectionStage.SetActive(stage == GameStage.Selection);
        _cashierStage.SetActive(stage == GameStage.Cashier);
    }
}