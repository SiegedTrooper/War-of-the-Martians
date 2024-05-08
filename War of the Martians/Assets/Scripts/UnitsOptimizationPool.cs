using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameResources;

public class UnitsOptimizationPool : MonoBehaviour
{
    public static UnitsOptimizationPool instance;

    [Header("Unit Creation Behavior")]
    [SerializeField] public int NumOfNewUnitsEachBatch = 5;
    public Transform poolFolder;

    private void Awake() {
        if (instance != null && instance != this) {
            Debug.Log("Another UnitsOptimizationPool exists! Deleting this second one...");
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    private void CreateUnit(GameObject prefab, List<GameObject> list) {
        for (int i = 0; i < NumOfNewUnitsEachBatch; i++) {
            GameObject copy = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            copy.SetActive(false);
            list.Add(copy);
            copy.transform.parent = poolFolder;
        }
    }

    [Header("Melee Units")]
    public GameObject playerMeleePrefab;
    public GameObject enemyMeleePrefab;

    private List<GameObject> playerMelee = new List<GameObject>();
    private List<GameObject> enemyMelee = new List<GameObject>();
    public GameObject GetMeleeUnit(Faction faction) {
        GameObject unit;
        if (faction == Faction.Player) {
            if (playerMelee.Count == 0) {
                CreateUnit(playerMeleePrefab, playerMelee);
            }
            unit = playerMelee[0];
            playerMelee.RemoveAt(0);
        } else {
            if (enemyMelee.Count == 0) {
                CreateUnit(enemyMeleePrefab, enemyMelee);
            }
            unit = enemyMelee[0];
            enemyMelee.RemoveAt(0);
        }
        unit.transform.SetAsLastSibling();
        unit.SetActive(true);
        return unit;
    }

    public void AddMeleeUnit(Faction faction, GameObject unit) {
        unit.SetActive(false);
        unit.GetComponent<UnitController>().ResetUnit();

        if (faction == Faction.Player) {
            playerMelee.Add(unit);
        } else {
            enemyMelee.Add(unit);
        }
    }

    [Header("Melee Units")]
    public GameObject playerWorkerPrefab;
    public GameObject enemyWorkerPrefab;
    
    private List<GameObject> playerWorker = new List<GameObject>();
    private List<GameObject> enemyWorker = new List<GameObject>();
    public GameObject GetWorkerUnit(Faction faction) {
        GameObject unit;
        if (faction == Faction.Player) {
            if (playerWorker.Count == 0) {
                CreateUnit(playerWorkerPrefab, playerWorker);
            }
            unit = playerWorker[0];
            playerWorker.RemoveAt(0);
        } else {
            if (enemyWorker.Count == 0) {
                CreateUnit(enemyWorkerPrefab, enemyWorker);
            }
            unit = enemyWorker[0];
            enemyWorker.RemoveAt(0);
        }
        unit.transform.SetAsLastSibling();
        unit.SetActive(true);
        return unit;
    }

    public void AddWorkerUnit(Faction faction, GameObject unit) {
        unit.SetActive(false);
        unit.GetComponent<UnitController>().ResetUnit();

        if (faction == Faction.Player) {
            playerWorker.Add(unit);
        } else {
            enemyWorker.Add(unit);
        }
    }
}
