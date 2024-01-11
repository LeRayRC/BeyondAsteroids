using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnGameOver : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerController pc_;
    void Start(){   
        pc_ = GameManager.instance.player_.GetComponent<PlayerController>();
        pc_.PlayerDead += AutoDestroyMySelf;
        pc_.PlayerSpawned += AutoDestroyMySelfInstant;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.player_.activeSelf){
            // Destroy(gameObject,0.0f);
        }
    }

    // public void RemoveEvent(){
    //     pc_.PlayerDead -= AutoDestroy;
    // }
    public void AutoDestroyMySelf(){
        Debug.Log("Destroying");
        if(this != null){
            Destroy(gameObject,1.0f);
        }
        // if(gameObject != null){
            // if(!gameObject.IsDestroyed()){
            // }
        // }
    }

    public void AutoDestroyMySelfInstant(){
        Debug.Log("Destroying");
        if(this != null){
            Destroy(gameObject,0.0f);
        }
        // if(gameObject != null){
            // if(!gameObject.IsDestroyed()){
            // }
        // }
    }
}
