using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    private Camera _camera;

    public BaseUnit selectedUnit;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            WorldPositionRaycast();
        }
        
    }
    private void WorldPositionRaycast()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        RaycastDefault();
        RaycastTerrain();

        void RaycastDefault()
        {
            if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << 0))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    BaseUnit unit = hit.collider.GetComponent<BaseUnit>();
                    if (unit != null)
                    {
                        SelectUnit(unit);
                    }
                    else
                    {
                        UnselectUnits();
                    }
                }
            }
        }

        void RaycastTerrain()
        {
            if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << 3))
            {
                if (Input.GetKeyDown(KeyCode.Mouse1) && selectedUnit != null)
                {
                    List<BaseOrder> orders = new List<BaseOrder>();
                    GridCell gridCell = GridManager.GetCell(hit.point, true);

                    if (gridCell.isOcupated == false)
                    {
                        orders.Add(new OrderMoveTo(hit.point, selectedUnit));
                    }
                    else
                    {
                        if (gridCell.objectOnCell.TryGetComponent(out HarvestComponent harvestComponent))
                        {
                            orders.Add(new OrderHarvest(harvestComponent, selectedUnit));
                        }
                    }

                    if (!Input.GetKey(KeyCode.LeftShift)) selectedUnit.ordersBehaviour.ClearOrders();

                    if (orders.Count != 0) selectedUnit.ordersBehaviour.AddOrders(orders);
                }

                GridManager.HoverCell(hit.point);
            }
        }
    }

    private void SelectUnit(BaseUnit unit)
    {
        if (unit != selectedUnit)
        {
            unit.outline.enabled = true;
            UnselectUnits();
            selectedUnit = unit;

            InventoryUI inventoryUI = GUIManager.Instance.inventoryUI;
            inventoryUI.Refresh();
            selectedUnit.inventoryComponent.onInventoryChanged.AddListener(inventoryUI.Refresh);
        }
    }

    private void UnselectUnits()
    {
        if (selectedUnit != null && selectedUnit.gameObject.activeSelf)
        {
            selectedUnit.outline.enabled = false;

            InventoryUI inventoryUI = GUIManager.Instance.inventoryUI;
            selectedUnit.inventoryComponent.onInventoryChanged.RemoveListener(inventoryUI.Refresh);
        }
        selectedUnit = null;
    }
}
