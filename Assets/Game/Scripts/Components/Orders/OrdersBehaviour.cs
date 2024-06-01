using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrdersBehaviour : MonoBehaviour
{
    public List<BaseCmd> cmdList = new List<BaseCmd>();
    public List<string> cmdList_debug = new List<string>();
    BaseCmd currentCmd = null;
    private void Update()
    {
        if (cmdList.Count == 0) return;

        if (currentCmd != null)
        {
            currentCmd.Execute();
        }
        else
        {
            currentCmd = cmdList.First();

            currentCmd.CmdCompleted.AddListener(OnTaskCompeted);
            currentCmd.CmdAborted.AddListener(OnTaskAborted);
        }
    }

    public void AddOrder(BaseCmd order)
    {
        if (order is BaseTask task)
        {
            foreach (BaseCmd subCmd in task.subCmd)
            {
                if (subCmd is BaseTask subTask)
                {
                    AddOrder(subTask);
                }
                else
                {
                    cmdList.Add(subCmd);
                    cmdList_debug.Add(subCmd.ToString());
                }
            }
        } else
        {
            cmdList.Add(order);
            cmdList_debug.Add(order.ToString());
        }
    }

    public void RemoveAll()
    {
        if (currentCmd != null) RemoveCurrentCmd();
        cmdList.Clear();
    }

    private void OnTaskCompeted()
    {
        RemoveCurrentCmd();
    }

    private void OnTaskAborted()
    {
        if (currentCmd is BaseTask)
        {
            foreach (BaseCmd subTask in currentCmd.parentTask.subCmd)
            {
                if (cmdList.Contains(subTask))
                {
                    cmdList.Remove(subTask);
                    cmdList_debug.Remove(subTask.ToString());
                }
            }
        }

        RemoveCurrentCmd();
    }

    private void RemoveCurrentCmd()
    {
        currentCmd.CmdCompleted.RemoveListener(OnTaskCompeted);
        currentCmd.CmdAborted.RemoveListener(OnTaskAborted);

        cmdList.Remove(currentCmd);
        cmdList_debug.Remove(currentCmd.ToString());

        currentCmd = null;
    }
}
