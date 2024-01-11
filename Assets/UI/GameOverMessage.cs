using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverMessage : MonoBehaviour
{
    public TMP_Text gameOverText_;
    public float animationTime_;
        // Start is called before the first frame update
    PlayerController pc_;
    void Start()
    {
        pc_ = GameManager.instance.player_.GetComponent<PlayerController>();
        gameOverText_.overrideColorTags = true;
        pc_.PlayerDead += InitGameOverMessage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitGameOverMessage(){
        GameManager.instance.gameoverCanvas_.enabled = true;
        StartCoroutine(AnimateGameOverText());
        //Show during 9 seconds

    }

    IEnumerator AnimateGameOverText(){
        float alpha = 0.0f;
        float timer = 0.0f;
        while(alpha <= 1.0f){
            timer += Time.deltaTime;
            alpha = timer/animationTime_;
            Color new_color = gameOverText_.color;
            new_color.a = alpha;
            gameOverText_.color = new_color;
            yield return null;
        }
        // GameManager.ClearDeletableList();
        MenusController.LoadMainMenu();
    }
}
