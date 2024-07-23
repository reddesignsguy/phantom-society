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

    private Animator _animator;

    private Vector2 _moveDir;
    private int _lookDir;

    // Settings
    public float _speed = 350;

    private void Awake()
    {
        _defaultPlayerActions = new DefaultPlayerActions();
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _moveAction = _defaultPlayerActions.Player.Move;
        _moveAction.Enable();

        _defaultPlayerActions.Player.Jump.performed += OnJump; // Assigns a listener to the event w/ a callback
        _defaultPlayerActions.Player.Jump.Enable();

        _moveAction.performed += OnMove;


    }

    private void OnDisable()
    {

        _moveAction.Disable();
        _defaultPlayerActions.Player.Jump.Disable();

    }

    /* Returns an integer representing the player-character's look direction where
       0 = down
       1 = right
       2 = up
       3 = left

    @params  context  An input action which contains the 2D movement/translation vector of the player-character 
    */
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 newDir = context.ReadValue<Vector2>(); // Direction in polar coordinates 

        // The newest dir is the newest direction the player tries to go to
        float oldHoriz = _moveDir[0];
        float oldVert = _moveDir[1];
        float newHoriz = newDir[0];
        float newVert = newDir[1];
        
        // Only one of these is true
        bool horizChanged = System.Math.Sign(oldHoriz) != System.Math.Sign(newHoriz);
        bool vertChanged = System.Math.Sign(oldVert) != System.Math.Sign(newVert);
        Debug.Assert(!(horizChanged == true && horizChanged == vertChanged), "The horizontal and vertical move InputAction were both detected to be changed. Expected only one to be changed.");

        if (horizChanged)
        {
            if (newHoriz != 0)
                _lookDir = getDirFromPolar(Mathf.RoundToInt(newHoriz), true);
            else
                _lookDir = getDirFromPolar(Mathf.RoundToInt(newVert), false);

        }
        else if (vertChanged)
        {
            if (newVert != 0)
                _lookDir = getDirFromPolar(Mathf.RoundToInt(newVert), false);
             else
                _lookDir = getDirFromPolar(Mathf.RoundToInt(newHoriz), true);
        }

        _animator.SetInteger("dir", _lookDir);
    }

    private int getDirFromPolar(int polar, bool getHorizontal)
    {
        int dir = -1;
        if (getHorizontal)
            dir = polar == 1 ? 1 : 3;
        else
            dir = polar == 1 ? 2 : 0;

        return dir;
    }

    private void OnJump(InputAction.CallbackContext context)
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 newDir = _moveAction.ReadValue<Vector2>();
        //_lookDir = getLookDir(_moveDir, newDir);

        _moveDir = newDir;

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

        _animator.SetFloat("speed", newVelocity.magnitude);

        _rigidBody.velocity = newVelocity;

    }

    int getLookDir(Vector2 oldDir, Vector2 newDir)
    {
        int horiz = (int) newDir[0];
        int vert = (int) newDir[1];

        bool goingRight = horiz == 1;
        bool goingUp = vert == 1;

        // If two valid directions are pressed, then
        // the look direction is going to be the latest new key pressed
        if ((goingRight && goingUp) || (!goingRight && !goingUp))
        {
            return -1;
        }

        return -1;
    }
}
