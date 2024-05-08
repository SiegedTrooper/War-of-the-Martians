using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameResources;

public class PlayerResources : MonoBehaviour
{
    // Singleton
    public static PlayerResources instance;

    // These have their own getters/setters
    [SerializeField] public int StartingMaterialPoints = 0;
    private int materialPoints = 0;
    private int oxygenPoints = 0;
    
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    private void Start() {
        IEnumerator Delay() { // Buildings will pull resources throwing the counter to negative. This is to offset that.
            yield return new WaitForSeconds(1f);
            materialPoints = 0;
            AddResource(StartingMaterialPoints, ResourceType.Material);
        }
        StartCoroutine(Delay());
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
            CommandUIController.instance.UpdateCount(materialPoints, t);
        } else if (t == ResourceType.Oxygen) {
            oxygenPoints -= p;
            CommandUIController.instance.UpdateCount(oxygenPoints, t);
        }
    }
}