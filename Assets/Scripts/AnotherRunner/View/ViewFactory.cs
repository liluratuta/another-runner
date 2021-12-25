using System.Collections.Generic;
using AnotherRunner.Model.Bodies;
using UnityEngine;

namespace AnotherRunner.View
{
    public class ViewFactory : MonoBehaviour
    {
        [SerializeField] private BodyView _bodyPrefab;
        private readonly Dictionary<IBody, BodyView> _activeBodies = new Dictionary<IBody, BodyView>();
        
        public void Make(IBody body)
        {
            var view = Instantiate(_bodyPrefab, transform);
            view.Init(body);
            _activeBodies.Add(body, view);
        }

        public void Reclaim(IBody body)
        {
            if (!_activeBodies.ContainsKey(body)) return;
            
            var view = _activeBodies[body];
            _activeBodies.Remove(body);
            Destroy(view.gameObject);
        }
    }
}