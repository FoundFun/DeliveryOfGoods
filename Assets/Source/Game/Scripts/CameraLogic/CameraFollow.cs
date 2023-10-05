using UnityEngine;

namespace Source.Game.Scripts.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _following;

        private const float MaxOrthographicSize = 12.9f;
        private const float MinOrthographicSize = 8.4f;

        public void ChangeCameraView(Vector3 targetPosition)
        {
            transform.position = targetPosition;
            transform.LookAt(_following.transform.position);
        }

        public void IncreaseSize() =>
            _camera.orthographicSize = MaxOrthographicSize;

        public void ReduceSize() =>
            _camera.orthographicSize = MinOrthographicSize;
    }
}