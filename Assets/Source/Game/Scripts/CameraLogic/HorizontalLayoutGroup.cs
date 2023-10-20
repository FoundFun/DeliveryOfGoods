using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Scripts.CameraLogic
{
    public class HorizontalLayoutGroup : LayoutGroup
    {
        [SerializeField] private RectTransform _canvasRectTransform;
        [SerializeField] private CameraFollow _cameraFollow;
        [SerializeField] private Transform _targetHorizontal;
        [SerializeField] private Transform _targetVertical;

        private const float TargetWidthScale = 1500;

        private Vector3 _startPosition;
        private bool _isChangeCameraView;

        public override void SetLayoutVertical()
        {
            if (_canvasRectTransform.rect.width > TargetWidthScale && !_isChangeCameraView)
            {
                _isChangeCameraView = true;
                _cameraFollow.ChangeCameraView(_targetHorizontal.position);
                _cameraFollow.ReduceSize();
            }

            if (_canvasRectTransform.rect.width <= TargetWidthScale && _isChangeCameraView)
            {
                _isChangeCameraView = false;
                _cameraFollow.ChangeCameraView(_targetVertical.position);
                _cameraFollow.IncreaseSize();
            }
        }

        public override void CalculateLayoutInputVertical()
        {
        }

        public override void SetLayoutHorizontal()
        {
        }
    }
}