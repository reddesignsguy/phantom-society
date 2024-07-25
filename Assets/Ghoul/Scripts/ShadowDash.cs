using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class ShadowDash : Node
    {

        private Rigidbody2D _rigidbody;
        private Transform _transform;


        public ShadowDash(Transform transform, Rigidbody2D rigidbody)
        {
            _transform = transform;
            _rigidbody = rigidbody;
        }
        
        public override NodeState Evaluate()
        {
            Vector3 myPos = _transform.position;
            Vector3 targetPos = (Vector3) getData("finalPos");

            // Charge at target if not near the target, which is a bit behind the player
            if ((myPos - targetPos).magnitude > 0.5)
            {
                // Adjust charge
                Vector3 velocity = (targetPos - _transform.position).normalized;
                velocity *= GhoulBT._shadowChargeSpeed;

                // Charge at player
                _rigidbody.velocity = velocity;

                _state = NodeState.RUNNING;
                return NodeState.RUNNING;
            }

            _rigidbody.velocity *= 0;
            _state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
    }
}
