using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject AIsFolder;
    private bool cooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IEnumerator a (){
            Vector2 pos = transform.position;
            pos.x -= 2;
            pos.y += 2;
            SpawnPrefab(enemyPrefab, pos);
            yield return new WaitForSeconds(30f);
            cooldown = false;
        }
        if (!cooldown) {
            cooldown = true;
            StartCoroutine(a());
        }
    }

    private void SpawnPrefab(GameObject prefab, Vector3 pos) {
        GameObject a = Instantiate(prefab, pos, Quaternion.identity);
        a.transform.eulerAngles = new Vector3(0f,0f,-33.33f);
        a.transform.parent = AIsFolder.transform;
    } 
}
