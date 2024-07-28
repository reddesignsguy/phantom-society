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
        public static float _targetRadiusAroundPlayer = 10.0f;
        public static float _rangedShadowSlashSpeed = 15.0f;
        // Object pool

        public GameObject _target;
        private Animator _animator;
        private Transform _transform;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        protected override Node SetupTree()
        {

            Node root =
                new Randomizer(new List<Node>() {
                    //new RepeatDecorator(3, new List<Node>() {
                    //    new Sequence(new List<Node>()
                    //    {
                    //        new Rest(1.0f),
                    //        new LookAtTarget(_animator, _transform, _target.transform, _shadowChargeLeftOverDistance),
                    //        new ShadowDash(_transform, _rigidbody),

                    //    })
                    //}),
                    new Sequence(new List<Node>()
                    {
                        new TeleportAroundTarget(_rigidbody, _targetRadiusAroundPlayer, _target.transform),
                        new Rest(2.0f),
                        new RangedShadowSlash(_transform, _target.transform),
                        new Rest(2.0f),
                    })
                });; ;
     
            return root;
        }
    }
}
