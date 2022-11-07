using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace StackSystem
{
    public abstract class Stacker : MonoBehaviour
    {
        [Header("Components")]
        #region Components
        private StackController _stackController;
        private StackVisualController _stackVisual;
        private CharacterController _characterController;
        private AudioSource _audioSource;
        #endregion

        [Header("Variables")]
        #region Variables
        [SerializeField] private GameObject[] _emojiList = new GameObject[4];
        private Vector3 _punchScale = Vector3.one*.3f;
        private Vector3 _addPosEmoji = new(-1f, 6f, 0f);
        private Vector3 _trailPos = new(0f, -.5f, 0f);
        #endregion


        private void Awake()
        {
            _characterController = GetComponentInParent<CharacterController>();
            _stackController = GetComponent<StackController>();
            _stackVisual = GetComponent<StackVisualController>();
            _audioSource = GetComponent<AudioSource>();
        }
    }
}

