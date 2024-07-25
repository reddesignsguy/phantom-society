using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class LookAtTarget : Node
    {

        // Params
        private float _shadowChargeLeftOverDistance;

        // Refs
        private Animator _animator;
        private Transform _targetTransform;
        private Transform _transform;

        public LookAtTarget(Animator animator, Transform transform, Transform targetTransform, float shadowChargeLeftOverDistance)
        {
            _animator = animator;
            _targetTransform = targetTransform;
            _transform = transform;
            _shadowChargeLeftOverDistance = shadowChargeLeftOverDistance;
        }

        public override NodeState Evaluate()
        {
            // Calculate final pos
            Vector3 targetPos = _targetTransform.position;
            Vector3 myPos = _transform.position;

            // Make ghoul go a bit past player
            Vector3 dir = (targetPos - myPos).normalized;
            Vector3 finalPos = targetPos + (dir * _shadowChargeLeftOverDistance);

            bool finalPosTooFar = (finalPos - myPos).magnitude > GhoulBT._maxShadowChargeDistance;
            if (finalPosTooFar) {
                finalPos = myPos + (dir * GhoulBT._maxShadowChargeDistance);
            }


            // Update context
            setData("finalPos", finalPos);

            // Turn sprite towards pos

            // Update context: share "targetPos"


            _state = NodeState.SUCCESS;
            return NodeState.SUCCESS;
        }
    }
}