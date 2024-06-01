using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseTask : BaseCmd
{
    public List<BaseCmd> subCmd = new List<BaseCmd>();

    public override void Execute()
    {
        CmdCompleted?.Invoke();
    }
}
