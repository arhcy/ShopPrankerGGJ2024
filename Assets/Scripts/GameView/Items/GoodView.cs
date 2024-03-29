using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameView.Items
{
    public class GoodView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public GoodData good { get; private set; }  
        [SerializeField]
        public Image image;
        public int BusketId;
        [HideInInspector] public Transform parentAfterDrug;
        
        private Vector3 _orginalPos;
        private Transform _originalParent;

        public Action OnEndDragHappen;

        private void Awake()
        {
            _orginalPos = transform.position;
            _originalParent = transform.parent;
        }

        public void ResetGood()
        {
            if(_originalParent==null)return;
            transform.SetParent(_originalParent);
            transform.position = _orginalPos;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Begin drag");
            parentAfterDrug = transform.parent.parent;
            transform.SetAsLastSibling();
            image.raycastTarget = false;
            BusketId = 0;
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
            
            OnEndDragHappen.Invoke();
        }

        public void SetGood(GoodData goodData)
        {
            good = goodData;
            if (image == null)
                image = GetComponent<Image>();

            image.sprite = good.Sprite;

            image.transform.localScale = new Vector3(good.ScaleX, good.ScaleY, 1);
        }
    }

}
