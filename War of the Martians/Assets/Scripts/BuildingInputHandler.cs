using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingInputHandler : MonoBehaviour
{
    public static BuildingInputHandler instance;

    [Header("HQ Config")]
    public TextMeshProUGUI HQ;
    public GameObject HQ_Prefab;
    [SerializeField] public int HQ_MatCost = 1;
    [SerializeField] public int HQ_OxyCost = 0;
    [SerializeField] public KeyCode HQ_Key = KeyCode.Z;

    [Header("Barrcks Config")]
    public TextMeshProUGUI Barracks;
    public GameObject Barracks_Prefab;
    [SerializeField] public int Barracks_MatCost = 1;
    [SerializeField] public int Barracks_OxyCost = 0;
    [SerializeField] public KeyCode Barracks_Key = KeyCode.X;

    [Header("Supply Depot Config")]
    public TextMeshProUGUI Supply;
    public GameObject Supply_Prefab;
    [SerializeField] public int Supply_MatCost = 1;
    [SerializeField] public int Supply_OxyCost = 0;
    [SerializeField] public KeyCode Supply_Key = KeyCode.C;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    private void Start() {
        string HQ_Cost = "\n";
        if (HQ_MatCost > 0) {
            HQ_Cost += "Mats: " + HQ_MatCost;
        }
        if (HQ_MatCost > 0 & HQ_OxyCost > 0) {
            HQ_Cost += " & ";
        }
        if (HQ_OxyCost > 0) {
            HQ_Cost += "Oxys: " + HQ_OxyCost;
        }

        string Barracks_Cost = "\n";
        if (Barracks_MatCost > 0) {
            Barracks_Cost += "Mats: " + Barracks_MatCost;
        }
        if (Barracks_MatCost > 0 & Barracks_OxyCost > 0) {
            Barracks_Cost += " & ";
        }
        if (Barracks_OxyCost > 0) {
            Barracks_Cost += "Oxys: " + Barracks_OxyCost;
        }

        string Supply_Cost = "\n";
        if (Supply_MatCost > 0) {
            Supply_Cost += "Mats: " + Supply_MatCost;
        }
        if (Supply_MatCost > 0 & Supply_OxyCost > 0) {
            Supply_Cost += " & ";
        }
        if (Supply_OxyCost > 0) {
            Supply_Cost += "Oxys: " + Supply_OxyCost;
        }

        HQ.text += " <u>[" + HQ_Key + "]</u>" + HQ_Cost;
        Barracks.text += " <u>[" + Barracks_Key + "]</u>" + Barracks_Cost;
        Supply.text += " <u>[" + Supply_Key + "]</u>" + Supply_Cost;
    }

    private void Update() {
        if (Input.GetKeyDown(HQ_Key)) {
            if (MeetCostChecks(HQ_MatCost,HQ_OxyCost)) {
                GridBuildingSystem.instance.InitializeWithBuilding(HQ_Prefab);
            }
        } else if (Input.GetKeyDown(Barracks_Key)) {
            if (MeetCostChecks(Barracks_MatCost,Barracks_OxyCost)) {
                GridBuildingSystem.instance.InitializeWithBuilding(Barracks_Prefab);
            }
        } else if (Input.GetKeyDown(Supply_Key)) {
            if (MeetCostChecks(Supply_MatCost,Supply_OxyCost)) {
                GridBuildingSystem.instance.InitializeWithBuilding(Supply_Prefab);
            }
        }
    }

    private bool MeetCostChecks(int required_material, int required_oxygen) {
        bool pass = true;
        if (PlayerResources.instance.GetMaterialPoints() < required_material) {
            CommandUIController.instance.FlashMaterialCounter();
            ChatNotifications.instance.Notify("Not enough Materials","red");
            pass = false;
        }

        if (PlayerResources.instance.GetOxygenPoints() < required_oxygen) {
            CommandUIController.instance.FlashOxygenCounter();
            ChatNotifications.instance.Notify("Not enough Oxygen","red");
            pass = false;
        }

        return pass;
    }
}
