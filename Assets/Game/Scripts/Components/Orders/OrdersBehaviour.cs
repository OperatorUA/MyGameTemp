using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrdersBehaviour : MonoBehaviour
{
    private List<BaseOrder> _ordersList = new List<BaseOrder>();
    private BaseOrder _currentOrder = null;

    [SerializeField] private List<string> _ordersList_debug = new List<string>();
    private void Update()
    {
        if (_currentOrder != null)
        {
            _currentOrder.Execute();
        } else
        {
            if (_ordersList.Count != 0)
            {
                UpdateCurrentOrder();
            }
        }
    }

    private void UpdateCurrentOrder()
    {
        _currentOrder = _ordersList.First();
        _currentOrder.OnOrderCompleted.AddListener(HandleOrderCompleted);
    }

    private void HandleOrderCompleted()
    {
        //Debug.Log("Order Completed");
        _currentOrder.OnOrderCompleted.RemoveListener(HandleOrderCompleted);
        _ordersList.Remove(_currentOrder);
        _ordersList_debug.Remove(_currentOrder.ToString());
        _currentOrder = null;
    }

    public void ClearOrders()
    {
        _currentOrder?.OnOrderCompleted.Invoke();
        _ordersList.Clear();
        _ordersList_debug.Clear();
    }

    public void AbortCurrentOrder()
    {
        if (_currentOrder != null)
        {
            _currentOrder.OnOrderCompleted.Invoke();
            _ordersList.Remove(_currentOrder);
            _ordersList_debug.Remove(_currentOrder.ToString());
        }
    }

    public void AddOrder(BaseOrder order)
    {
        _ordersList.Add(order);
        _ordersList_debug.Add(order.ToString());
    }

    public void AddOrders(List<BaseOrder> orders)
    {
        foreach (BaseOrder order in orders)
        {
            AddOrder(order);
        }
    }
}
