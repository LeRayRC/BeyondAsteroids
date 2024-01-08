using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeController : MonoBehaviour{

    Camera camera_;
    Transform tr_;
    Transform targetTr_;

    float timeRoaming_;
    public float minRoamingTime_;
    public float maxRoamingTime_;
    float kamikazeSpeed_;
    public float minKamikazeSpeed_;
    public float maxKamikazeSpeed_;
    Vector3 dir_;
    // Start is called before the first frame update
    void Start(){
        if(camera_ == null){
            camera_ = GameManager.instance.camera_;
        }
        if(targetTr_ == null){
            targetTr_ = GameManager.instance.player_.GetComponent<Transform>();
        }

        timeRoaming_ = Random.Range(minRoamingTime_,maxRoamingTime_);
        kamikazeSpeed_ = Random.Range(minKamikazeSpeed_,maxKamikazeSpeed_);
        tr_ = GetComponent<Transform>();
        //Spawn at random pos x at top or bot position
        if(Random.Range(0,2) == 1){
            tr_.position = camera_.ViewportToWorldPoint(new Vector3(Random.Range(0.0f,1.0f), 0.95f,1.0f));
            tr_.up = Vector3.down;
        }else{
            tr_.position = camera_.ViewportToWorldPoint(new Vector3(Random.Range(0.0f,1.0f), 0.05f, 1.0f));
        }
    }

    // Update is called once per frame
    void Update(){
        if(timeRoaming_ > 0.0f){
            timeRoaming_ -= Time.deltaTime;
            tr_.position = Vector3.Lerp(tr_.position ,new Vector3(targetTr_.position.x, tr_.position.y, tr_.position.z), Time.deltaTime);
            if(timeRoaming_ <= 0.0f){
                if(targetTr_.position.y < tr_.position.y){
                    dir_ = Vector3.down;
                }else{
                    dir_ = Vector3.up;
                }
            }
        }else{
            tr_.position += dir_ * kamikazeSpeed_ * Time.deltaTime;
            if(dir_.y > 0.0f){
                if(tr_.position.y > camera_.ViewportToWorldPoint(new Vector2(0.0f,1.0f)).y){
                    Destroy(gameObject);
                }
            }else{
                if(tr_.position.y < camera_.ViewportToWorldPoint(new Vector2(0.0f,0.0f)).y){
                    Destroy(gameObject);
                }
            }
        } 
    }
}
