using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{

    private DefaultPlayerActions _defaultPlayerActions;
    private InputAction _moveAction;

    private Rigidbody2D _rigidBody;

    public Animator _animator;

    private Vector2 _moveDir;

    // Settings
    public float _speed = 350;

    private void Awake()
    {
        _defaultPlayerActions = new DefaultPlayerActions();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _moveAction = _defaultPlayerActions.Player.Move;
        _moveAction.Enable();

        _defaultPlayerActions.Player.Jump.performed += OnJump; // Assigns a listener to the event w/ a callback
        _defaultPlayerActions.Player.Jump.Enable();
        
    }

    private void OnDisable()
    {

        _moveAction.Disable();
        _defaultPlayerActions.Player.Jump.Disable();

    }

    private void OnJump(InputAction.CallbackContext context)
    {

    }

    // Update is called once per frame
    void Update()
    {
        _moveDir = _moveAction.ReadValue<Vector2>();

        //if (_moveDir.y < 0)
        //{
        //    _animation.Play();
        //}

    }

    private void FixedUpdate()
    {
        Vector2 newVelocity = new Vector2();

        newVelocity.x = _moveDir.x;
        newVelocity.y = _moveDir.y;
        newVelocity.Normalize();
        newVelocity *= _speed;

        _rigidBody.velocity = newVelocity;

    }
}
