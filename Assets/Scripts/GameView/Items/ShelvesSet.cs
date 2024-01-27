using System;
using GameView.Items;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class ShelvesSet : MonoBehaviour
{
    public BasketView[] BasketViews;
    public List<GoodView> Goods;
    public Good_Repository good_rep;



    public void Setup(int level)
    {
        var good = good_rep.Provide(level+1);
        for (int i = 0; i < good.Count; i++)
        {
            Goods[i].ResetGood();
            Goods[i].SetGood(good[i]);
        }
    }


}
