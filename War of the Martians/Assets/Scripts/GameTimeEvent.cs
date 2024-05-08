using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimeEvent : MonoBehaviour {
    [Header("Mission Time")]
    [SerializeField] private float remainingTime = 600f; // 10 minutes * 60 seconds

    [Header("Audio Sources")]
    public AudioSource Victory;
    public AudioSource Soundtrack;
    public AudioSource LowMachineGun;
    public AudioSource DistantTankShots;
    public AudioSource DistantGunShots;
    public AudioSource DistantExplosions;
    public AudioSource DistantFireworks;

    [Header("Others")]
    public GameObject VictoryScreen;
    public GameObject MineFolder;
    public float chanceToPlayAmbience;
    public Transform AIFolder;
    public GameObject EnemyPrefab;
    public Vector3 attackTowards = new Vector3(-25.47f,-26.89f,0f);

    private AudioSource prevPlayed = null;
    void Start() { 
        prevPlayed = DistantFireworks;
        //remainingTime -= 590f;
        Soundtrack.Play();
    }

    private bool event100;
    private void Update() {
        string baseWord = "Main Objective\n- Survive Hostile Attacks (";
        if (remainingTime > 0) {
            // Decrease remaining time
            remainingTime -= Time.deltaTime;
            // Format the time remaining into minutes and seconds
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            // Update the text display
            gameObject.GetComponent<TextMeshProUGUI>().text = baseWord + string.Format("{0:00}:{1:00}", minutes, seconds) + ")";
            CheckForTimeTriggers(minutes,seconds);
        } else if (!event100) {
            event100 = true;
            // When the countdown finishes;
            gameObject.GetComponent<TextMeshProUGUI>().text = "Main Objective\n<color=#00FF00>- Survive Hostile Attacks (00:00)</color>";
            Debug.Log("Countdown has finished!");
            Soundtrack.Stop();
            Victory.Play();
            VictoryScreen.SetActive(true);
        }
        RandomAmbience();
    }

    public void Button_MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    private void RandomAmbience() {
        int a = Random.Range(1,100);
        float b = a/100f;
        if (b > 1 - chanceToPlayAmbience) {
            // Ambience must not be currently playing either
            if (!prevPlayed.isPlaying) {
                switch(Random.Range(1,6)) {
                    case 1:
                        LowMachineGun.Play();
                        prevPlayed = LowMachineGun;
                        break;
                    case 2:
                        DistantTankShots.Play();
                        prevPlayed = DistantTankShots;
                        break;
                    case 3:
                        DistantGunShots.Play();
                        prevPlayed = DistantGunShots;
                        break;
                    case 4:
                        DistantExplosions.Play();
                        prevPlayed = DistantExplosions;
                        break;
                    case 5:
                        DistantFireworks.Play();
                        prevPlayed = DistantFireworks;
                        break;
                    default:
                        Debug.Log("Uh oh, this is an unexpected value. Change the range on Random!");
                        break;
                }
            }
        }
    }

    private bool event1,event2,event3,event4,event5,event6,event7,event8,event9,event10,event11,event12,event13,event14,event15,event16,event17;
    private void CheckForTimeTriggers(int mins, int secs) {
        if (mins <= 9 & secs <= 50 & !event1) {
            ChatNotifications.instance.Notify("<u>General</u>: Welcome Commander. The rebeling Martians have taken up arms against the provisional government.","white");
            event1 = true;
        }
        if (mins <= 9 & secs <= 46 & !event2) {
            ChatNotifications.instance.Notify("<u>General</u>: Scanners have indicated that they've constructed two outposts in your area.","white");
            event2 = true;
        }
        if (mins <= 9 & secs <= 42 & !event3) {
            ChatNotifications.instance.Notify("<u>General</u>: We've deployed reinforcements and they are on the way. ETA 10 mins,","white");
            event3 = true;
        }
        if (mins <= 9 & secs <= 38 & !event4) {
            ChatNotifications.instance.Notify("<u>General</u>: You're goal is to survive until reinforcement arrive.","white");
            event4 = true;
        }
        if (mins <= 9 & secs <= 34 & !event5) {
            ChatNotifications.instance.Notify("<u>General</u>: Best of luck to you and your soldiers Commander. Ad Astra.","white");
            event5 = true;
        }
        if (mins <= 9 & secs <= 4 & !event6) {
            ChatNotifications.instance.Notify("<u>Adjutant</u>: Hello Commander, I am your Adjutant. I will tell you when enemies are approaching.","white");
            event6 = true;
        }
        if (mins <= 8 & secs <= 30 & !event7) { // First Wave Prepares
            ChatNotifications.instance.Notify("<u>Adjutant</u>: Commander, scanners are picking up enemies movements and they're gathering at our perimeter.","white");
            event7 = true;
            prepareAttackWaveOne();
        }
        if (mins <= 8 & secs <= 26 & !event8) { // First Wave Prepares
            ChatNotifications.instance.Notify("<u>Adjutant</u>: They will be attack in 30 seconds! Hope our defenses are ready.","white");
            event8 = true;
        }
        if (mins <= 7 & secs <= 56 & !event9) { // First Wave Attacks
            ChatNotifications.instance.Notify("<u>Soldier</u>: Here they come! Let's rock 'n roll!","red");
            event9 = true;
            sendAttackWaveOne();
        }
        if (mins <= 6 & secs <= 30 & !event10) { // Second Wave Prepares
            ChatNotifications.instance.Notify("<u>Adjutant</u>: They are gathering again for an attack. We will be attacked in 60 seconds!","white");
            event10 = true;
            prepareAttackWaveTwo();
        }
        if (mins <= 5 & secs <= 30 & !event11) { // Second Wave Attacks
            ChatNotifications.instance.Notify("<u>Soldier</u>: Here they come again. Brace yourselves!","red");
            event11 = true;
            sendAttackWaveTwo();
        }
        if (mins <= 5 & secs <= 10 & !event12) { // Spawn Mines
            event12 = true;
            MineFolder.SetActive(true);
        }
        if (mins <= 4 & secs <= 60 & !event13) { // Mention Mines
            ChatNotifications.instance.Notify("<u>Adjutant</u>: While we were defending, the enemy setup mines through the environment, be careful.","yellow");
            event13 = true;
        }
        if (mins <= 4 & secs <= 10 & !event14) { // Third Wave Prepares
            ChatNotifications.instance.Notify("<u>Adjutant</u>: The enemy are pooling their forces again for another attack. Will be soon.","white");
            event14 = true;
            prepareAttackWaveThree();
        }
        if (mins <= 3 & secs <= 40 & !event15) { // Third Wave Builds Up
            ChatNotifications.instance.Notify("<u>Adjutant</u>: They will be attacking from both sides. Please be prepared.","yellow");
            event15 = true;
            firstBuildUp();
        }
        if (mins <= 3 & secs <= 10 & !event16) { // Third Wave Builds Up
            ChatNotifications.instance.Notify("<u>Adjutant</u>: Seems like the Rebels are about ready to push us. Attack imminent in 30 seconds!","white");
            event16 = true;
            secondBuildUp();
        }
        if (mins <= 2 & secs <= 40 & !event17) { // Third Wave Attacks
            ChatNotifications.instance.Notify("<u>Soldier</u>: Watch y'all! Here they come.","red");
            event17 = true;
            sendAttackWaveThree();
        }
    }

    private List<GameObject> waveOneEnemies = new List<GameObject>();
    private void prepareAttackWaveOne() {
        Vector3 pos1 = new Vector3(25f,-2f,0f);
        Vector3 pos2 = new Vector3(26f,-4f,0f);
        Vector3 pos3 = new Vector3(29f,-3f,0f);

        GameObject enemy1 = Instantiate(EnemyPrefab, pos1, Quaternion.identity);
        GameObject enemy2 = Instantiate(EnemyPrefab, pos2, Quaternion.identity);
        GameObject enemy3 = Instantiate(EnemyPrefab, pos3, Quaternion.identity);
        waveOneEnemies.Add(enemy1);
        waveOneEnemies.Add(enemy2);
        waveOneEnemies.Add(enemy3);

        Vector3 moveToPos = new Vector3(-2.88f,-10.69f,0f);

        foreach (GameObject enemy in waveOneEnemies) {
            if (enemy) {
                enemy.transform.parent = AIFolder;
                enemy.GetComponent<EnemyUnitController>().targetPosition = moveToPos;
                enemy.GetComponent<EnemyUnitController>().isSelected = true;
            }
        }
    }

    private void sendAttackWaveOne() {
        foreach (GameObject enemy in waveOneEnemies) {
            if (enemy) {
                enemy.GetComponent<EnemyUnitController>().targetPosition = attackTowards;
                enemy.GetComponent<EnemyUnitController>().isSelected = true;
            }
        }
    }

    private List<GameObject> waveTwoEnemies = new List<GameObject>();
    private void prepareAttackWaveTwo() {
        Vector3 pos = new Vector3(-3f,30f,0f);

        GameObject enemy1 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy2 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy3 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy4 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy5 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy6 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy7 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        waveTwoEnemies.Add(enemy1);
        waveTwoEnemies.Add(enemy2);
        waveTwoEnemies.Add(enemy3);
        waveTwoEnemies.Add(enemy4);
        waveTwoEnemies.Add(enemy5);
        waveTwoEnemies.Add(enemy6);
        waveTwoEnemies.Add(enemy7);

        Vector3 moveToPos = new Vector3(-18f, 5f, 0f);

        foreach (GameObject enemy in waveTwoEnemies) {
            if (enemy) {
                enemy.transform.parent = AIFolder;
                enemy.GetComponent<EnemyUnitController>().targetPosition = moveToPos;
                enemy.GetComponent<EnemyUnitController>().isSelected = true;
            }
        }
    }

    private void sendAttackWaveTwo() {
        foreach (GameObject enemy in waveTwoEnemies) {
            if (enemy) {
                enemy.GetComponent<EnemyUnitController>().targetPosition = attackTowards;
                enemy.GetComponent<EnemyUnitController>().isSelected = true;
            }
        }
    }

    private List<GameObject> waveThreeEnemies = new List<GameObject>();
    private void prepareAttackWaveThree() {
        Vector3 pos = new Vector3(-3f,30f,0f);

        GameObject enemy1 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy2 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy3 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy4 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy5 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy6 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy7 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        waveThreeEnemies.Add(enemy1);
        waveThreeEnemies.Add(enemy2);
        waveThreeEnemies.Add(enemy3);
        waveThreeEnemies.Add(enemy4);
        waveThreeEnemies.Add(enemy5);
        waveThreeEnemies.Add(enemy6);
        waveThreeEnemies.Add(enemy7);

        Vector3 moveToPos = new Vector3(-18f, 5f, 0f);

        foreach (GameObject enemy in waveThreeEnemies) {
            if (enemy) {
                enemy.transform.parent = AIFolder;
                enemy.GetComponent<EnemyUnitController>().targetPosition = moveToPos;
                enemy.GetComponent<EnemyUnitController>().isSelected = true;
            }
        }
    }

    private void firstBuildUp() {
        List<GameObject> temp = new List<GameObject>();

        Vector3 pos = new Vector3(25f,-2f,0f);

        GameObject enemy1 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy2 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy3 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy4 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy5 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy6 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy7 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        temp.Add(enemy1);
        temp.Add(enemy2);
        temp.Add(enemy3);
        temp.Add(enemy4);
        temp.Add(enemy5);
        temp.Add(enemy6);
        temp.Add(enemy7);

        Vector3 moveToPos = new Vector3(-2.88f,-10.69f,0f);

        foreach (GameObject enemy in temp) {
            if (enemy) {
                enemy.transform.parent = AIFolder;
                enemy.GetComponent<EnemyUnitController>().targetPosition = moveToPos;
                enemy.GetComponent<EnemyUnitController>().isSelected = true;
                waveThreeEnemies.Add(enemy);
            }
        }
    }

    private void secondBuildUp() {
        List<GameObject> temp = new List<GameObject>();

        Vector3 pos = new Vector3(25f,-2f,0f);

        GameObject enemy1 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy2 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy3 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy4 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy5 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy6 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy7 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        temp.Add(enemy1);
        temp.Add(enemy2);
        temp.Add(enemy3);
        temp.Add(enemy4);
        temp.Add(enemy5);
        temp.Add(enemy6);
        temp.Add(enemy7);

        Vector3 moveToPos = new Vector3(-2.88f,-10.69f,0f);

        foreach (GameObject enemy in temp) {
            if (enemy) {
                enemy.transform.parent = AIFolder;
                enemy.GetComponent<EnemyUnitController>().targetPosition = moveToPos;
                enemy.GetComponent<EnemyUnitController>().isSelected = true;
                waveThreeEnemies.Add(enemy);
            }
        }
    }

    private void thirdBuildUp() {
        List<GameObject> temp = new List<GameObject>();
        Vector3 pos = new Vector3(-3f,30f,0f);

        GameObject enemy1 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy2 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy3 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy4 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy5 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy6 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        GameObject enemy7 = Instantiate(EnemyPrefab, pos, Quaternion.identity);
        temp.Add(enemy1);
        temp.Add(enemy2);
        temp.Add(enemy3);
        temp.Add(enemy4);
        temp.Add(enemy5);
        temp.Add(enemy6);
        temp.Add(enemy7);

        Vector3 moveToPos = new Vector3(-18f, 5f, 0f);

        foreach (GameObject enemy in temp) {
            if (enemy) {
                enemy.transform.parent = AIFolder;
                enemy.GetComponent<EnemyUnitController>().targetPosition = moveToPos;
                enemy.GetComponent<EnemyUnitController>().isSelected = true;
                waveThreeEnemies.Add(enemy);
            }
        }
    }

    private void sendAttackWaveThree() {
        foreach (GameObject enemy in waveThreeEnemies) {
            if (enemy) {
                enemy.GetComponent<EnemyUnitController>().targetPosition = attackTowards;
                enemy.GetComponent<EnemyUnitController>().isSelected = true;
            }
        }
    }
}
