using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainScreenController : MonoBehaviour
{
    public TMP_Text gameName;
    public TMP_Text infoMessage;
    public Image mainScreen_;
    public bool gameStarted_;
    // Start is called before the first frame update
    void Start()
    {
        gameStarted_ = false;
    }

    // Update is called once per frame
    void Update(){
        if(Input.anyKeyDown){
            gameName.gameObject.SetActive(false);
            infoMessage.gameObject.SetActive(false);
            if(!gameStarted_){
                gameStarted_ = true;
                StartCoroutine(VanishMainScreen());
            }
        }
    }

    IEnumerator VanishMainScreen(){
        while(mainScreen_.color.a > 0.0f){
            // Debug.Log("Vanishing");
            Color newColor = mainScreen_.color;
            newColor.a -= Time.deltaTime;
            mainScreen_.color = newColor;
            // Debug.Log(newColor);
            yield return null;
        }
        //Disable canvas
        Time.timeScale = 0.0f;
        GameManager.instance.mainScreenCanvas_.gameObject.SetActive(false);
    }
}
