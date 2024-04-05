using Dev.PlayerLogic;

namespace Dev.WeaponLogic
{
    public class BotWeaponController : WeaponController
    {
        protected override bool IsPlayer => false;

        protected override void OnAmmoDied(AmmoDieContext ammoDieContext, Weapon weapon)
        {
            bool isPlayer = ammoDieContext.Target.TryGetComponent<Player>(out var player);

            if (isPlayer)
            {
                player.Health.TakeDamage(1);
            }
        }
    }
}