using System;
using GameData;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Menu
{
    public class IntroPresenter : MonoBehaviour
    {
        [SerializeField]
        private SkeletonAnimation _animation;

        [SerializeField]
        private GameObject _button;

        private GlobalGameData _globalGameData;
        private bool _canMoveNext;

        public void Construct(GlobalGameData data)
        {
            _globalGameData = data;
        }

        private void Awake()
        {
            _button.SetActive(false);
            _animation.AnimationState.Complete += IntroAnimationDone;


            void IntroAnimationDone(TrackEntry entry)
            {
                _button.gameObject.SetActive(true);
                _canMoveNext = true;
            }
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(0) && _canMoveNext)
                _globalGameData.GameStage.Value = GameStage.Selection;
        }
    }
}