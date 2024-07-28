using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class RangedShadowSlash : Node
    {

        private Transform _transform;
        private Transform _targetTransform;
        private GameObject _shadowSlashProjectile;

        public RangedShadowSlash(Transform transform, Transform targetTransform) {
            _transform = transform;
            _targetTransform = targetTransform;
            _shadowSlashProjectile = GameObject.Find("ShadowSlashProjectile");
        }

        public override NodeState Evaluate()
        {

            Vector3 spawnPos = _transform.position;
            Vector3 dirToTarget = (_targetTransform.position - spawnPos).normalized;
            Quaternion rotationPointingToTarget = Quaternion.LookRotation(Vector3.forward , dirToTarget);
            rotationPointingToTarget *= Quaternion.Euler(0, 0, -90);

            // Instantiate the projectile object from pool
            GameObject obj = ObjectPoolManager.spawnObject(_shadowSlashProjectile, spawnPos, rotationPointingToTarget);
            
            // Make projectile move
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            Vector3 vel = dirToTarget * GhoulBT._rangedShadowSlashSpeed;
            rb.velocity = vel;

            return success();
        }
    }
}