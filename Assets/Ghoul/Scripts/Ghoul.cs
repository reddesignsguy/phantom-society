using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine; 

public class Ghoul : MonoBehaviour
{
    // References
    [SerializeField] private Player _player;
    [SerializeField] private int _shadowDashDamage;


    public bool inGhoulForm;
    private Animator animator;
    private GhoulBT _ghoulBT;
    private BoxCollider2D _collider;

    [SerializeField] private int _maxHealth = 50;
    [SerializeField] private int _health;

    private void Awake()
    {
        _health = _maxHealth;
        animator = GetComponent<Animator>();
        _ghoulBT = gameObject.GetComponent<GhoulBT>();
        inGhoulForm = false;
        _collider = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (inGhoulForm && collision.gameObject.name == "Player")
        {
            _player.depleteHealth(_shadowDashDamage);
        }
    }

    public void transformIntoGhoul()
    {
        inGhoulForm = true;
        animator.SetBool("inGhoulForm", inGhoulForm);
        _ghoulBT.enabled = true;
        _collider.isTrigger = true;
    }

    public void depleteHealth(int value)
    {
        _health -= value;

        // Death
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }


}
