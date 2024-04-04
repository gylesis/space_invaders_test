using Dev.PlayerLogic;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dev
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;
        
        private Player _player;

        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }
    
        private void Start()
        {
            _player.Health.Changed.TakeUntilDestroy(this).Subscribe((OnHealthChanged));
            _player.Health.ZeroHealth.TakeUntilDestroy(this).Subscribe((unit => OnZeroHealth()));
        }

        private void OnZeroHealth()
        {
            
        }

        private void OnHealthChanged(int health)
        {
            _healthText.text = $"Lives: {health}";
        }
    }
}