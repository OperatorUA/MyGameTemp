using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrdersBehaviour : MonoBehaviour
{
    public List<BaseCmd> orders = new List<BaseCmd>();
    public List<string> orders_debug = new List<string>();
    BaseCmd currentOrder = null;

    private void Update()
    {
        if (orders.Count == 0) return;

        if (currentOrder != null)
        {
            currentOrder.Execute();
        }
        else
        {
            StartNextCmd();
        }
    }

    private void StartNextCmd()
    {
        currentOrder = orders.First();

        currentOrder.CmdCompleted.AddListener(OnCmdCompleted);
        currentOrder.CmdAborted.AddListener(OnCmdAborted);
    }

    private void OnCmdCompleted()
    {
        RemoveCurrentOrder();
    }

    private void OnCmdAborted()
    {
        BaseTask parentTask = currentOrder.parentTask;
        RemoveCurrentOrder();

        if (parentTask != null)
        {
            RemoveTask(parentTask);
        }
    }

    public void AddOrder(BaseCmd order)
    {
        if (order is BaseTask task)
        {
            foreach (BaseCmd cmd in task.subCmds)
            {
                AddOrder(cmd);
            }
        }

        orders.Add(order);
        orders_debug.Add(order.ToString());
    }

    private void RemoveTask(BaseTask task)
    {
        foreach (BaseCmd subCmd in task.subCmds)
        {
            if (subCmd is BaseTask subTask)
            {
                RemoveTask(subTask);
            }

            orders.Remove(subCmd);
            orders_debug.Remove(subCmd.ToString());
        }

        orders.Remove(task);
        orders_debug.Remove(task.ToString());
    }

    private void RemoveCurrentOrder()
    {
        orders.Remove(currentOrder);
        orders_debug.Remove(currentOrder.ToString());

        currentOrder.CmdCompleted.RemoveListener(OnCmdCompleted);
        currentOrder.CmdAborted.RemoveListener(OnCmdAborted);

        currentOrder = null;
    }

    public void RemoveAll()
    {
        if (currentOrder != null) RemoveCurrentOrder();
        orders.Clear();
    }
}
