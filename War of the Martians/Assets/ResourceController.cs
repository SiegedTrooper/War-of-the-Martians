using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameResources;

public class ResourceController : MonoBehaviour
{
    [SerializeField] public int resourceQuantity = 100;
    [SerializeField] public ResourceTypes resourceType;

    public void RemoveAmount(int amount) {
        resourceQuantity -= amount;
        if (resourceQuantity <= 0) {
            Destroy(gameObject);
            // TODO: remove resource - probably will just delete
            //throw new System.NotImplementedException();
        }
    }

}