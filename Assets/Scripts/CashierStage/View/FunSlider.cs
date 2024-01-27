using System.Threading.Tasks;
using CashierStage.Data;
using DG.Tweening;
using UnityEngine;

namespace CashierStage.View
{
    public class FunSlider : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _slider;

        [SerializeField]
        private RectTransform _pos0;

        [SerializeField]
        private RectTransform _pos1;

        [SerializeField]
        private RectTransform _pos2;

        [SerializeField]
        private RectTransform _pos3;

        [SerializeField]
        private CashierStageSettings _settings;

        private Vector3[] _positionToLevelBinder;
        private Transform[] _levelsBinder;


        public Task SlideAsync(int level, bool immediate = false)
        {
            InitBinder();

            _levelsBinder[level].DOPunchScale(Vector3.one * _settings.SliderMPunchMagnitude, _settings.SliderMPunchDuration, _settings.SliderMPunchVibration).SetDelay(_settings.SliderMoveTime);

            return _slider.DOMoveX(_positionToLevelBinder[level].x, immediate ? 0 : _settings.SliderMoveTime).AsyncWaitForCompletion();

            void InitBinder()
            {
                if (_positionToLevelBinder != null) return;

                _levelsBinder = new[]
                {
                    _pos0.transform,
                    _pos1.transform,
                    _pos2.transform,
                    _pos3.transform,
                };

                _positionToLevelBinder = new[]
                {
                    _pos0.transform.position,
                    _pos1.transform.position,
                    _pos2.transform.position,
                    _pos3.transform.position,
                };
            }
        }
    }
}