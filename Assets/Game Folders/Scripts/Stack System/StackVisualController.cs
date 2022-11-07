using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace StackSystem
{
    public class StackVisualController : MonoBehaviour
    {
        private StackController _stackController;

        [ShowInInspector] private readonly Stack<RainbowCube> _stackedObjects = new();
        [SerializeField] private float _distance;


        private void Awake()
        {
            _stackController = GetComponent<StackController>();
        }

        private void OnEnable()
        {
            _stackController.OnStackAdded += UpdateVisualAdded;
            _stackController.OnStackLost += UpdateVisualLost;
        }

        private void OnDisable()
        {
            _stackController.OnStackAdded -= UpdateVisualAdded;
            _stackController.OnStackLost -= UpdateVisualLost;
        }

        private void UpdateVisualAdded(RainbowCube obj)
        {
            _stackedObjects.Push(obj);
            var objTransform = obj.transform;
            objTransform.SetParent(transform);
            transform.localPosition = Vector3.up * (_stackController.Stack);
            SetPosition(obj);
            objTransform.localRotation = Quaternion.identity;
        }

        private void UpdateVisualLost()
        {
            var obj = _stackedObjects.Pop();
            obj.transform.DOScale(Vector3.zero, .2f).OnComplete(() =>
            {
                obj.transform.localScale = Vector3.one;
            });
        }

        private void SetPosition(RainbowCube obj)
        {
            obj.transform.position = new Vector3(transform.position.x, transform.position.y - _stackController.Stack, transform.position.z);
        }
    }
}
