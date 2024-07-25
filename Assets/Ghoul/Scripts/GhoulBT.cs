using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class GhoulBT : Tree
    {

        public static float _shadowChargeSpeed = 35;
        public static float _shadowChargeLeftOverDistance = 2.5f;
        public static float _maxShadowChargeDistance = 15.0f;

        public GameObject _target;
        private Animator _animator;
        private Transform _transform;
        private Rigidbody2D _rigidybody;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _transform = GetComponent<Transform>();
            _rigidybody = GetComponent<Rigidbody2D>();
        }

        protected override Node SetupTree()
        {
            Node root = new RepeatDecorator(3, new List<Node>() {
                new Sequence(new List<Node>()
                {
                    new Rest(1.0f),
                    new LookAtTarget(_animator, _transform, _target.transform, _shadowChargeLeftOverDistance),
                    new ShadowDash(_transform, _rigidybody),
                    
                })
            });
     
            return root;
        }
    }
}
