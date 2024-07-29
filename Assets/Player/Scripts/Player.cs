using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health;

    public HealthBar _healthBar;

	// Use this for initialization
	void Start()
	{
		print("init health");
		_health = _maxHealth;
        _healthBar.setInitialHealth(_maxHealth);
    }

	float timer;
	// Update is called once per frame
	void Update()
	{
        _healthBar.updateHealth(_health);

		print("udating");
		timer += Time.deltaTime;
		if (timer > 1)
		{
			timer = 0;
			_health -= 1;
			print("Health");
			print(_health);
		}
	}
}

