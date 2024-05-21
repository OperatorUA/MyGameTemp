using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemData itemData;

    public Image image;
    public TextMeshProUGUI textComponent;

    [HideInInspector] public Transform parentAfterDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector3.zero;

        image.raycastTarget = true;
    }

    public void UpdateData(ItemData itemData)
    {
        this.itemData = itemData;
        image.sprite = itemData.icon;
        textComponent.text = itemData.count.ToString();
    }
}
