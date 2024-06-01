using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbbortButton : MonoBehaviour
{
    public BaseUnit selectedUnit;
    public void OnClick()
    {
        if ( selectedUnit != null )
        {
            selectedUnit.ordersBehaviour.orders.First().CmdAborted?.Invoke();
        }
    }
}
