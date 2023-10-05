using UnityEngine;
using UnityEngine.UI;

namespace Source.Game.Scripts.CameraLogic
{
    public class HorizontalLayoutGroup : LayoutGroup
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CameraFollow _cameraFollow;
        [SerializeField] private Transform _targetHorizontal;
        [SerializeField] private Transform _targetVertical;
        
        private Vector3 _startPosition;
        private bool _isChangeCameraView;
        
        public override void CalculateLayoutInputVertical()
        {
        }

        public override void SetLayoutHorizontal()
        {
        }

        public override void SetLayoutVertical()
        {
            if (_canvas.transform.localScale.x > 1 && !_isChangeCameraView)
            {
                _isChangeCameraView = true;
                _cameraFollow.ChangeCameraView(_targetHorizontal.position);
                _cameraFollow.ReduceSize();
            }
            
            if (_canvas.transform.localScale.x <= 1 && _isChangeCameraView)
            {
                _isChangeCameraView = false;
                _cameraFollow.ChangeCameraView(_targetVertical.position);
                _cameraFollow.IncreaseSize();
            }
        }
    }
}