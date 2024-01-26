using UnityEngine;
using UnityEngine.UI;

namespace CashierStage.View
{
    public class PresentingGoodItem : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _image;

        public void SetData(GoodData good)
        {
            _image.sprite = good.Sprite;
            _image.transform.localScale = new Vector3(good.ScaleX, good.ScaleY, 1);
        }
    }
}
