using Spine.Unity;
using UnityEngine;
using System;
using System.Linq;
using System.Threading;
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

        public void PlayAnimation(CharacterState state, bool isLoop)
        {
            
            var name = GetAnimName(state);
            _skeletonAnimation.AnimationName = name;
            var animState = _skeletonAnimation.state;
            var animationObject = _skeletonAnimation.skeletonDataAsset.GetSkeletonData(false).FindAnimation(name);

            if (animationObject != null)
                animState.SetAnimation(0, animationObject, isLoop);

            _skeletonAnimation.loop = isLoop;
        }

        public async Task PlayAnimationAsync(CharacterState state, float loopTime = -1)
        {
            var name = GetAnimName(state);
            var animState = _skeletonAnimation.state;
            var completed = false;
            var animationObject = _skeletonAnimation.skeletonDataAsset.GetSkeletonData(false).FindAnimation(name);

            if (animationObject != null)
                animState.SetAnimation(0, animationObject, loopTime > 0);

            if (loopTime < 0)
            {
                _skeletonAnimation.loop = false;
                animState.Complete += Completed;
                animState.End += Completed;
                animState.Interrupt += Completed;
            }
            else
            {
                _skeletonAnimation.loop = true;
            }

            var token = new CancellationTokenSource(new TimeSpan(0, 0, 0, 0, (int)(loopTime > 0 ? loopTime * 1000 : 30 * 1000)));

            await UniTask.WaitUntil(() => completed || token.IsCancellationRequested);

            if (!completed)
                Completed(default);

            token.Dispose();
            _skeletonAnimation.loop = false;

            void Completed(TrackEntry entry)
            {
                animState.Complete -= Completed;
                animState.End -= Completed;
                animState.Interrupt += Completed;
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