using Source.Game.Scripts.Model;
using UnityEngine;

namespace Source.Game.Scripts.Presenter
{
    [RequireComponent(typeof(Rigidbody))]
    public class PipePresenter : MonoBehaviour
    {
        private PipeModel _pipeModel;

        private void Awake()
        {
            _pipeModel = new PipeModel(this);
            _pipeModel.Init(GetComponent<Rigidbody>(), GetComponent<Renderer>().material);
        }

        private void FixedUpdate() => 
            _pipeModel.Rotate();
    }
}