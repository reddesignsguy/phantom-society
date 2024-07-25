using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class FightBehavior : MonoBehaviour
{
    // Components
    private Animator _animator;
    private Transform _transform;
    private Rigidbody2D _rigidbody;

    // References
    public GameObject _target;

    // Params
    public float _shadowChargeSpeed;

    private float _timer;
    public float _moveInterval;
    private bool moveInProgress;

    private void Awake()
    {
        moveInProgress = false;
        _timer = 0;
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animator.SetFloat("speed", 3);

    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        bool timeToMakeAMove = _timer > _moveInterval;

        if (timeToMakeAMove && !moveInProgress)
        {
            moveInProgress = true;
            ShadowRush();
            _timer = 0;
        }

    }

    void ShadowRush()
    {
        // Calculate direction towards player
        Transform targetTransform = _target.GetComponent<Transform>();
        Vector3 targetPos = targetTransform.position;
        Vector3 myPos = _transform.position;
        Vector3 velocity = (targetPos - myPos).normalized;

        // Adjust charge
        velocity *= _shadowChargeSpeed;
     
        // Charge at player
        _rigidbody.velocity = new Vector2(velocity.x, velocity.y);

        // Listen for when my position has passed the target
        // When event happens, set moveInProgress to false

    }
}
