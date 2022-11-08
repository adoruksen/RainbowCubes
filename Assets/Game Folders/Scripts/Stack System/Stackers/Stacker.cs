using InteractionSystem;
using DG.Tweening;
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
        [SerializeField] private TrailRenderer _trailRenderer;

        #endregion

        [Header("Variables")]
        #region Variables
        [SerializeField] private Transform _emojis;
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

        public void GetRandomEmoji()
        {
            var randomIndex = Random.Range(0, _emojis.childCount);
            var emoji = _emojis.GetChild(randomIndex).gameObject;
            emoji.transform.SetPositionAndRotation(transform.position + _addPosEmoji, Quaternion.identity);
            emoji.SetActive(true);
            emoji.transform.DOShakeRotation(1f).OnComplete(() => emoji.SetActive(false));
        }

        public void SetTrail(Color color)
        {
            _trailRenderer.enabled = true;
            var _renderer = _trailRenderer.GetComponent<Renderer>();
            _renderer.material.color = color;
            _trailRenderer.transform.position = new Vector3(transform.position.x, -.4f, transform.position.z);
        }

        public void SetTrailColorOnLost(Color color)
        {
            var _renderer = _trailRenderer.GetComponent<Renderer>();
            _renderer.material.color = color;
        }
    }
}

