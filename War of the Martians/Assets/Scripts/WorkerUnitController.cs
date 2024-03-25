using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameResources;

public class WorkerUnitController : MonoBehaviour
{
    [SerializeField] public int collectionAmount = 5;
    [SerializeField] public float collectionTime = 2f;
    [SerializeField] public float collectionDistance = 1f;

    [SerializeField] private bool hasResourceInHand = false;
    [SerializeField] private ResourceType resourceType;

    [SerializeField] private GameObject HQTarget;
    [SerializeField] private Transform resourceTarget;
    private UnitController unitController;

    private void Start() {
        unitController = gameObject.GetComponent<UnitController>();
    }

    public void Collect(Transform resource) {
        resourceTarget = resource;
    }

    public Transform GetTarget() {
        return resourceTarget;
    }

    /*public void SetResourceInHand(bool b) {
        hasResourceInHand = b;
    }*/
    public void CollectResource() {
        hasResourceInHand = true;
        resourceType = resourceTarget.gameObject.GetComponent<ResourceController>().resourceType;
        resourceTarget.gameObject.GetComponent<ResourceController>().RemoveAmount(collectionAmount);
    }

    public bool HasResourceInHand() {
        return hasResourceInHand;
    }

    public void RemoveResourceInHand() {
        hasResourceInHand = false;
    }

    public void SetHQ(GameObject hq) {
        HQTarget = hq;
    }

    public GameObject GetHQ() {
        return HQTarget;
    }

    public ResourceType GetResourceType () {
        return resourceType;
    }
}
