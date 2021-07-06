using UnityEngine;
namespace ShootEmUp.TriggerCircles
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class TriggerCircle : MonoBehaviour
    {
        public Triggers.CircleTriggers typeOfTrigger;

    }
}
