using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class DamagePlatform : MonoBehaviour
{
	[SerializeField] private Color _activationColor;
	[SerializeField] private Color _damageColor;
	[SerializeField] private float _startDamageCooldown = 1f;
	[SerializeField] private float _damageCooldown = 5f;
	[SerializeField] private int _damage = 50;
	[SerializeField] private float _colorChangeDuration = 0.25f;

	private MeshRenderer _meshRenderer;
	private Color _baseColor;
	private bool _isActive;
	private float _damageTimer;
	private PlayerHealth _playerHealth;

	private void Awake()
	{
		_meshRenderer = GetComponent<MeshRenderer>();
		_baseColor = _meshRenderer.material.color;
	}

	private void Update()
	{
		if (!_isActive)
			return;
		
		if (_damageTimer <= 0)
		{
			_meshRenderer.material.DOColor(_damageColor, _colorChangeDuration);
			
			_playerHealth.TakeDamage(_damage);

			_damageTimer = _damageCooldown;
		}
		else
		{
			_damageTimer -= Time.deltaTime;
		}

		if (_meshRenderer.material.color == _damageColor)
		{
			_meshRenderer.material.DOColor(_activationColor, _colorChangeDuration);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth))
		{
			_playerHealth = playerHealth;
			ActivatePlatform();
		}
	}

	private void OnCollisionExit(Collision other)
	{
		if (other.gameObject.GetComponent<PlayerHealth>())
		{
			_playerHealth = null;
			DeactivatePlatform();
		}
	}

	private void ActivatePlatform()
	{
		_meshRenderer.material.DOColor(_activationColor, _colorChangeDuration);
		
		_damageTimer = _startDamageCooldown;
		
		_isActive = true;
	}

	private void DeactivatePlatform()
	{
		_meshRenderer.material.DOColor(_baseColor, _colorChangeDuration);
		
		_isActive = false;
	}
}