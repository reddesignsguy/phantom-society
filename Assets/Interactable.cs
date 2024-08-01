using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    protected DefaultPlayerActions _defaultPlayerActions;
    protected InputAction _interactAction;
    protected DialogBox _dialogBox;
    protected bool _enteredDialog;
    protected int _dialogIndex = 0;
    //private Coroutine _dialogCoroutine;


    [SerializeField] private Player _player;
    public string[] dialogs;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        _defaultPlayerActions = new DefaultPlayerActions();
    }

    protected virtual void Start()
    {
        _dialogBox = GameObject.FindAnyObjectByType<DialogBox>();
    }


    protected virtual void OnEnable()
    {
        _interactAction = _defaultPlayerActions.Player.Interact;
        _interactAction.Enable();


        _interactAction.performed += OnInteract;
    }

    protected virtual void OnDisable()
    {
        _interactAction.Disable();

    }

    public virtual void OnInteract(InputAction.CallbackContext context)
    {
        
        if (!_enteredDialog && (_player.transform.position - gameObject.transform.position).magnitude < 1)
        {
            _enteredDialog = true;
            _dialogBox.showDialog();
            _dialogBox.setText(getCurDialog(_dialogIndex));

        } else if (_enteredDialog)
        {
            _dialogIndex += 1;

            // Edge: Out of dialog options
            if (_dialogIndex >= dialogs.Length)
            {
                _dialogIndex = 0;
                _enteredDialog = false;
                _dialogBox.hideDialog();
                return;
            }

            _dialogBox.setText(getCurDialog(_dialogIndex));
        }
    }

    protected virtual string getCurDialog(int index)
    {
        string curDialog = dialogs[_dialogIndex];
        return curDialog;
    }

    //private void OnEnable()
    //{
    //    _dialogCoroutine = StartCoroutine(returnToPoolAfterTime());
    //}

    //private IEnumerator startDialog()
    //{
    //    // Destroy this projectile after a certain time interval
    //    float elapsedTime = 0.0f;
    //    while (elapsedTime < _timeToLive)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    ObjectPoolManager.returnObjectToPool(gameObject);
    //}
}