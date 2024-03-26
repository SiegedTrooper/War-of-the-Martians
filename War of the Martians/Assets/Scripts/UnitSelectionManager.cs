using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script issue commands to units
public class UnitSelectionManager : MonoBehaviour
{
    // Singleton
    public static UnitSelectionManager UnitSelector;

    [Header("Lists")]
    [SerializeField] public List<GameObject> allUnits = new List<GameObject>();
    [SerializeField] public List<GameObject> selectedUnits = new List<GameObject>();

    [Header("Layers")]
    [SerializeField] public LayerMask unitLayer;
    [SerializeField] public LayerMask attackableLayer;
    [SerializeField] public LayerMask resourceLayer;

    public enum cursorTypes {Default, Attack, Collect};
    public cursorTypes cursor; // TODO: Give functionality

    private void Awake() {
        if (UnitSelector != null && UnitSelector != this) {
            Destroy(gameObject);
        } else {
            UnitSelector = this;
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseInWorld.z = 0;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(mouseInWorld,(mouseInWorld-Camera.main.transform.position).normalized,100,unitLayer);
            if (raycastHit2D) {
                //Debug.Log(raycastHit2D.collider.gameObject.name + " was hit and is unit");
                if (Input.GetKey(KeyCode.LeftShift)) {
                    MultiSelect(raycastHit2D.collider.gameObject);
                } else {
                    SelectByClicking(raycastHit2D.collider.gameObject);
                }
            } else {
                DeselectAll();
            }
        }

        // Attack Target
        if (selectedUnits.Count > 0 && OffensiveUnitPresent(selectedUnits)) {
            Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseInWorld.z = 0;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(mouseInWorld,(mouseInWorld-Camera.main.transform.position).normalized,100,attackableLayer);
            if (raycastHit2D) { // Purpose: cursor image
                //Debug.Log("Enemy Hovered on");
                cursor = cursorTypes.Attack;
                if (Input.GetMouseButtonDown(1)) {
                    Debug.Log("Enemy Clicked on");
                    // TODO: Give indication on enemy clicked
                    Transform target = raycastHit2D.transform;
                    foreach (GameObject unit in selectedUnits) {
                        if (!unit) continue;
                        if (unit.GetComponent<AttackController>()) {
                            unit.GetComponent<AttackController>().targetToAttack = target;
                        }
                    }
                }
            } else {
                cursor = cursorTypes.Default;
            }

        // Collect Material
        } else if (selectedUnits.Count > 0 && WorkerUnitPresent(selectedUnits)) {
            Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseInWorld.z = 0;
            // Hey stupid! Make sure the game object has a collider!
            RaycastHit2D raycastHit2D = Physics2D.Raycast(mouseInWorld,(mouseInWorld-Camera.main.transform.position).normalized,100,resourceLayer);
            if (raycastHit2D) { // Purpose: cursor image
                cursor = cursorTypes.Collect;
                if (Input.GetMouseButtonDown(1)) {
                    // can only current give the workers the collect command
                    foreach (GameObject unit in selectedUnits) {
                        if (!unit) continue;
                        if (unit.GetComponent<WorkerUnitController>()) {
                            unit.GetComponent<WorkerUnitController>().Collect(raycastHit2D.transform); // Worker will proceeds to take over
                        }
                    }
                }
            } else {
                cursor = cursorTypes.Default;
            }
        }
    }
    // Checks to see if a worker unit is present
    private bool WorkerUnitPresent(List<GameObject> units) {
        foreach (GameObject unit in selectedUnits) {
            if (!unit) continue;
            if (unit.GetComponent<WorkerUnitController>())
                return true;
        }
        return false;
    }

    // Checks to see if an attack unit is present
    private bool OffensiveUnitPresent(List<GameObject> units) {
        foreach (GameObject unit in selectedUnits) {
            if (!unit) continue;
            if (unit.GetComponent<AttackController>())
                return true;
        }
        return false;
    }

    private void MultiSelect(GameObject unit) {
        if (selectedUnits.Contains(unit) == false) {
            selectedUnits.Add(unit);
            SelectUnit(unit,true);
        } else {
            SelectUnit(unit,false);
            selectedUnits.Remove(unit);
        }
    }

    private void SelectByClicking(GameObject unit) {
        DeselectAll();

        selectedUnits.Add(unit);

        SelectUnit(unit,true);
    }

    private void EnableUnitMovement(GameObject unit, bool isSelected) {
        unit.GetComponent<UnitController>().isSelected = isSelected;
    }

    public void DeselectAll() {
        foreach (var unit in selectedUnits) {
            if (!unit) continue;
            SelectUnit(unit,false);
        }
        selectedUnits.Clear();
    }

    internal void DragSelect(GameObject unit) {
        if (selectedUnits.Contains(unit) == false) {
            selectedUnits.Add(unit);
            SelectUnit(unit,true);
        }
    }

    private void TriggerSelectionIndicator(GameObject unit, bool isVisible) {
        unit.transform.GetChild(1).gameObject.SetActive(isVisible);
    }

    private void SelectUnit(GameObject unit, bool b) {
        TriggerSelectionIndicator(unit,b);
        EnableUnitMovement(unit,b);
    }
}
