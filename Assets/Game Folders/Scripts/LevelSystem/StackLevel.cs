using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LevelSystem
{
    [Serializable]
    public class StackLevel : LevelPart
    {
        [SerializeField] private StackAreaManager _manager;

        public override GameAreaManager SetupPart(Transform parent, GameAreaManager previousArea = null)
        {
            var manager = Object.Instantiate(_manager, parent);
            if (previousArea != null)
            {
                manager.MoveArea(Vector3.zero);
            }

            return manager;
        }
    }
}