using UnityEngine;

namespace CameraSystem
{
    [CreateAssetMenu(fileName = "CameraConfig",menuName = "Game/CameraSystem",order =0)]
    public class CameraConfig : ScriptableObject
    {
        public Vector3 offset;
        public Vector3 rotation;
    }
}

