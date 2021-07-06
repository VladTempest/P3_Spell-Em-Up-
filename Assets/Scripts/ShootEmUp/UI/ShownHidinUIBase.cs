using ShootEmUp.ObserverPattern;
using UnityEngine;
namespace ShootEmUp.UI
{
    public abstract class ShownHidinUIBase : MonoBehaviour,ISubscribers
    {
        void Awake()
        {
            Subscribe();
        }
        
        private void OnDestroy()
        {
            Unsubscribe();
        }
        

        protected void ShowThisUIObject()
        {
            gameObject.SetActive(true);
        }

        protected void HideThisUIObject()
        {
            gameObject.SetActive(false);
        }
        public abstract void Subscribe();
        
        public abstract void Unsubscribe();

    }
}
