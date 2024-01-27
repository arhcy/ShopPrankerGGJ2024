using Spine.Unity;
using UnityEngine;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Spine;
using static Cysharp.Threading.Tasks.UniTask;

public class BaseAnimController : MonoBehaviour
{
    [SerializeField]
    protected SkeletonAnimation _skeletonAnimation;

    public void PlayAnimation(string name, bool isLoop)
    {
        _skeletonAnimation.AnimationName = name;
        var animState = _skeletonAnimation.state;
        var animationObject = _skeletonAnimation.skeletonDataAsset.GetSkeletonData(false).FindAnimation(name);

        if (animationObject != null)
            animState.SetAnimation(0, animationObject, isLoop);

        _skeletonAnimation.loop = isLoop;
    }

    public async Task PlayAnimationAsync(string name, float loopTime = -1)
    {
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


#if UNITY_EDITOR
    public string AnimName;

    [ContextMenu("Test")]
    private void Test()
    {
        _skeletonAnimation.AnimationName = AnimName;
    }

#endif
}