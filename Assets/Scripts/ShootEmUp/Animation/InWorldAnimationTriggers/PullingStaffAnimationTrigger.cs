using System;
using UnityEngine;
namespace ShootEmUp.Animation.InWorldAnimationTriggers
{
    public class PullingStaffAnimationTrigger : MonoBehaviour
    {
        [SerializeField]
        private AnimationPlayer _animationPlayer;
        private void OnTriggerEnter2D(Collider2D other)
        {
            var wallComponent = other.gameObject.GetComponent<WallClass>();
            if (wallComponent != null)
            {
                _animationPlayer.StartStaffPullingAnimation();
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            var wallComponent = other.gameObject.GetComponent<WallClass>();
            if (wallComponent != null)
            {
                _animationPlayer.StopStaffPullingAnimation();
            }
        }
    }
}
