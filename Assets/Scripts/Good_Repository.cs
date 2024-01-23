using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gooods", menuName = "Object/GoodsList")]
public class Good_Repository : ScriptableObject
{
    public Good[] goods;  
    Good[] Provide(int level)
    {
        Good[] nededGoods = new Good[30];
        for (int i = 0,num = 0; i<goods.Length; i++)
        {
            if (goods[i].Level == level)
            {
                nededGoods[num] = goods[i];
                num++;
            }
        }
        return nededGoods;
    }

}
