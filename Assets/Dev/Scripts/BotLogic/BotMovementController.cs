using UnityEngine;
using Zenject;

namespace Dev.BotLogic
{
    public class BotMovementController : ITickable
    {
        private Bot _bot;

        public BotMovementController(Bot bot)
        {
            _bot = bot;
        }

        public void Move(Vector2 direction, float moveStep)
        {
            _bot.transform.position += (Vector3)direction * moveStep;
        }

        public void Tick()
        {
            
        }
    }
}