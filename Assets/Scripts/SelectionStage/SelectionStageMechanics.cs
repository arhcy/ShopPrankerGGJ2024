using GameData;
using UniRx;
using UnityEngine;

namespace SelectionStage
{
    public class SelectionStageMechanics : MonoBehaviour
    {
        [SerializeField]
        private ShelvesSet _shelvesSet;


        public void Construct(GlobalGameData globalGameData)
        {
            globalGameData.PlayerLevel.Subscribe(_shelvesSet.Setup);
            globalGameData.GameStage.Where(a => a == GameStage.Selection).Subscribe(a=>_shelvesSet.Setup(globalGameData.PlayerLevel.Value));
        }
    }
}