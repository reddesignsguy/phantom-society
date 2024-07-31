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

	private bool[] keypoints;
	

	// Use this for initialization
	void Start()
	{
		_health = _maxHealth;
        _healthBar.setInitialHealth(_maxHealth);
		_rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

		keypoints = new bool[6];

    }

	public void depleteHealth(int val)
	{
		_health -= val;

		_healthBar.updateHealth(_health);
    }

	public bool reachedKeypointRequirement(int keypoint)
	{
		for (int i = 0; i < keypoint; i ++)
		{
			if (keypoints[i] == false)
			{
				return false;
			}
		}

		return true;
	}

	public void progressKeyPoint(int keyPoint)
	{
		keypoints[keyPoint] = true;

		print("Keypoints:");
		// Debug:
		for (int i = 0; i < keypoints.Length; i ++)
		{
			print("   " + i + ": " + keypoints[i]);
		}
		print("--------");
	}

	public int getKeypoint()
	{
		for (int i = 0; i < keypoints.Length; i ++ )
		{
			if (keypoints[i] == false)
			{
				return i;
			}
		}

		// All keypoints completed
		return -1;
	}
}

