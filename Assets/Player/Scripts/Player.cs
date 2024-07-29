using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health;

    public HealthBar _healthBar;

	// Use this for initialization
	void Start()
	{
		_health = _maxHealth;
        _healthBar.setInitialHealth(_maxHealth);
    }


	// Update is called once per frame
	void Update()
	{
        //_healthBar.updateHealth(_health);
		
	}
}

