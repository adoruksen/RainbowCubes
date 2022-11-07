using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputManager : MonoBehaviour
{
    private static MyInputManager _instance;
    public static InputModule Module => _instance._module;
    [SerializeField] private InputModule _module;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        Module.Update();
    }
}
