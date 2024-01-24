using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gooods", menuName = "Object/GoodsList")]
public class Good_Repository : ScriptableObject
{
    public List<GoodData> goods = new List<GoodData>();
    List<GoodData> Provide(int level)
    {
        return goods.FindAll(goods => goods.Level == level); 
    }

}
