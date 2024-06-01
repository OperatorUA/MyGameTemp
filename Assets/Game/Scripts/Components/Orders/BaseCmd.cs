using UnityEngine.Events;

public abstract class BaseCmd
{
    public BaseTask parentTask = null;

    public UnityEvent CmdCompleted = new UnityEvent();
    public UnityEvent CmdAborted = new UnityEvent();
    public abstract void Execute();
}
