using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameResources;

public class CommandUIController : MonoBehaviour
{
    // Singleton
    public static CommandUIController instance;

    [Header("Objects")]
    [SerializeField] TextMeshProUGUI materialCount;
    [SerializeField] TextMeshProUGUI oxygenCount;

    // Singleton Creation
    private void Awake() {
        if (instance != null && instance != this) {
            Debug.Log("Another CommandUIController exists! Deleting this second one...");
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    public void UpdateCount(int count, ResourceType resource) {

        // Edge case, user could exceed the counter
        if (count > 99999) {
            count = 99999;
        }

        string output = "";
        int digitCount = Mathf.Abs(count).ToString().Length;
        switch (digitCount) {
            case 1:
                output = "<color=#8E8E8E>0000</color><color=white>" + count.ToString() + "</color>";
                break;
            case 2:
                output = "<color=#8E8E8E>000</color><color=white>" + count.ToString() + "</color>";
                break;
            case 3:
                output = "<color=#8E8E8E>00</color><color=white>" + count.ToString() + "</color>";
                break;
            case 4:
                output = "<color=#8E8E8E>0</color><color=white>" + count.ToString() + "</color>";
                break;
            case 5:
            default:
                output = "<color=white>" + count.ToString() + "</color>";
                break;
        }
        if (resource == ResourceType.Material) {
            materialCount.GetComponent<TextMeshProUGUI>().text = output;
        } else if (resource == ResourceType.Oxygen) {
            oxygenCount.GetComponent<TextMeshProUGUI>().text = output;
        }
    }
}