using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public PlayerControls playerControls;
    public TextMeshProUGUI logText;
    public InventoryGroupUI inventoryGroup;

    public Inventory inventory;

    public static GUIManager _instance { get; private set; }
    public static GUIManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<GUIManager>();
            return _instance;
        }
    }

    private void Awake()
    {
        inventoryGroup.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            ToggleInventory(null);
        }
    }

    private void ToggleInventory(BaseUnit selectedUnit)
    {
        _instance.inventoryGroup.gameObject.SetActive(!_instance.inventoryGroup.gameObject.activeSelf);

        if (selectedUnit != null)
        {
            InventoryComponent inventoryComponent = selectedUnit.GetComponent<InventoryComponent>();
            //inventoryPanel.LoadInfo(inventoryComponent);
        }
    }
    public void AddLog(string message)
    {
        if (_instance != null && _instance.logText != null)
            logText.text += message;
    }

    public void ClearLog()
    {
        if (_instance != null && _instance.logText != null)
            logText.text = string.Empty;
    }

    public static void Log(string message)
    {
        if (_instance != null && _instance.logText != null)
            _instance.logText.text = message;
    }
}