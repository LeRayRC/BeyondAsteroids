using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyController : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    public float health_;
    public float collisionDamage_;

    public List<GameObject> memberList_;
    

    void Start(){
    }
    public void TakeDamage(float damage, GameObject causer){
        GameManager.instance.player_.GetComponentInChildren<ShieldController>().RegenerateShield(damage*0.5f);
        if(damage < 0.0f) return;
        if(damage < health_){
            health_ -= damage;
        }else{
            if(memberList_ != null){
                for(int i=0; i<memberList_.Count;i++){
                    if(memberList_[i] == gameObject){
                        memberList_.RemoveAt(i);
                        break;
                    }  
                }
            }
            Debug.Log("Destroy enemy");
            Destroy(gameObject,0.0f);
         }
        }

    

    void OnCollisionEnter2D(Collision2D col){
        Debug.Log(col.gameObject.name);
        if(col.gameObject.layer == LayerMask.NameToLayer("Shield")){
            ShieldController sc_ = col.gameObject.GetComponent<ShieldController>();
            sc_.TakeDamage(collisionDamage_, gameObject);
        }

        if(col.gameObject.layer == LayerMask.NameToLayer("Player")){
            PlayerController pc_ = col.gameObject.GetComponent<PlayerController>();
            pc_.TakeDamage(collisionDamage_, gameObject);
        }

        GameObject go_ = Instantiate<GameObject>(GameManager.instance.enemyExplosionParticles_, transform.position, transform.rotation);
        go_.GetComponentInChildren<ParticleSystem>().Play();

        if(col.gameObject.layer != LayerMask.NameToLayer("Bullet")){
            Destroy(gameObject,0.0f);
        }

    }


}
