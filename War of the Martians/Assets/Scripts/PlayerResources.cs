using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameResources;

public class PlayerResources : MonoBehaviour
{
    // Singleton
    public static PlayerResources instance;

    // These have their own getters/setters
    private int materialPoints;
    private int oxygenPoints;
    
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }



    // Getters and Setters
    public void AddResource(int points, ResourceType resource) {
        if (resource == ResourceType.Material) {
            materialPoints += points;
            CommandUIController.instance.UpdateCount(materialPoints, resource);
            //Debug.Log("Current Material: " + materialPoints);
        } else if (resource == ResourceType.Oxygen) {
            oxygenPoints += points;
            CommandUIController.instance.UpdateCount(oxygenPoints, resource);
            //Debug.Log("Current Oxygen: " + oxygenPoints);
        }
    }
    public int GetMaterialPoints() {
        return materialPoints;
    }
    public int GetOxygenPoints() {
        return oxygenPoints;
    }
    public void SubtractPoints(ResourceType t, int p) {
        if (t == ResourceType.Material) {
            materialPoints -= p;
        } else if (t == ResourceType.Oxygen) {
            oxygenPoints -= p;
        }
    }
}