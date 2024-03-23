using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager UnitSelector;
    [SerializeField] public List<GameObject> allUnits = new List<GameObject>();
    [SerializeField] public List<GameObject> selectedUnits = new List<GameObject>();
    
    [SerializeField] public LayerMask unitLayer;
    [SerializeField] public LayerMask attackableLayer;

    public bool attackCursorVisible = false;

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
            if (raycastHit2D) {
                Debug.Log("Enemy Hovered on");
                attackCursorVisible = true;
                if (Input.GetMouseButtonDown(1)) {
                    Debug.Log("Enemy Clicked on");
                    Transform target = raycastHit2D.transform;
                    foreach (GameObject unit in selectedUnits) {
                        if (unit.GetComponent<AttackController>()) {
                            unit.GetComponent<AttackController>().targetToAttack = target;
                        }
                    }
                }
            } else {
                attackCursorVisible = false;
            }
        }
    }

    private bool OffensiveUnitPresent(List<GameObject> units) {
        foreach (GameObject unit in selectedUnits) {
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
