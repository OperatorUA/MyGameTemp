using UnityEngine;

public class TestOrders : MonoBehaviour
{
    public BaseUnit selectedUnit;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << 3))
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector3Int cellCenterPosition = GridNavigation.PositionToCoords(hit.point);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    BaseCmd pickUpTask = new PickUpTask(selectedUnit, cellCenterPosition);
                    selectedUnit.ordersBehaviour.AddOrder(pickUpTask);
                } else
                {
                    BaseCmd moveCmd = new MoveCmd(selectedUnit, cellCenterPosition);
                    selectedUnit.ordersBehaviour.AddOrder(moveCmd);
                }
            }


        }
    }
}
