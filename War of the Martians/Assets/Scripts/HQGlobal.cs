using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQGlobal : MonoBehaviour
{
    // Singleton
    public static HQGlobal instance;

    [SerializeField] public List<GameObject> allHQs = new List<GameObject>();

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    // Returns the nearest depositing point to the worker
    public GameObject GetNearestHQ(Transform worker) {
        GameObject nearestHQ = allHQs[0];
        float nearestDistance = 0f;
        foreach (GameObject hq in allHQs) {
            float distanceToTarget = Vector2.Distance(hq.transform.position, worker.transform.position);
            if (distanceToTarget < nearestDistance) {
                nearestHQ = hq;
                nearestDistance = distanceToTarget;
            }
        }
        return nearestHQ;
    }

    public GameObject GetRandomHQ() {
        if (allHQs.Count == 0) {
            return null;
        } else if (allHQs.Count == 1) {
            return allHQs[0];
        }
        return allHQs[Random.Range(0, allHQs.Count + 1)];
    }
}
