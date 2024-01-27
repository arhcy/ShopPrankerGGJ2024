using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CashierStage.View
{
    public class EndLevelView : BaseAnimController
    {
        public async Task Play(bool isWin)
        {
            gameObject.SetActive(true);
            await PlayAnimationAsync(isWin ? "win_popup_in" : "win_popup_lose_in");
            PlayAnimation(isWin ? "win_popup_idle" : "win_popup_lose_idle", true);
            await UniTask.WaitUntil(() => Input.GetMouseButton(0));
            gameObject.SetActive(false);
        }
    }
}