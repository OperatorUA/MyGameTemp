using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Entity/Resources/Plants/Default Plant")]
public class PlantData : ResourceData
{
    public HealthConfig healthConfig;
    public GrowConfig growConfig;
}
