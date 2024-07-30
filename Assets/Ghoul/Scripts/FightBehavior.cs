using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class FightBehavior : MonoBehaviour
{
    // References
    [SerializeField] private Player _player;
    [SerializeField] private int _shadowDashDamage;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            _player.depleteHealth(_shadowDashDamage);
        }
    }
}
