using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    public float bulletSpeed_;
    public Vector3 bulletDir_;
    public float lifeTime_;
    public float damage_;
    
    void Start(){
        Destroy(this.gameObject,lifeTime_);
        // GameManager.instance.player_.GetComponent<PlayerController>().PlayerDead += AutoDestroyMySelf;
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(bulletDir_ * bulletSpeed_ * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter2D(Collision2D col){   
        if(col.gameObject.layer == LayerMask.NameToLayer("Shield")){
            ShieldController sc_ = col.gameObject.GetComponent<ShieldController>();
            sc_.TakeDamage(damage_, gameObject);
        }

        if(col.gameObject.layer == LayerMask.NameToLayer("EnemyShield")){
            BossShieldController bsc_ = col.gameObject.GetComponent<BossShieldController>();
            bsc_.TakeDamage(damage_, gameObject);
        }

        if(col.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            if(col.gameObject.GetComponent<BossController>() != null){
                BossController bc_ = col.gameObject.GetComponent<BossController>();
                bc_.TakeDamage(damage_, gameObject);
            }else{
                GenericEnemyController gec_ = col.gameObject.GetComponent<GenericEnemyController>();
                gec_.TakeDamage(damage_, gameObject);
            }
        }



        if(col.gameObject.layer == LayerMask.NameToLayer("Player")){
            // Destroy(gameObject.GetComponent<OnGameOver>(),0.0f);
            Debug.Log(col.gameObject.name);
            PlayerController pc_ = col.gameObject.GetComponent<PlayerController>();
            Debug.Log("Player hitted");
            pc_.TakeDamage(damage_, gameObject);
        }


        GameObject go_ = Instantiate<GameObject>(GameManager.instance.enemyExplosionParticles_, transform.position, transform.rotation);
        go_.GetComponentInChildren<ParticleSystem>().Play();

        // Destroy(this.gameObject,1.0f);
        Destroy(this.gameObject,0.1f);
    }

    // public void AutoDestroyMySelf(){
    //     Debug.Log("Destroying");
    //     // if(gameObject != null){
    //         // if(!gameObject.IsDestroyed()){
    //             Destroy(gameObject,1.0f);
    //         // }
    //     // }
    // }
}
