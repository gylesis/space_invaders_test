using UniRx;
using UnityEngine;

namespace Dev
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;

        private int _currentHealth;

        public int CurrentHealth => _currentHealth;
        
        public Subject<Unit> ZeroHealth { get; } = new Subject<Unit>();
        public Subject<int> Changed { get; } = new Subject<int>();
            
        private void Awake()
        {
            ResetHealth();
        }

        public void ResetHealth()
        {
            _currentHealth = _maxHealth;
            Changed.OnNext(_currentHealth);
        }

        private void Start()
        {
            Changed.OnNext(_currentHealth);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                ZeroHealth.OnNext(Unit.Default);
            }
            
            Changed.OnNext(_currentHealth);
        }
        
    }
}