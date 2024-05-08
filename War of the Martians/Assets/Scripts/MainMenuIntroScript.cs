using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
//using DG.Tweening; is broken for some reason

public class MainMenuIntroScript : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private AudioSource audioIntro;
    [SerializeField] private AudioSource audioMain;
    [SerializeField] private RawImage fade;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private Button play;
    [SerializeField] private Button options;
    [SerializeField] private Button quit;
    [SerializeField] private Button mission1;
    [SerializeField] private Button mission2;
    [SerializeField] private Button mission3;
    [SerializeField] private Button missionBack;
    [SerializeField] private GameObject missionSelectGroup;
    [SerializeField] private GameObject optionSelectGroup;
    public AudioSource click;

    private float titleStartingPosY;

    void Awake() {
        fade.gameObject.SetActive(true);
        title.gameObject.SetActive(false);
        play.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
    }

    void Start() {
        titleStartingPosY = title.transform.position.y;
        title.transform.position = new Vector3(0,0,0);
        StartCoroutine(BeginIntro());
    }

    IEnumerator BeginIntro() {
        audioIntro.Play();
        yield return new WaitForSeconds(.5f);
        title.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        StartCoroutine(FadeOut());
        StartCoroutine(MoveTitle());
        audioMain.Play();
    }

    IEnumerator FadeOut() {
        while (fade.color.a > 0) {
            fade.color = new Color(fade.color.r,fade.color.g,fade.color.b,fade.color.a-Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        fade.gameObject.SetActive(false);
    }

    IEnumerator MoveTitle() {
        while (title.transform.position.y < titleStartingPosY) {
            title.transform.position += new Vector3(0,(Time.deltaTime*2),0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        play.gameObject.SetActive(true);
        options.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
    }

    public void MissionButton() {
        click.Play();
        missionSelectGroup.SetActive(true);
        play.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
    }

    public void OptionButton() {
        click.Play();
        Debug.Log("Options Clicked");
    }

    public void QuitButton() {
        click.Play();
        Debug.Log("Quitting Game...");
        // Save any game data if necessary

        // Quit the application
        Application.Quit();
        
        // If we're running in the Unity editor
        #if UNITY_EDITOR
        // Stop playing the scene in the editor
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void Mission1Button() {
        click.Play();
        Debug.Log("Switching Scene to Mission 1 - Defense");
        SceneManager.LoadScene("Mission1");
    }

    public void Mission2Button() {
        click.Play();
        Debug.Log("Not Implemented");
    }

    public void Mission3Button() {
        click.Play();
        Debug.Log("Not Implemented");
    }

    public void MissionBackButton() {
        click.Play();
        missionSelectGroup.SetActive(false);
        play.gameObject.SetActive(true);
        options.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
    }
}
