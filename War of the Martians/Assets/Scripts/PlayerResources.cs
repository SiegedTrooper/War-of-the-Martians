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
        if (resource == ResourceTypes.Material) {
            materialPoints += points;
        } else if (resource == ResourceTypes.Oxygen) {
            oxygenPoints += points;
        }
    }
    public int GetMaterialPoints() {
        return materialPoints;
    }
    public int GetOxygenPoints() {
        return oxygenPoints;
    }
}