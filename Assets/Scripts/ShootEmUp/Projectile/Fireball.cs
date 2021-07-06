using System;
using ShootEmUp.FX;
using ShootEmUp.Managers;
using ShootEmUp.Sounds;
using UnityEngine;
namespace ShootEmUp.Projectile
{
    public class Fireball : ProjectileClass
    {
        [SerializeField]
        private protected Rigidbody2D _rigidBody;
        

        private void OnCollisionEnter2D(Collision2D other)
        {
            CreateExplosionOnContact();
            DestroyThisProjectile();
        }
        

        private void CreateExplosionOnContact()
        {
            try
            {
                if (!GameManager.Instance.isGameActive) return;
                if (InWorldFXPlayer.Instance == null) return;
            }
            catch (Exception e)
            {
                Debug.Log("InWorldFXPlayerException/GameManager while destroyng " + gameObject.name);
                return;
            }
                    
            InWorldFXPlayer.Instance.InstantiateFX(InWorldFXs.FireBallExplosion,transform.position);
            InWorldFXPlayer.Instance.PlayFXCameraShake();
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.FireBall_Explosion,transformOfPlayPoint:transform);
            

        }
    }
}
