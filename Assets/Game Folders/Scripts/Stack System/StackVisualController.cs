using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace StackSystem
{
    public class StackVisualController : MonoBehaviour
    {
        private StackController _stackController;
        [SerializeField] private Transform _modelTransform;

        [ShowInInspector] private readonly Stack<RainbowCube> _stackedObjects = new();


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
            objTransform.SetParent(_modelTransform);
            objTransform.DOPunchScale(Vector3.one * .25f, .5f, 1);
            objTransform.localRotation = Quaternion.identity;
            _modelTransform.localPosition = new Vector3(_modelTransform.localPosition.x, _stackController.Stack, _modelTransform.localPosition.z);
            SetPosition(obj);
        }

        private void UpdateVisualLost()
        {
            var obj = _stackedObjects.Pop();
            StartCoroutine(LostEnumerator());
            IEnumerator LostEnumerator()
            {
                obj.SetLost();
                yield return new WaitForSeconds(.25f);
                _modelTransform.localPosition = new Vector3(_modelTransform.localPosition.x, _modelTransform.localPosition.y - 1, _modelTransform.localPosition.z);
            }
            
        }

        private void SetPosition(RainbowCube obj)
        {
            obj.transform.position = new Vector3(transform.position.x, 0 , transform.position.z);
        }
    }
}
