using System.Collections.Generic;
using ShootEmUp.Sounds;
using UnityEngine;

namespace ShootEmUp.Settings
{

    [CreateAssetMenu(fileName = "SoundtracksListSettings", menuName = "Settings/SoundtracksListSettings", order = 0)]
    public class SoundtracksListSettings : ScriptableObject
    {
        public List<Soundtrack> list;

    }
}
