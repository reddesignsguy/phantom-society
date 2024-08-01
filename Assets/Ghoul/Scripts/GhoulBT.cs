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
        private BoxCollider2D _collider;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();

            _collider.enabled = false;
        }

        protected override Node SetupTree()
        {
            Node root =
                new Randomizer(new List<Node>() {
                    // Shadow dash
                    new Sequence(new List<Node>
                    {
                        new TeleportAroundTarget(_rigidbody, _targetRadiusAroundPlayer, _target.transform),
                        new RepeatDecorator(3, new List<Node>() {
                            new Sequence(new List<Node>()
                            {
                                new Rest(1.0f),
                                new LookAtTarget(_animator, _transform, _target.transform, _shadowChargeLeftOverDistance),
                                new Rest(0.02f),
                                new ShadowDash(_transform, _rigidbody, _collider),
                            })
                         }),
                        new Rest(1.0f)
                    }),

                    // Ranged shadow slash
                    new Sequence(new List<Node>()
                    {
                        new TeleportAroundTarget(_rigidbody, _targetRadiusAroundPlayer, _target.transform),
                        new Rest(0.1f),
                        new RepeatDecorator(5, new List<Node>() {
                            new Sequence(new List<Node>()
                            {
                                new RangedShadowSlash(_transform, _target.transform),
                                new Rest(0.3f)
                            })
                        }),
                        new Rest(1.0f),
                    })
                });
     
            return root;
        }
    }
}
