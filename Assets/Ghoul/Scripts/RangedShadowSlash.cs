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
            Quaternion quat = new Quaternion(0.5f, 0.5f , 0, 1); // random rotation

            // Instantiate the projectile object from pool
            Vector3 spawnPos = _transform.position;
            GameObject obj = ObjectPoolManager.spawnObject(_shadowSlashProjectile, spawnPos, quat);

            // Make projectile move
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            Vector3 vel = (_targetTransform.position - spawnPos).normalized;
            vel *= GhoulBT._rangedShadowSlashSpeed;

            rb.velocity = vel;

            return success();
        }
    }
}