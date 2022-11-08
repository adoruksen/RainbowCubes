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
        private Stacker _stacker;
        [SerializeField] private Transform _modelTransform;
        [SerializeField] private Transform _interactorTransform;

        [ShowInInspector] private readonly Stack<RainbowCube> _stackedObjects = new();


        private void Awake()
        {
            _stacker = GetComponentInParent<Stacker>();
            _stackController = GetComponent<StackController>();
        }

        private void OnEnable()
        {
            _stackController.OnStackAdded += UpdateVisualAdded;
            _stackController.OnStackUsed += UpdateVisualUsed;
            _stackController.OnStackLost += UpdateVisualLost;
        }

        private void OnDisable()
        {
            _stackController.OnStackAdded -= UpdateVisualAdded;
            _stackController.OnStackUsed -= UpdateVisualUsed;
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

        private void UpdateVisualUsed()
        {
            var obj = _stackedObjects.Pop();
            StartCoroutine(LostEnumerator(obj,_interactorTransform,true));
        }

        private void UpdateVisualLost()
        {
            var obj = _stackedObjects.Pop();
            StartCoroutine(LostEnumerator(obj,_modelTransform,false));
            if (_stackController.Stack <= 0) return;
            _stacker.SetTrailColorOnLost(_stackedObjects.Peek().transform.GetChild(0).GetComponent<Renderer>().material.color);
        }

        private void SetPosition(RainbowCube obj)
        {
            obj.transform.position = new Vector3(transform.position.x, 0 , transform.position.z);
        }

        IEnumerator LostEnumerator(RainbowCube obj,Transform transform,bool isAddition)
        {
            obj.SetLost();
            yield return new WaitForSeconds(.25f);
            if(isAddition)
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1, transform.localPosition.z);
            else
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 1, transform.localPosition.z);
        }
    }
}
