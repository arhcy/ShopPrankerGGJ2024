using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;


public class StageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;

    [SerializeField]
    private GameObject _selectionStage;

    [SerializeField]
    private GameObject _cashierStage;

    public void ShowStage(GameStage stage)
    {
        _menu.SetActive(stage == GameStage.Menu);
        _selectionStage.SetActive(stage == GameStage.Selection);
        _cashierStage.SetActive(stage == GameStage.Cashier);
    }
}