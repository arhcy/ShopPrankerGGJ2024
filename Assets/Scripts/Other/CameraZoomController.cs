using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Other
{
    public class CameraZoomController : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        public Task ZoomTween(float zoom, float duraiton) =>
            _camera.DOOrthoSize(5f * zoom, duraiton).AsyncWaitForCompletion();
    }
}