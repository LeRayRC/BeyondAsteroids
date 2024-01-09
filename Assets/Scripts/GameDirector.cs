using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GamePhases{
        GamePhases_Phase1,
        GamePhases_Phase2,
        GamePhases_Phase3,
        GamePhases_Phase4, //Boss Phase
    }

public class GameDirector : MonoBehaviour
{
    

    Camera camera_;
    public GamePhases currentPhase_;

    public PhaseOptions Phase1 = new PhaseOptions();
    [SerializeField]
    public PhaseOptions Phase2 = new PhaseOptions();
    [SerializeField]
    public PhaseOptions Phase3 = new PhaseOptions();
    [SerializeField]
    public PhaseOptions Phase4 = new PhaseOptions();

    // Start is called before the first frame update
    void Start(){
        camera_ = GameManager.instance.camera_;
        currentPhase_ = GamePhases.GamePhases_Phase1;
    }

    // Update is called once per frame
    void Update(){
        switch(currentPhase_){
            case GamePhases.GamePhases_Phase1:
                RunPhase(Phase1);
                CheckPhase(Phase1);
                break;
            case GamePhases.GamePhases_Phase2:
                RunPhase(Phase2);
                CheckPhase(Phase2);
                break;
            case GamePhases.GamePhases_Phase3:
                RunPhase(Phase3);
                CheckPhase(Phase3);
                break;
            case GamePhases.GamePhases_Phase4:
                RunPhase(Phase4);
                CheckPhase(Phase4);
                break;
        }

        //Check to change phase
    }

    public void RunPhase(PhaseOptions phase){
        Vector3 spawnPosition_;
        if(phase.timer_delay_ <= phase.initDelay_){
            phase.timer_delay_ += Time.deltaTime;
        }else{
            if(phase.enemy_shooter_activated){
                phase.timer_shooter_ += Time.deltaTime;
                if(phase.timer_shooter_ >= phase.shooter_ratio){
                    //Spawn Shooter
                    if(Random.Range(0,2) == 1){
                        //Spawn at Y axis
                        if(Random.Range(0,2) == 1){
                            //Left Part
                            spawnPosition_ = camera_.ViewportToWorldPoint(new Vector2(0.0f, Random.Range(0.0f, 1.0f)));
                            spawnPosition_.z = 1.0f;

                        }else{
                            //Righ Part
                            spawnPosition_ = camera_.ViewportToWorldPoint(new Vector2(1.0f, Random.Range(0.0f, 1.0f)));
                            spawnPosition_.z = 1.0f;
                        }
                    }else{
                        //Spawn at X axis
                        if(Random.Range(0,2) == 1){
                            //Top Part
                            spawnPosition_ = camera_.ViewportToWorldPoint(new Vector2(Random.Range(0.0f, 1.0f), 1.0f));
                            spawnPosition_.z = 1.0f;

                        }else{
                            //Bottom Part
                            spawnPosition_ = camera_.ViewportToWorldPoint(new Vector2(Random.Range(0.0f, 1.0f), 0.0f));
                            spawnPosition_.z = 1.0f;
                        }
                    }

                    GameObject go_ = Instantiate<GameObject>(GameManager.instance.enemyShooterPrefab_, spawnPosition_, Quaternion.identity);
                    phase.timer_shooter_ = 0.0f;
                }
            }

            if(phase.enemy_orbit_activated){
                phase.timer_orbit_ += Time.deltaTime;
                if(phase.timer_orbit_ >= phase.orbit_ratio){
                    //Spawn Orbit
                    GameObject go_ = Instantiate<GameObject>(GameManager.instance.enemyOrbitPrefab_, gameObject.transform.position, Quaternion.identity);
                    phase.timer_orbit_ = 0.0f;
                }
            }

            if(phase.enemy_kamikaze_activated){
                phase.timer_kamikaze_ += Time.deltaTime;
                if(phase.timer_kamikaze_ >= phase.kamikaze_ratio){
                    //Spawn Orbit
                    GameObject go_ = Instantiate<GameObject>(GameManager.instance.enemyKamikazePrefab_, gameObject.transform.position, Quaternion.identity);
                    phase.timer_kamikaze_ = 0.0f;
                }
            }
        }
    }

    public void CheckPhase(PhaseOptions phase){
        if(phase.initDelay_ <= phase.timer_delay_){
            phase.timer_duration_ += Time.deltaTime;
            if(phase.timer_duration_ >= phase.duration){
                switch(currentPhase_){
                    case GamePhases.GamePhases_Phase1:
                        currentPhase_ = GamePhases.GamePhases_Phase2;
                        ResetPhase(Phase2);
                        break;
                    case GamePhases.GamePhases_Phase2:
                        currentPhase_ = GamePhases.GamePhases_Phase3;
                        ResetPhase(Phase3);
                        break;
                    case GamePhases.GamePhases_Phase3:
                        currentPhase_ = GamePhases.GamePhases_Phase4;
                        ResetPhase(Phase4);
                        break;
                    case GamePhases.GamePhases_Phase4:
                        currentPhase_ = GamePhases.GamePhases_Phase1;
                        ResetPhase(Phase1);
                        break;
                }
            }
        }
    }

    public void ResetPhase(PhaseOptions phase){
        phase.timer_delay_      = 0.0f;
        phase.timer_duration_   = 0.0f;
        phase.timer_kamikaze_   = 0.0f;
        phase.timer_orbit_      = 0.0f;
        phase.timer_shooter_    = 0.0f;
    }
}





[System.Serializable]
public class PhaseOptions{
    public float duration;
    public float initDelay_;
    public bool enemy_shooter_activated;
    public bool enemy_orbit_activated;
    public bool enemy_kamikaze_activated;

    public float shooter_ratio;
    public float orbit_ratio;
    public float kamikaze_ratio;

    public float timer_shooter_;
    public float timer_orbit_;
    public float timer_kamikaze_;
    public float timer_delay_;
    public float timer_duration_;

}

