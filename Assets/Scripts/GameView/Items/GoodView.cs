using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameView.Items
{
    public class GoodView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public GoodData good;
        public Image image;
        public int BusketId;
        [HideInInspector] public Transform parentAfterDrug;
        void Start()
        {
            //image.sprite = good.Sprite;
            //image.transform.localScale = new Vector3(good.ScaleX, good.ScaleY, 1);
        }
        void Update()
        {
            image.sprite = good.Sprite;
            image.transform.localScale = new Vector3(good.ScaleX, good.ScaleY, 1);
            Debug.Log(BusketId);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Begin drag");
            parentAfterDrug = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("Dragging");
            transform.position = Input.mousePosition;

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("End drag");
            transform.SetParent(parentAfterDrug);
            image.raycastTarget = true;
        }
    }

}
