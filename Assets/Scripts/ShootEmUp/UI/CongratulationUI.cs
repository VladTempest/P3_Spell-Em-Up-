using ShootEmUp.ObserverPattern;
using UnityEngine;
namespace ShootEmUp.UI
{
    public class CongratulationUI : ShownHidinUIBase
    {
        void Start()
        {
            HideThisUIObject();
        }
        public override void Subscribe()
        {
            EventBroker.PlayerWon += ShowThisUIObject;
        }
        public override void Unsubscribe()
        {
            EventBroker.PlayerWon -= ShowThisUIObject;
        }
    }
}
