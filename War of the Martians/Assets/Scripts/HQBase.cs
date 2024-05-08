using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameResources;

public class HQBase : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start() {
        PlayerResources.instance.SubtractPoints(ResourceType.Material, BuildingInputHandler.instance.HQ_MatCost);
        PlayerResources.instance.SubtractPoints(ResourceType.Oxygen, BuildingInputHandler.instance.HQ_OxyCost);
        HQGlobal.instance.allHQs.Add(gameObject);
    }

    private void OnDestroy() {
        HQGlobal.instance.allHQs.Remove(gameObject);
        PlayerResources.instance.AddResource(BuildingInputHandler.instance.HQ_OxyCost, ResourceType.Oxygen);
    }
}
