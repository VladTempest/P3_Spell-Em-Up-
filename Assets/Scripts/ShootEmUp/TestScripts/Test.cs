using ShootEmUp.Managers;
using ShootEmUp.Skills;
using ShootEmUp.Spawners;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public class Test : MonoBehaviour
    {
        [SerializeField]
        private SkillsHandler _skillsHandler = null;
        [SerializeField]
        private bool _isSkillTest = false;

        [SerializeField]
        private bool _isGoodsSpawnerTest = false;
        [SerializeField]
        private GoodsSpawner _goodsSpawner;

        


        void Start()
        {
            var _testSceneManager = SceneManager.Instance;
        }
        void Update()
        {
            if (_isSkillTest)
            {
             if (Input.GetKeyDown(KeyCode.Mouse1))
                         {
                             _skillsHandler.CreateFireSplash();
                         }
                         if (Input.GetKeyDown(KeyCode.Mouse2))
                         {
                             _skillsHandler.ChangeShooterProjectile();
                         }
                         
                         if (Input.GetKeyDown(KeyCode.Q))
                         {
                             _skillsHandler.LaunchHellFire();
                         }   
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                GameManager.Instance.CurrentMana += 5;
            }

            if (_isGoodsSpawnerTest)
            {

                if (Input.GetKeyDown(KeyCode.G))
                {
                    _goodsSpawner.InstantiateCollectable();
                }
            }

        }
    }
}
