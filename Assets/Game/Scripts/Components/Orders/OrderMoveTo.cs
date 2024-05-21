using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderMoveTo : BaseOrder
{
    private Vector3 _targetPosition;
    private Transform _unitTransform;
    private float _moveSpeed;
    private Collider _unitCollider;

    public OrderMoveTo(Vector3 targetPosition, BaseUnit unitComponent)
    {
        _targetPosition = GridNavigation.GetCellCenterPosition(targetPosition);
        _unitTransform = unitComponent.transform;
        _moveSpeed = unitComponent.unitData.moveSpeed;
        _unitCollider = _unitTransform.GetComponent<Collider>();
    }

    public override void Execute()
    {
        Vector3 bottomPosition = _unitTransform.position - Vector3.up * _unitCollider.bounds.extents.y;

        Vector3 direction = _targetPosition - bottomPosition;
        Vector3 moveVector = direction.normalized * _moveSpeed * Time.deltaTime;
        _unitTransform.position += moveVector;

        if (Vector3.Distance(bottomPosition, _targetPosition) < _moveSpeed * Time.deltaTime)
        {
            OnOrderCompleted.Invoke();
        }
    }
}
