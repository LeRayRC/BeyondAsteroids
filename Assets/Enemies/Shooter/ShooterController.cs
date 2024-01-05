using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    Transform tr_;
    public Transform shootToTr_;
    public GameObject bulletPrefab_;

    public float fireCooldown_;
    public float timeSinceLastFire_;
    public float damage_;
    public float bullet_speed_;

    // Start is called before the first frame update
    void Start(){
        tr_ = GetComponent<Transform>();
        if(shootToTr_ == null){
            shootToTr_ = GameManager.instance.player_.transform;
        }
    }

    // Update is called once per frame
    void Update(){
        timeSinceLastFire_ += Time.deltaTime;
        if(timeSinceLastFire_ >= fireCooldown_){
            timeSinceLastFire_ = 0.0f;
            Fire();
        }
    }

    void Fire(){
        GameObject go = PlayerShooting.InitBullet(gameObject, bulletPrefab_,tr_.up,0.0f,bullet_speed_);
        BulletController bc_ = go.GetComponent<BulletController>();
        bc_.damage_ = damage_;
    }
}
