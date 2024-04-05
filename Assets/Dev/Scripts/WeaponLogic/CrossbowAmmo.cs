using UnityEngine;

namespace Dev.WeaponLogic
{
    public class CrossbowAmmo : ProjectileWeaponAmmo
    {
        protected override void Move()
        {
            Vector3 position = transform.position;

            position.y += _setupContext.Direction.y * (Time.deltaTime * _setupContext.Speed);
            
            position.x = Mathf.Lerp(position.x,_setupContext.StartPos.x + Mathf.Sin(Time.time * 6), Time.deltaTime) ;

            transform.position = position;
        }
    }
}