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
    //[SerializeField] private GameObject robotPrefab;
    [SerializeField] private GameObject AIsFolder;

    private string desc = "Welcome to the 'War of the Martians'. This is a prototype building featuring enemy variation, basic unit spawning, and resource collection.\n\nHold left mouse button and drag to select units, or directly click on units to select. Holding shift allows you to select more units without deselecting current units.\n\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string l1 = "Spawning units cost resource points.\n";
        string l2 = "Material: " + PlayerResources.instance.GetMaterialPoints() + "pts\n";
        string l3 = "Oxygen: " + PlayerResources.instance.GetOxygenPoints() + "pts\n\n";
        string l4 = "Push the respective keys to spawn units\n";
        string l5 = "Worker - 5 Mats - Q\n";
        string l6 = "Human - 10 Mats - R\n";
        //string l7 = "Robot - 10 Mats - T";
        string combined = l1+l2+l3+l4+l5+l6;//+l7;
        gameObject.GetComponent<TextMeshProUGUI>().text = desc + combined;

        if (CheckConditions(KeyCode.Q,5,0)) {
            PlayerResources.instance.SubtractPoints(ResourceType.Material, 5);
            // Spawn worker
            SpawnPrefab(workerPrefab, new Vector2(-20f,6f));
        } else if (CheckConditions(KeyCode.R,10,0)) {
            PlayerResources.instance.SubtractPoints(ResourceType.Material, 5);
            // Spawn human
            SpawnPrefab(humanPrefab, new Vector2(-15.5,5f));
        //} else if (CheckConditions(KeyCode.T,10,0)) {
        //    PlayerResources.instance.SubtractPoints(ResourceType.Material, 10);
        //    // Spawn Robot
        //    SpawnPrefab(robotPrefab, new Vector2(-11f,5.5f));
        }   
    }

    private bool CheckConditions(KeyCode key, int mat, int oxy) {
        if (Input.GetKeyDown(key) && PlayerResources.instance.GetMaterialPoints() >= mat && PlayerResources.instance.GetOxygenPoints() >= oxy)
            return true;
        return false;
    }

    private void SpawnPrefab(GameObject prefab, Vector3 pos) {
        GameObject a = Instantiate(prefab, pos, Quaternion.identity);
        a.transform.eulerAngles = new Vector3(0f,0f,-33.33f);
        a.transform.parent = AIsFolder.transform;
    }   
}
