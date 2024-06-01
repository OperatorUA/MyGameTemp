using UnityEngine;

public class MoveCmd : BaseCmd
{
    private Transform _unitTransform;
    private float _moveSpeed;
    private Collider _unitCollider;

    private Vector3 _targetPosition;
    public MoveCmd(BaseUnit unitComponent, Vector3 targetPosition)
    {
        _unitTransform = unitComponent.transform;
        _moveSpeed = unitComponent.unitData.moveSpeed;
        _unitCollider = unitComponent.GetComponent<Collider>();
        _targetPosition = GridNavigation.GetCellCenterPosition(targetPosition);
    }
    public override void Execute()
    {
        Vector3 bottomPosition = _unitTransform.position - Vector3.up * _unitCollider.bounds.extents.y;

        Vector3 direction = _targetPosition - bottomPosition;
        Vector3 moveVector = direction.normalized * _moveSpeed * Time.deltaTime;
        _unitTransform.position += moveVector;

        if (Vector3.Distance(bottomPosition, _targetPosition) < _moveSpeed * Time.deltaTime)
        {
            CmdCompleted.Invoke();
        }
    }
}
