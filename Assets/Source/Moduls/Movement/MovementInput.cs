using Source.Game.Scripts.Configure;
using UnityEngine;

namespace Source.Moduls.Movement
{
    public class MovementInput : MonoBehaviour
    {
        [SerializeField] private CharacterMovement _movement;
        [SerializeField] private Config _config;
 
        private void Start()
        {
            _movement.Init();
        }

        private void OnMouseDown()
        {
            if (_movement.IsReady && _config.IsGaming)
                _movement.Rotate();
        }

        private void FixedUpdate()
        {
            _movement.Scroll();
        }
    }
}