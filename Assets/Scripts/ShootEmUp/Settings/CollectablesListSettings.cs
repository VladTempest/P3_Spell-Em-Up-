using System.Collections.Generic;
using ShootEmUp.Collectables;
using UnityEngine;
namespace ShootEmUp.Settings
{
    [CreateAssetMenu(fileName = "CollectablesListSettings", menuName = "Settings/CollectablesListSettings", order = 0)]
    public class CollectablesListSettings : ScriptableObject
    {
        
        public List<CollectablesClass> list;

    }
    }

