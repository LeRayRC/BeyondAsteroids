using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameVictory : MonoBehaviour
{
    public TMP_Text victoryText;
    public float animationTime_;

    
        // Start is called before the first frame update
    // BossController bc_;
    void Start()
    {
        // bc_ = GameManager.instance.player_.GetComponent<PlayerController>();
        victoryText.overrideColorTags = true;
        // bc_.PlayerDead += InitGameOverMessage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InitVictoryMessage(){
        GameManager.instance.gameoverCanvas_.enabled = true;
        Debug.Log("Started routine");
        StartCoroutine(AnimateGameOverText());
        //Show during 9 seconds

    }

    IEnumerator AnimateGameOverText(){
        GameManager.instance.player_.SetActive(false);
        victoryText.text = "you win";
        float alpha = 0.0f;
        float timer = 0.0f;
        while(alpha <= 1.0f){
            timer += Time.deltaTime;
            alpha = timer/animationTime_;
            Color new_color = victoryText.color;
            new_color.a = alpha;
            victoryText.color = new_color;
            yield return null;
        }
        // GameManager.ClearDeletableList();
        Debug.Log("Load main menu");
        MenusController.LoadMainMenu();
    }
}
