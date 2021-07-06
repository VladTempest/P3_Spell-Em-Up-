using UnityEditor;
using UnityEngine;
namespace ShootEmUp.ExtraGizmos
{
    public class TextOnScreenEditor : MonoBehaviour
    {
        [SerializeField]
        private string _textOnScreen = "default";
        void OnDrawGizmos() 
        {
            //Handles.Label(transform.position, _textOnScreen);
        }
    }
}
