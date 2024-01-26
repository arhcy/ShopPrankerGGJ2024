using GameView.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShelvesSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            GoodView draggableItem = dropped.GetComponent<GoodView>();
            draggableItem.parentAfterDrug = transform;
            draggableItem.BusketId = 0;
        }
    }

}
