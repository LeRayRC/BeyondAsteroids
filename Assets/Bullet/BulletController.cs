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

        if(col.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            GenericEnemyController gec_ = col.gameObject.GetComponent<GenericEnemyController>();
            gec_.TakeDamage(damage_, gameObject);
        }


        GameObject go_ = Instantiate<GameObject>(GameManager.instance.enemyExplosionParticles_, transform.position, transform.rotation);
        go_.GetComponentInChildren<ParticleSystem>().Play();
        Destroy(this.gameObject,0.0f);
    }
}
