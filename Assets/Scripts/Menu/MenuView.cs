using GameData;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        public void Construct(
            GlobalGameData data
        )
        {
            _button.onClick.AddListener(
                () => data.GameStage.Value = GameStage.Selection
            );
        }
    }
}