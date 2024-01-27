using System;
using GameData.Items;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
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

                transform.DOPunchScale(Vector3.one * 0.2f, 0.3f);
            }
        }

    }
}
