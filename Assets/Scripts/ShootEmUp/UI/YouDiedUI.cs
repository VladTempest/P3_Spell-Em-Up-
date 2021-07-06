using ShootEmUp.ObserverPattern;

namespace ShootEmUp.UI
{
    public class YouDiedUI : ShownHidinUIBase
    {
        void Start()
        {
            HideThisUIObject();
        }
      
        public override void Subscribe()
        {
            EventBroker.PlayerDied+=ShowThisUIObject;
        }
        public override void Unsubscribe()
        {
            EventBroker.PlayerDied-=ShowThisUIObject;
        }
    }
}
