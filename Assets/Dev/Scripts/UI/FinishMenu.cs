using Dev.ScoreLogic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Dev.Scripts.UI.PopUpsAndMenus
{
    public class FinishMenu : Menu
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _scoreText;
        
        private ScoreService _scoreService;

        [Inject]
        private void Construct(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }
        
        public void Setup(bool isLosed)
        {
            string titleText;
            
            if (isLosed)
            {
                titleText = $"You lost, try your luck again!";
            }
            else
            {
                titleText = $"You beat all enemies!";
            }

            _title.text = titleText;
            _scoreText.text = $"You scored {_scoreService.Score}!";
        }
        
    }
}