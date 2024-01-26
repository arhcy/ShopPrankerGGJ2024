using GameData.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;


namespace GameView.Items
{
    public class BasketView : MonoBehaviour, IDropHandler
    {
        public int busketId;
        public void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount < 6)
            {
                GameObject dropped = eventData.pointerDrag;
                GoodView good = dropped.GetComponent<GoodView>();
                good.parentAfterDrug = transform;
                good.BusketId = busketId;
                
            }
        }

    }
}
