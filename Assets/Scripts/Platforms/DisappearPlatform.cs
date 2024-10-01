using UnityEngine;

public class DisappearPlatform : MonoBehaviour
{
	[SerializeField] private GameObject[] _parts;
	[SerializeField] private float _disappearCooldown;

	private float _disappearTimer;
	private int index;

	private void Awake()
	{
		_disappearTimer = _disappearCooldown;
		index = 0;
	}

	private void Update()
	{
		if (_disappearTimer <= 0)
		{
			_parts[index].gameObject.SetActive(true);
			index = (index + 1) % _parts.Length;
			_parts[index].gameObject.SetActive(false);

			_disappearTimer = _disappearCooldown;
		}
		else
		{
			_disappearTimer -= Time.deltaTime;
		}
	}
}