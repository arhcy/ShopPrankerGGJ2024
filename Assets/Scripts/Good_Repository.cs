using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gooods", menuName = "Object/GoodsList")]
public class Good_Repository : ScriptableObject
{
    public List<Good> goods = new List<Good>();
    List<Good> Provide(int level)
    {
        return goods.FindAll(goods => goods.Level == level); 
    }

}
