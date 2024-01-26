using System.Threading.Tasks;
using CashierStage.Data;
using DG.Tweening;
using UnityEngine;

namespace CashierStage.View
{
    public class GoodsPresentingView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _presentingLeftZone;

        [SerializeField]
        private GameObject _presentingRightZone;

        [SerializeField]
        private CashierStageSettings _settings;

        private GameObject _goods;


        public Task PresentGoods(PresentingGoodItem[] goods)
        {
            var num = goods.Length;
            var left = _presentingLeftZone.transform.position;
            var right = _presentingRightZone.transform.position;

            var len = right.x - left.x;
            var delta = len / num;
            Tweener last = null;

            for (var i = 0; i < num; i++)
                last = goods[i].transform.DOMove(left + new Vector3(delta * i, 0, 0), _settings.GoodsUnpackDuration);

            return last.AsyncWaitForCompletion();
        }
    }
}