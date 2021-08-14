using UnityEngine;
public delegate void IsDeadDelegate();
public class HealthController
{
    public event IsDeadDelegate IsDead;
    private int _health;
    private int _maxHealth;
    private bool _isDead => _health == 0;
    HealthController(int maxHealth, int startHealth)
    {
        _maxHealth = maxHealth;
        _health = startHealth;
    }

    public int GetMaxHealth() => _maxHealth;
    public int GetHealth() => _maxHealth;

    public void SetHealth(int value)
    {
        _health = Mathf.Clamp(value, 0, _maxHealth);
        if (_health == 0) IsDead?.Invoke();
        
    }

    public void SetDamage(int value)
    {
        SetHealth(_health - value);
    }

    public void SetHill(int value)
    {
        SetHealth(_health + value);
    }
}
