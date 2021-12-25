using System;
using System.Collections.Generic;
using System.Linq;
using AnotherRunner.Model.Bodies;
using AnotherRunner.Model.Levels;
using JetBrains.Annotations;
using QuadTree;
using UnityEngine;

namespace AnotherRunner.Model.Simulations
{
    public class CollisionSimulation : ISimulation
    {
        public event Action<IEnumerable<(IBody, IBody)>> CollisionsDetected;

        private const int QuadTreeSplitCount = 50;
        private const int QuadTreeDepthLimit = 10;
        
        private readonly QuadTree<IBody> _quadTree;
        private readonly List<IBody> _bodies = new List<IBody>();

        private readonly Stack<(IBody, IBody)> _collisions = new Stack<(IBody, IBody)>();
        private readonly HashSet<IBody> _viewedBodies = new HashSet<IBody>();
        private List<IBody> _findCollisions = new List<IBody>();

        public CollisionSimulation(LevelInfo levelInfo)
        {
            var quad = MakeQuadFromRect(levelInfo.worldBounds);
            _quadTree = new QuadTree<IBody>(QuadTreeSplitCount, QuadTreeDepthLimit, quad);
        }

        public void Update(float dt)
        {
            LoadBodiesToQuadTree();
            FindCollisions();
            _quadTree.Clear();
        }

        public void AddBody(IBody body) => _bodies.Add(body);

        public void RemoveBody(IBody body) => _bodies.Remove(body);

        private void FindCollisions()
        {
            _collisions.Clear();
            _viewedBodies.Clear();
            
            foreach (var body in _bodies)
            {
                FindCollisionsForBody(body);
            }

            if (_collisions.Count == 0)
            {
                return;
            }
            
            CollisionsDetected?.Invoke(_collisions);
        }

        private void FindCollisionsForBody(IBody body)
        {
            _quadTree.FindCollisions(body, ref _findCollisions);

            foreach (var collidedBody in _findCollisions.Where(collidedBody => !_viewedBodies.Contains(collidedBody)))
            {
                _collisions.Push((body, collidedBody));
            }

            _viewedBodies.Add(body);
        }

        private void LoadBodiesToQuadTree()
        {
            foreach (var body in _bodies)
            {
                AddBodyToQuadtree(body);
            }
        }

        private void AddBodyToQuadtree(IBody body)
        {
            var size = body.Size;
            var position = body.Position - size / 2f;
            _quadTree.Insert(body, position.x, position.y, size.x, size.y);
        }

        private Quad MakeQuadFromRect(Rect rect)
        {
            return new Quad(rect.xMin, rect.yMin, rect.xMax, rect.yMax);
        }
    }
}