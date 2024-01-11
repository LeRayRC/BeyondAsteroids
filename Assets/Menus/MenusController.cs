using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenusController : MonoBehaviour
{
    // Start is called before the first frame update
    public static void ExitGame(){
        Application.Quit();
    }

    public static void LoadCredits(){
        //Load credits
        GameManager.instance.settingsCanvas_.enabled = false;
        GameManager.instance.creditsCanvas_.enabled = true;
        GameManager.instance.mainMenuCanvas_.enabled = false;
    }

    public static void LoadTutorial(){
        //Load tutorial 
        //En realidad carga los controles
    }

    public static void LoadSettings(){
        //Load settings
        GameManager.instance.settingsCanvas_.enabled = true;
        GameManager.instance.creditsCanvas_.enabled = false;
        GameManager.instance.mainMenuCanvas_.enabled = false;
    }

    public void DropDownValueChanged(TMP_Dropdown change)
    {
        // Debug.Log("DD index changed to: " + change.options[change.value].text);
        switch(change.value){
            case 0:
                GameManager.instance.GetComponent<GameDirector>().currentPhase_ = (GamePhases) change.value;
                break;
            case 1:
                GameManager.instance.GetComponent<GameDirector>().currentPhase_ = (GamePhases) change.value;
                break;
            case 2:
                GameManager.instance.GetComponent<GameDirector>().currentPhase_ = (GamePhases) change.value;
                break;
            case 3:
                GameManager.instance.GetComponent<GameDirector>().currentPhase_ = (GamePhases) change.value;
                break;
        }
    }

    public static void ToggleGodMode(Toggle toggle){
        GameManager.instance.player_.GetComponent<PlayerController>().invencible_ = toggle.isOn;
    }

    public static void LoadMainMenu(){
        GameManager.instance.settingsCanvas_.enabled = false;
        GameManager.instance.creditsCanvas_.enabled = false;
        GameManager.instance.mainMenuCanvas_.enabled = true;
        GameManager.instance.gameoverCanvas_.enabled = false;
        GameManager.instance.gamePaused_ = true;
        Time.timeScale = 0.0f;
        // GameManager.ClearDeletableList();
    }

    public static void StartGame(){
        // GameManager.ClearDeletableList();
        PlayerController pc_ = GameManager.instance.player_.GetComponent<PlayerController>();
        pc_.Init();
        GameManager.instance.player_.GetComponent<PlayerShooting>().ResetPowerUps();
        GameManager.instance.player_.GetComponentInChildren<ShieldController>().Init();
        pc_.SpawnPlayer();
        // GameManager.instance.player_.GetComponent<PlayerController>().SpawnPlayer();
        GameManager.instance.mainMenuCanvas_.enabled = false;
        GameManager.instance.settingsCanvas_.enabled = false;
        GameManager.instance.creditsCanvas_.enabled = false;
        GameManager.instance.gameoverCanvas_.enabled = false;

        GameManager.instance.player_.SetActive(true);
        //Reset director timers
        GameManager.instance.GetComponent<GameDirector>().ResetPhases();
        // GameManager.instance.star
        GameManager.instance.gamePaused_ = false;
        Time.timeScale = 1.0f;
    }

    // public void reloadScene(){
    //     SceneManager.LoadScene(0);
        
    // }
}
