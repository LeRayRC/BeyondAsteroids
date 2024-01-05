using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemyController : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    public float health_;

    public void TakeDamage(float damage, GameObject causer){
        GameManager.instance.player_.GetComponentInChildren<ShieldController>().RegenerateShield(damage*0.5f);
        if(damage < 0.0f) return;
        if(damage < health_){
            health_ -= damage;
        }else{
            Destroy(gameObject,0.0f);
        }

    }
}
