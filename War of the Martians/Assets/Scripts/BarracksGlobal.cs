using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarracksGlobal : MonoBehaviour
{
    public static BarracksGlobal instance;

    [SerializeField] public List<GameObject> allBarracks = new List<GameObject>();

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    public GameObject GetRandomBarracks() {
        if (allBarracks.Count == 0) {
            return null;
        } else if (allBarracks.Count == 1) {
            return allBarracks[0];
        }
        return allBarracks[Random.Range(0, allBarracks.Count + 1)];
    }
}
