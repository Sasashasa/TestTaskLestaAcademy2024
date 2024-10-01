using UnityEngine;

public class WindPlatform : MonoBehaviour
{
	[SerializeField] private float _changeWindDirectionCooldown;
	[SerializeField] private float _windPower;

	private Vector3 _currentWindDirection;
	private float _changeWindDirectionTimer;

	private void Awake()
	{
		_currentWindDirection = GetRandomWindDirection();
	}


	private void Update()
	{
		if (_changeWindDirectionTimer <= 0)
		{
			_currentWindDirection = GetRandomWindDirection();
			
			_changeWindDirectionTimer = _changeWindDirectionCooldown;
		}
		else
		{
			_changeWindDirectionTimer -= Time.deltaTime;
		}
	}

	private void OnCollisionStay(Collision other)
	{
		if (other.gameObject.TryGetComponent(out PlayerMovement player))
		{
			player.GetComponent<Rigidbody>().AddForce(_currentWindDirection * _windPower);
		}
	}

	private Vector3 GetRandomWindDirection()
	{
		Vector3 direction = new Vector3
		{
			x = Random.Range(-1f, 1f),
			z = Random.Range(-1f, 1f)
		};

		return direction.normalized;
	}
}