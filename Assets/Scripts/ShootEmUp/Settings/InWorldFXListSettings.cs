using System.Collections.Generic;
using ShootEmUp.FX;
using UnityEngine;

namespace ShootEmUp.Settings
{

    [CreateAssetMenu(fileName = "InWorldFXListSettings", menuName = "Settings/InWorldFXListSettings", order = 0)]
    public class InWorldFXListSettings : ScriptableObject
    {
        public List<InWorldFXClass> list;

    }
}

