using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StackSystem
{
    public class StackController : MonoBehaviour
    {
        public event Action<RainbowCube> OnStackAdded;
        public event Action OnStackUsed;
        public event Action OnStackLost;

        [ShowInInspector,ReadOnly,PropertyOrder(-1)] public int Stack { get; private set; }

        public void AddStack(RainbowCube obj)
        {
            Stack++;
            OnStackAdded?.Invoke(obj);
        }
        public void UseStack()
        {
            Stack--;
            OnStackUsed?.Invoke();
        }

        public void LoseStack()
        {
            Stack--;
            OnStackLost?.Invoke();
        }
    }
}

