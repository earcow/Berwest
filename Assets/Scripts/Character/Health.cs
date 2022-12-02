using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Health: MonoBehaviour
{
    [Tooltip("Максимальное значение здоровья")] [SerializeField] private float _maxHealth = 100f;
    public float MaxHealth => this._maxHealth;

    [Tooltip("Текущее значение здоровья")] [SerializeField] private float _currentHealth;
    public float CurrentHealth => this._currentHealth;

    public void DecreaseAt(GameObject source, float damage)
    {
        _currentHealth -= damage;
        OnHealthChanged?.Invoke(_currentHealth);
    }

    public void IncreaseAt(string source, float heal)
    {
        _currentHealth += heal;
        OnHealthChanged?.Invoke(_currentHealth);
    }

    public void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void OnEnable()
    {
        if (this.TryGetComponent<FallDamage>(out var fallDamage))
        {
            fallDamage.OnFallDamage.AddListener(DecreaseAt);
        }

        OnHealthChanged.AddListener(DeathChecker);
    }
    public void OnDisable()
    {
        if (this.TryGetComponent<FallDamage>(out var fallDamage))
        {
            fallDamage.OnFallDamage.RemoveListener(DecreaseAt);
        }

        OnHealthChanged.RemoveListener(DeathChecker);
    }

    public void DeathChecker(float hp)
    {
        if (hp <= 0)
        {
            Destroy(gameObject, 1f);
        }
    }

    public UnityEvent<float> OnHealthChanged = new UnityEvent<float>();
}
