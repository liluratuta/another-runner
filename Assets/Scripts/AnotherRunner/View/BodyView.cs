using AnotherRunner.Model.Bodies;
using UnityEngine;

namespace AnotherRunner.View
{
    public class BodyView : MonoBehaviour
    {
        private IBody _body;
        private Transform _transform;

        public void Init(IBody obstacle) => _body = obstacle;
        private void Awake() => _transform = transform;
        private void Start() => _transform.localScale = _body.Size;
        private void LateUpdate() => _transform.position = _body.Position;
    }
}