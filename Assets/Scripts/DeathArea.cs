using System;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
	public static DeathArea Instance { get; private set; }
	
	public event EventHandler OnPlayerDied;

	private void Awake()
	{
		Instance = this;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<PlayerHealth>())
		{
			OnPlayerDied?.Invoke(this, EventArgs.Empty);
		}
	}
}