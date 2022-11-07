using System.Collections;
using System.Collections.Generic;
using InteractionSystem;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace StackSystem
{
    public abstract class Stacker : MonoBehaviour,IInteractor
    {
        #region Components
        public Rigidbody Rigidbody { get; private set; }
        public StackController StackController { get; private set; }
        public AudioSource AudioSource { get; private set; }

        public CharacterController CharacterController { get; private set; }

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
            Rigidbody = GetComponent<Rigidbody>();
            CharacterController = GetComponentInParent<CharacterController>();
            StackController = GetComponent<StackController>();
            AudioSource = GetComponent<AudioSource>();
        }
    }
}

