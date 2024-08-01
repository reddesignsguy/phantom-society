using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{

    private DefaultPlayerActions _defaultPlayerActions;
    private InputAction _moveAction;
    private InputAction _attackAction;

    private Rigidbody2D _rigidBody;

    private Animator _animator;

    private Vector2 _moveDir;
    private int _lookDir;

    // States
    private bool _attacking = false;
    private GameObject _attackArea;
    private float _attackingTimer = 0;
    private float _attackTime = 0.25f;

    // Settings
    public float _speed = 350;


    private void Awake()
    {
        _defaultPlayerActions = new DefaultPlayerActions();
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _attackArea = transform.GetChild(2).gameObject;
    }



    private void OnEnable()
    {
        _moveAction = _defaultPlayerActions.Player.Move;
        _moveAction.Enable();

        _defaultPlayerActions.Player.Jump.performed += OnJump; // Assigns a listener to the event w/ a callback
        _defaultPlayerActions.Player.Jump.Enable();

        _attackAction = _defaultPlayerActions.Player.Fire;
        _attackAction.Enable();
        _attackAction.performed += OnAttack;

        _moveAction.performed += OnMove;
    }

    private void OnDisable()
    {

        _moveAction.Disable();
        _defaultPlayerActions.Player.Jump.Disable();
        _attackAction.Enable();

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

        UpdateAttackSettings();

        _animator.SetInteger("dir", _lookDir);
    }

    private void UpdateAttackSettings()
    {
        switch (_lookDir)
        {
            case 0:
                _attackArea.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 1:
                _attackArea.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 2:
                _attackArea.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case 3:
                _attackArea.transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        _attacking = true;
        _attackArea.SetActive(_attacking);
    }

    private int getDirFromPolar(int polar, bool getHorizontal)
    {
        int dir;
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
        // Update direction
        Vector2 newDir = _moveAction.ReadValue<Vector2>();
        _moveDir = newDir;

        if (_attacking)
        {
            _attackingTimer += Time.deltaTime;

            if (_attackingTimer > _attackTime)
            {
                _attacking = false;
                _attackArea.SetActive(false);
                _attackingTimer = 0;
            }
        }
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
