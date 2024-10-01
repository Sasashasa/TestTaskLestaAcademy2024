using System;
using UnityEngine;

public class WinArea : MonoBehaviour
{
	public static WinArea Instance { get; private set; }
	
	public event EventHandler OnPlayerWin;

	private void Awake()
	{
		Instance = this;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<PlayerHealth>())
		{
			OnPlayerWin?.Invoke(this, EventArgs.Empty);
		}
	}
}