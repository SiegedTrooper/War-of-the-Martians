using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameResources;

public class SupplyDepotBuilding : MonoBehaviour
{
    public int OxygenProduction = 20;

    // Start is called before the first frame update
    private void Start() {
        PlayerResources.instance.SubtractPoints(ResourceType.Material, BuildingInputHandler.instance.Supply_MatCost);
        
        PlayerResources.instance.AddResource(OxygenProduction, ResourceType.Oxygen);
    }

    // Called by Unity when object is being destroyed
    private void OnDestory() {
        PlayerResources.instance.SubtractPoints(ResourceType.Oxygen, OxygenProduction);
    }
}
