using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script enables the unit to be selected

public class Unit : MonoBehaviour
{
    
    private void Start() {
        UnitSelectionManager.UnitSelector.allUnits.Add(gameObject);
    }

    void OnDestroy() {
        UnitSelectionManager.UnitSelector.allUnits.Remove(gameObject);
    }
}
