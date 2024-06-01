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
                    GridCell gridCell = GridManager.GetCell(hit.point, true);

                    if (!Input.GetKey(KeyCode.LeftShift)) selectedUnit.ordersBehaviour.RemoveAll();

                    if (gridCell.isOcupated == false)
                    {
                        selectedUnit.ordersBehaviour.AddOrder(new MoveCmd(selectedUnit, hit.point));
                    }
                    else
                    {
                        if (gridCell.objectOnCell.TryGetComponent(out HarvestComponent harvestComponent))
                        {
                            selectedUnit.ordersBehaviour.AddOrder(new HarvestTask(selectedUnit, harvestComponent));
                        }
                    }
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

            Inventory inventoryUI = GUIManager.Instance.inventory;
            inventoryUI.Refresh();
            selectedUnit.inventoryComponent.itemsStorage.StorageChanged.AddListener(inventoryUI.Refresh);
        }
    }

    private void UnselectUnits()
    {
        if (selectedUnit != null && selectedUnit.gameObject.activeSelf)
        {
            selectedUnit.outline.enabled = false;

            Inventory inventoryUI = GUIManager.Instance.inventory;
            selectedUnit.inventoryComponent.itemsStorage.StorageChanged.RemoveListener(inventoryUI.Refresh);
        }
        selectedUnit = null;
    }
}
