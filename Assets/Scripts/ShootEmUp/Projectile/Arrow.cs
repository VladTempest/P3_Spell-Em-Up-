using System;
using ShootEmUp.Characteristics;
using ShootEmUp.Sounds;
using UnityEngine;

namespace ShootEmUp.Projectile
{
    public class Arrow : ProjectileClass
    {
        private float _radiusOfArrowStucking = 0.4f;
        [SerializeField]
        private CircleCollider2D _colliderOfProjectile = null;
        [SerializeField]
        private protected Rigidbody2D _rigidBody;

        private void OnCollisionEnter2D(Collision2D other)
        {

        
                if (other.gameObject.GetComponent<CharacterCharacteristics>() != null)
                {
                    SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.Arrow_HitSomeThing,transformOfPlayPoint:transform);
                    MakeArrowStuckInCharacter(other);
                   
                }
                else if (other.gameObject.GetComponent<ProjectileClass>()==null)
                {
                    SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.Arrow_HitSomeThing,transformOfPlayPoint:transform);
                    MakeArrowStuckInObjects(other);
                }
                else
                {
                    SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.Arrow_HitNothing,transformOfPlayPoint:transform);
                    _destroyTimer = 0;
                }
                DestroyThisProjectile(); 
            
        }


        private void MakeArrowStuckInCharacter(Collision2D other)
        {

            _rigidBody.isKinematic = true;
            _rigidBody.simulated = false;
            _colliderOfProjectile.enabled = false;
            transform.parent = other.transform;
            var transformPosition = transform.localPosition;
            var x = Mathf.Clamp(transformPosition.x, -_radiusOfArrowStucking, _radiusOfArrowStucking);
            var y = Mathf.Clamp(transformPosition.y, -_radiusOfArrowStucking, _radiusOfArrowStucking);
            transform.localPosition = new Vector3(x, y, 0);
        }

        private void MakeArrowStuckInObjects(Collision2D other)
        {
            if(other.gameObject.GetComponent<ProjectileClass>()) return;
            _rigidBody.isKinematic = true;
            _colliderOfProjectile.enabled = false;
            _rigidBody.simulated = false;
            transform.parent = other.transform;
        }
    }
}
