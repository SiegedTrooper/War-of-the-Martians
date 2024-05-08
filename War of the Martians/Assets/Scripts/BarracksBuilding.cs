using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameResources;

public class BarracksBuilding : MonoBehaviour
{
    
    private void Start() {
        PlayerResources.instance.SubtractPoints(ResourceType.Material, BuildingInputHandler.instance.Barracks_MatCost);
        PlayerResources.instance.SubtractPoints(ResourceType.Oxygen, BuildingInputHandler.instance.Barracks_OxyCost);

        BarracksGlobal.instance.allBarracks.Add(gameObject);
    }

    private void Destory() {
        PlayerResources.instance.AddResource(BuildingInputHandler.instance.Barracks_OxyCost, ResourceType.Oxygen);
        BarracksGlobal.instance.allBarracks.Remove(gameObject);
    }
}
