using System.Security.Cryptography;
using System.Threading.Tasks;
using CashierStage.Data;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

namespace CashierStage.View
{
    public class MovingBasketView : MonoBehaviour
    {
        [SerializeField]
        private PresentingGoodItem _goodPrototype;

        [SerializeField]
        private CashierStageSettings _cashierStageSettings;

        [SerializeField]
        private Transform _startPos;

        [SerializeField]
        private Transform _presentingPos;

        [SerializeField]
        private Transform _exitPos;

        public PresentingGoodItem[] Goods;

        public void SetGoods(GoodData[] data)
        {
            Goods = new PresentingGoodItem[data.Length];

            for (var i = 0; i < data.Length; i++)
            {
                Goods[i] = Object.Instantiate(_goodPrototype.gameObject, _goodPrototype.transform.parent, true).GetComponent<PresentingGoodItem>();
                Goods[i].SetData(data[i]);
                Goods[i].gameObject.SetActive(true);
            }
        }

        public void PlaceStart()
        {
            transform.position = _startPos.position;
        }

        public async Task MoveToPresenting()
        {
            await transform.DOMove(_presentingPos.position, _cashierStageSettings.BasketAppearDuration).AsyncWaitForCompletion();
            await transform.DOPunchScale(Vector3.one * _cashierStageSettings.BasketAppearPunchMagnitude, _cashierStageSettings.BasketAppearPunchDuration, _cashierStageSettings.BasketAppearPunchVibration).AsyncWaitForCompletion();
        }

        public async Task MoveOut()
        {
            await transform.DOPunchScale(Vector3.one * _cashierStageSettings.BasketAppearPunchMagnitude, _cashierStageSettings.BasketAppearPunchDuration, _cashierStageSettings.BasketAppearPunchVibration).AsyncWaitForCompletion();
            await transform.DOMove(_exitPos.position, _cashierStageSettings.BasketDisappearDuration).AsyncWaitForCompletion();

            foreach (var good in Goods)
                Destroy(good.gameObject);
        }
    }
}