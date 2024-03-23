using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tracks all HQs in the game (TEMP)
public class HQBase : MonoBehaviour
{
    void Start() {
        HQGlobal.instance.allHQs.Add(gameObject);
    }

    void OnDestroy() {
        HQGlobal.instance.allHQs.Remove(gameObject);
    }
}
