using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }
	
    public event EventHandler OnTakeDamage;
    public event EventHandler OnPlayerDied;
	
    public int HealthValue { get; private set; }
    public int MaxHealth => _maxHealth;
	
    [SerializeField] private int _maxHealth = 250;

    private void Awake()
    {
        Instance = this;
		
        HealthValue = _maxHealth;
    }
	
    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;
        
        HealthValue = Mathf.Clamp(HealthValue - damage, 0, _maxHealth);
		
        OnTakeDamage?.Invoke(this, EventArgs.Empty);

        if (HealthValue == 0)
        {
            OnPlayerDied?.Invoke(this, EventArgs.Empty);
        }
    }
}