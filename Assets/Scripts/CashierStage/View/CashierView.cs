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

    public class CashierView : BaseAnimController
    {
        [Serializable]
        public struct AnimNameBinder
        {
            public CharacterState State;
            public string AnimName;
        }

        [SerializeField]
        private string[] _characterPrefixes;

        [SerializeField]
        private AnimNameBinder[] _binders;

        public int CharacterId;

        public void PlayAnimation(CharacterState state, bool isLoop)
        {
            var name = GetAnimName(state);
            PlayAnimation(name, isLoop);
        }

        public Task PlayAnimationAsync(CharacterState state, float loopTime = -1)
        {
            var name = GetAnimName(state);

            return PlayAnimationAsync(name, loopTime);
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