using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyPoint : Interactable
{
    private Player _player;
    public List<StringList> keypointDialogs;
    public int[] _keypoints;
    private int keypointIndex;


    protected override void Awake()
    {
        base.Awake();
        //base.OnEnable();
        _player = GameObject.FindAnyObjectByType<Player>();
        keypointIndex = 0;

    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

    }

    public override void OnInteract(InputAction.CallbackContext context)
    {
        if (!_player.reachedKeypointRequirement(_keypoints[keypointIndex]))
            return;

        // First interaction
        if (!_enteredDialog && (_player.transform.position - gameObject.transform.position).magnitude < 1)
        {
            //print("First interaction");

            _enteredDialog = true;
            _dialogBox.showDialog();

            // Edge: Player is trying to talk to the same keypoint again, so use the old set of dialogs
            print(_player.getKeypoint());
            if (keypointIndex != 0 && _keypoints[keypointIndex - 1] == _player.getKeypoint() - 1)
                keypointIndex -= 1;

            _dialogBox.setText(getCurDialog(_dialogIndex));
        }

        // Continuing dialog
        else if (_enteredDialog)
        {
            //print("Ongoing interaction" + getCurDialog(_dialogIndex));
            _dialogIndex += 1;

            // Edge: Out of dialog options
            if (_dialogIndex >= keypointDialogs[keypointIndex].strings.Count)
            {
                _dialogIndex = 0;
                _enteredDialog = false;
                _dialogBox.hideDialog();

                // This interacable is the next thing the player has to talk to, so progress the player
                if (_player.reachedKeypointRequirement(_keypoints[keypointIndex]))
                {
                    print("leveling up");
                    _player.progressKeyPoint(_keypoints[keypointIndex]);
                    keypointIndex += 1;
                }
                return;
            }

            _dialogBox.setText(getCurDialog(_dialogIndex));
        }
    }

    protected override string getCurDialog(int index)
    {
        return keypointDialogs[keypointIndex].strings[index];
    }
}

[System.Serializable]
public class StringList
{
    public List<string> strings;
}
