using GameView.Items;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class ShelvesSet : MonoBehaviour
{
    
    public List<GoodView> Goods;
    public Good_Repository good_rep;
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        var good = good_rep.Provide(level);
        Debug.Log(good[0].Name);
        for (int i = 0; i < good.Count && i < Goods.Count; i++)
        {
           Goods[i].good = good[i];
        }
    }


}
