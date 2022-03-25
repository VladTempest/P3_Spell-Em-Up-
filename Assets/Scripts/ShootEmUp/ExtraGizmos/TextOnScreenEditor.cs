#if UNITY_Editor
using UnityEditor;
#endif
using UnityEngine;
namespace ShootEmUp.ExtraGizmos
{
    public class TextOnScreenEditor : MonoBehaviour
    {
        [SerializeField]
        private string _textOnScreen = "default";
       #if UNITY_Editor
        void OnDrawGizmos() 
        {
            Handles.Label(transform.position, _textOnScreen);
        }
        #endif
    }
}
