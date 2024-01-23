using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Basket", menuName = "Objects/Basket")]
public class Basket : ScriptableObject
{
    public int Id;
    public Sprite sprite;
    public List<Good> GoodsInBasket = new List<Good>();

    int CalculateBonus()
    {
        int bonus = 0;
        bonus += 10 * GoodsInBasket.FindAll(goods => goods.IsFunny).Count;

        return bonus;
    }
}
