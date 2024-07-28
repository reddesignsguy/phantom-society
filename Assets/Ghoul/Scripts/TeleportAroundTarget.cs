using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{

    public class TeleportAroundTarget : Node
    {

        private Rigidbody2D _rigidbody;
        private float _targetRadius;
        private Transform _targetTransform;

        public TeleportAroundTarget(Rigidbody2D rigidbody, float targetRadius, Transform targetTransform) 
        {
            _targetRadius = targetRadius;
            _targetTransform = targetTransform;
            _rigidbody = rigidbody;
        }

        public override NodeState Evaluate()
        {
            // Construct a random target position along the circle of _targetRadius with the player's pos as the center
            Vector3 targetPos = _targetTransform.position;
            Vector3 finalPos = targetPos + (getRandomUnitVector() * _targetRadius);

            // Teleport ghoul
            _rigidbody.position = finalPos;

            return success();
        }

        private Vector3 getRandomUnitVector() {
            float randDir = Random.Range(0f, Mathf.PI * 2);
            Vector3 randVec = new Vector3(Mathf.Cos(randDir), Mathf.Sin(randDir), 0);
            return randVec;
        }

    }

}