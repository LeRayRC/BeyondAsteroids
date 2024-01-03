using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour{
    public static GameManager instance = null; //Static instance of GameInstance which allows it to be

    public int gameLevel;
    public float timeElapsedOnGame;
    public float timeToChangeLevel;
    public float enemyWaveRatio;
    public int enemyWaveSize;
    public Camera camera_;
    public GameObject player_;
    public GameObject enemyExplosionParticles_;

    public List<GameObject> bulletList_;
    public List<MeshRenderer> levelBackgrounds_;
    public int bulletCount_;

    void Awake(){
        //Check if instance already exists
        if (instance == null){
            instance = this;
        }else if (instance != this){
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
}
