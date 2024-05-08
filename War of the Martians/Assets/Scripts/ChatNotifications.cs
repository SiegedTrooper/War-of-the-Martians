using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatNotifications : MonoBehaviour
{
    public static ChatNotifications instance;

    public Transform MessagePool;
    public GameObject MessagePrefab;
    public int NumEachBatch = 5;

    private List<GameObject> messagesInPool = new List<GameObject>();

    private void Awake() {
        if (instance != null && instance != this) {
            Debug.Log("Another ChatNotifications exists! Deleting this second one...");
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    private void CreateMessages() {
        for (int i = 0; i < NumEachBatch; i++) {
            GameObject message = Instantiate(MessagePrefab);
            message.SetActive(false);
            messagesInPool.Add(message);
            message.transform.SetParent(MessagePool,false);
        }
    }

    public void Notify(string notificationMessage, string color) {
        Color msgColor;
        if (color == "red") {
            msgColor = Color.red;
        } else if (color == "yellow") {
            msgColor = Color.yellow;
        } else {
            msgColor = Color.white;
        }
        
        if (messagesInPool.Count == 0) {
            CreateMessages();
        }
        GameObject message = messagesInPool[0];
        Debug.Log(message);
        messagesInPool.RemoveAt(0);
        message.GetComponent<TextMeshProUGUI>().text = notificationMessage;
        message.transform.SetParent(gameObject.transform,false);
        message.transform.SetSiblingIndex(0);
        message.GetComponent<TextMeshProUGUI>().color = msgColor;
        message.SetActive(true);

        IEnumerator FadeOut() {
            yield return new WaitForSeconds(5f);
            while (true) {
                if (message.GetComponent<TextMeshProUGUI>().color.a <= 0) {
                    // Adding message back to pool
                    messagesInPool.Add(message);
                    message.transform.SetParent(MessagePool,false);
                    message.SetActive(false);
                    break;
                }

                float someValue = Time.deltaTime * 2;
                yield return new WaitForSeconds(someValue);
                message.GetComponent<TextMeshProUGUI>().color = new Color(message.GetComponent<TextMeshProUGUI>().color.r,message.GetComponent<TextMeshProUGUI>().color.g,message.GetComponent<TextMeshProUGUI>().color.b,message.GetComponent<TextMeshProUGUI>().color.a - Time.deltaTime);
            }
        }
        StartCoroutine(FadeOut());
    }
}
