using System.Collections.Generic;
using ShootEmUp.Characteristics;
using UnityEngine;

namespace ShootEmUp.Settings.EnemiesSettings
{

    [CreateAssetMenu(fileName = "EnemiesListSettings", menuName = "Settings/EnemiesListSettings", order = 0)]
    public class EnemiesListSettings : ScriptableObject
    {
        public List<EnemyCharacteristics> list;

    }
}
