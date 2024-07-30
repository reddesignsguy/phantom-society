using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health;

    public HealthBar _healthBar;
	private Rigidbody2D _rb;
	private BoxCollider2D _collider;
	

	// Use this for initialization
	void Start()
	{
		_health = _maxHealth;
        _healthBar.setInitialHealth(_maxHealth);
		_rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

	public void depleteHealth(int val)
	{
		_health -= val;

		_healthBar.updateHealth(_health);
    }
	

}

