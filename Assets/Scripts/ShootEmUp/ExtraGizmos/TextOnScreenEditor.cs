#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
namespace ShootEmUp.ExtraGizmos
{
    public class TextOnScreenEditor : MonoBehaviour
    {
        [SerializeField]
        private string _textOnScreen = "default";
       #if UNITY_EDITOR
        void OnDrawGizmos() 
        {
            Handles.Label(transform.position, _textOnScreen);
        }
        #endif
    }
}
