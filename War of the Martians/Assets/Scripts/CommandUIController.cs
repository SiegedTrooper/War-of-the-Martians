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

    private bool isMaterialFlashing = false;
    private bool isOxygenFlashing = false;

    // Singleton Creation
    private void Awake() {
        if (instance != null && instance != this) {
            Debug.Log("Another CommandUIController exists! Deleting this second one...");
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    private string FormatCount(int count, string color1, string color2) {
        string output = "";
        int digitCount = Mathf.Abs(count).ToString().Length;
        switch (digitCount) {
            case 1:
                output = "<color=" + color1 + ">0000</color><color=" + color2 + ">" + count.ToString() + "</color>";
                break;
            case 2:
                output = "<color=" + color1 + ">000</color><color=" + color2 + ">" + count.ToString() + "</color>";
                break;
            case 3:
                output = "<color=" + color1 + ">00</color><color=" + color2 + ">" + count.ToString() + "</color>";
                break;
            case 4:
                output = "<color=" + color1 + ">0</color><color=" + color2 + ">" + count.ToString() + "</color>";
                break;
            case 5:
            default:
                output = "<color=white>" + count.ToString() + "</color>";
                break;
        }
        return output;
    }

    public void FlashMaterialCounter() {
        if (isMaterialFlashing) {
            return;
        }
        
        isMaterialFlashing = true;
        IEnumerator Flash() {
            materialCount.GetComponent<TextMeshProUGUI>().text = FormatCount(PlayerResources.instance.GetMaterialPoints(), "#8E3B3B", "red");
            yield return new WaitForSeconds(.5f);
            materialCount.GetComponent<TextMeshProUGUI>().text = FormatCount(PlayerResources.instance.GetMaterialPoints(), "#8E8E8E", "white");
            yield return new WaitForSeconds(.5f);
            materialCount.GetComponent<TextMeshProUGUI>().text = FormatCount(PlayerResources.instance.GetMaterialPoints(), "#8E3B3B", "red");
            yield return new WaitForSeconds(.5f);
            materialCount.GetComponent<TextMeshProUGUI>().text = FormatCount(PlayerResources.instance.GetMaterialPoints(), "#8E8E8E", "white");
            yield return new WaitForSeconds(.5f);
            materialCount.GetComponent<TextMeshProUGUI>().text = FormatCount(PlayerResources.instance.GetMaterialPoints(), "#8E3B3B", "red");
            yield return new WaitForSeconds(.5f);
            materialCount.GetComponent<TextMeshProUGUI>().text = FormatCount(PlayerResources.instance.GetMaterialPoints(), "#8E8E8E", "white");
            isMaterialFlashing = false;
        }
        StartCoroutine(Flash());
    }

    public void FlashOxygenCounter() {
        if (isOxygenFlashing) {
            return;
        }

        isOxygenFlashing = true;
        IEnumerator Flash() {
            oxygenCount.GetComponent<TextMeshProUGUI>().text = FormatCount(PlayerResources.instance.GetOxygenPoints(), "#8E3B3B", "red");
            yield return new WaitForSeconds(.5f);
            oxygenCount.GetComponent<TextMeshProUGUI>().text = FormatCount(PlayerResources.instance.GetOxygenPoints(), "#8E8E8E", "white");
            yield return new WaitForSeconds(.5f);
            oxygenCount.GetComponent<TextMeshProUGUI>().text = FormatCount(PlayerResources.instance.GetOxygenPoints(), "#8E3B3B", "red");
            yield return new WaitForSeconds(.5f);
            oxygenCount.GetComponent<TextMeshProUGUI>().text = FormatCount(PlayerResources.instance.GetOxygenPoints(), "#8E8E8E", "white");
            yield return new WaitForSeconds(.5f);
            oxygenCount.GetComponent<TextMeshProUGUI>().text = FormatCount(PlayerResources.instance.GetOxygenPoints(), "#8E3B3B", "red");
            yield return new WaitForSeconds(.5f);
            oxygenCount.GetComponent<TextMeshProUGUI>().text = FormatCount(PlayerResources.instance.GetOxygenPoints(), "#8E8E8E", "white");
            isOxygenFlashing = false;
        }
        StartCoroutine(Flash());
    }

    public void UpdateCount(int count, ResourceType resource) {
        // Edge case, user could exceed the counter
        if (count > 99999) {
            count = 99999;
        }

        string output = FormatCount(count, "#8E8E8E", "white");
        
        if (resource == ResourceType.Material & !isMaterialFlashing) {
            materialCount.GetComponent<TextMeshProUGUI>().text = output;
        } else if (resource == ResourceType.Oxygen & !isOxygenFlashing) {
            oxygenCount.GetComponent<TextMeshProUGUI>().text = output;
        }
    }
}