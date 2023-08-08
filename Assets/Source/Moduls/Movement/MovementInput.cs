using UnityEngine;

namespace Movement
{
    public class MovementInput : MonoBehaviour
    {
        [SerializeField] CharacterMovement _movement;

        private void Start()
        {
            _movement.Init();
        }

        private void OnMouseDown()
        {
            if (_movement.IsReady)
                _movement.Rotate();
        }

        private void FixedUpdate()
        {
            _movement.Scroll();
        }
    }
}