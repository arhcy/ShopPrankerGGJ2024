using UnityEngine;

namespace CashierStage.Data
{
    [CreateAssetMenu(fileName = "CashierSettings.asset", menuName = "Create cashier settings")]
    public class CashierStageSettings : ScriptableObject
    {
        [Header("Start")]
        public float StartDelay;
        
        [Header("Basket:")]
        public float BasketAppearDuration = 1;
        public float BasketAppearPunchMagnitude = 1;
        public int BasketAppearPunchVibration = 1;
        public float BasketAppearPunchDuration = 1;
        
        public float BasketDisappearDuration = 1;
        
        [Header("Cashier:")]
        public float CashierLookTime = 3;
        public float CashierLaughTime = 3;
        
        [Header("Goods:")]
        public float GoodsUnpackDuration = 1;
    }
}