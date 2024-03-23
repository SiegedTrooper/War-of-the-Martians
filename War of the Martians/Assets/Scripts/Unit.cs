using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    
    private void Start() {
        UnitSelectionManager.UnitSelector.allUnits.Add(gameObject);
    }

    void OnDestroy() {
        UnitSelectionManager.UnitSelector.allUnits.Remove(gameObject);
    }
}
