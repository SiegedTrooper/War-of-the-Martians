using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameResources;

public class UIControl : MonoBehaviour
{
    [SerializeField] private GameObject workerPrefab;
    [SerializeField] private GameObject humanPrefab;
    [SerializeField] private GameObject objective;
    //[SerializeField] private GameObject robotPrefab;
    [SerializeField] private GameObject AIsFolder;

    //private string desc = "Welcome to the 'War of the Martians'.\n\nHold left mouse button and drag to select units, or directly click on units to select. Holding shift allows you to select more units without deselecting current units.\n\nPress any key correlating to create a building. Left click to place, right click to cancel.\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\n";

    // Update is called once per frame
    void Update()
    {
        
            //string l1 = "Spawning units cost resource points.\n";
            //string l4 = "Push the respective keys to spawn units\n";
            //string l5 = "Worker - 10 Mats - Q\n";
            //string l6 = "Human - 5 Mats - E\n";
            //string l7 = "Robot - 10 Mats - T";
            //string combined = l1+l4+l5+l6;//+l7;
            //gameObject.GetComponent<TextMeshProUGUI>().text = desc + combined;

            if (CheckConditions(KeyCode.Q,10,0)) {
                if (HQGlobal.instance.allHQs.Count == 0) {
                    ChatNotifications.instance.Notify("You need to construct a HQ.","red");
                    return;
                }
                PlayerResources.instance.SubtractPoints(ResourceType.Material, 10);
                // Spawn worker // Optimization: Pooling
                //SpawnPrefab(workerPrefab, new Vector2(-20f,6f));
                GameObject unit = UnitsOptimizationPool.instance.GetWorkerUnit(Faction.Player);
                unit.transform.parent = AIsFolder.transform;
                // Spawn at HQ
                unit.transform.position = HQGlobal.instance.GetRandomHQ().transform.position;
            } else if (CheckConditions(KeyCode.E,5,0)) {
                if (BarracksGlobal.instance.allBarracks.Count == 0) {
                    ChatNotifications.instance.Notify("You need to construct a Barracks.","red");
                    return;
                }
                PlayerResources.instance.SubtractPoints(ResourceType.Material, 5);
                // Spawn human // Optimization: Pooling
                //SpawnPrefab(humanPrefab, new Vector2(-15.5f,5f));
                GameObject unit = UnitsOptimizationPool.instance.GetMeleeUnit(Faction.Player);
                unit.transform.parent = AIsFolder.transform;
                // Spawn at Barracks
                unit.transform.position = BarracksGlobal.instance.GetRandomBarracks().transform.position;
            } 
    }

    private bool CheckConditions(KeyCode key, int mat, int oxy) {
        bool pass = false;
        if (Input.GetKeyDown(key)) {
            pass = true; // Key is now down
            if (PlayerResources.instance.GetMaterialPoints() < mat) {
                pass = false;
                ChatNotifications.instance.Notify("Not enough Materials","red");
            }
            if (PlayerResources.instance.GetOxygenPoints() < oxy) {
                pass = false;
                ChatNotifications.instance.Notify("Not enough Oxygen","red");
            }
        }
        
        return pass;
    } 
}
