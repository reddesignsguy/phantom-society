using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] private Ghoul _ghoul;

    public int damage = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.tag);
        if (collision.gameObject.tag == "Person")
        {
            print("mission failed");
        }
        else if (collision.gameObject.tag == "Ghoul")
        {
            print("Ghoul hit");
            if (!_ghoul.inGhoulForm)
            {
                print("transforming ghoul");
                _ghoul.transformIntoGhoul();
            }
            else
            {
                _ghoul.depleteHealth(damage);
            }
        }
    }
}
