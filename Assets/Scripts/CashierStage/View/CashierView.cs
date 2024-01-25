using Spine.Unity;
using UnityEngine;
using System;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Spine;
using static Cysharp.Threading.Tasks.UniTask;

namespace CashierStage.View
{
    public enum CharacterState
    {
        Cancel,
        Idle,
        Laugh,
        Laugh2,
        Laugh3,
        Laugh3_in,
        Looking
    }

    public class CashierView : MonoBehaviour
    {
        [Serializable]
        public struct AnimNameBinder
        {
            public CharacterState State;
            public string AnimName;
        }

        [SerializeField]
        private SkeletonAnimation _skeletonAnimation;

        [SerializeField]
        private string[] _characterPrefixes;

        [SerializeField]
        private AnimNameBinder[] _binders;

        public int CharacterId;

        public void PlayAnimation(CharacterState state, Action onComplete = null)
        {
            _skeletonAnimation.AnimationName = GetAnimName(state);

            if (onComplete == null) return;

            var animState = _skeletonAnimation.state;
            animState.Complete += Completed;

            void Completed(TrackEntry entry)
            {
                animState.Complete -= Completed;
                onComplete?.Invoke();
            }
        }        
        
        public async Task PlayAnimationAsync(CharacterState state)
        {
            _skeletonAnimation.AnimationName = GetAnimName(state);

            var animState = _skeletonAnimation.state;
            animState.Complete += Completed;
            var completed = false;

            await UniTask.WaitUntil(() => completed);

            void Completed(TrackEntry entry)
            {
                animState.Complete -= Completed;
                completed = true;
            }
        }

        private string GetAnimName(CharacterState state) =>
            _characterPrefixes[CharacterId] + _binders.First(a => a.State == state).AnimName;


#if UNITY_EDITOR
        public string AnimName;

        [ContextMenu("Test")]
        private void Test()
        {
            _skeletonAnimation.AnimationName = AnimName;
        }

#endif
    }
}