using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BaseTask : BaseCmd
{
    public List<BaseCmd> subCmds = new List<BaseCmd>();

    public override void Execute()
    {
        Debug.Log($"{GetType().Name} completed");
        CmdCompleted?.Invoke();
    }
}
