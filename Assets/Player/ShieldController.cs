using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ShieldController : MonoBehaviour, IDamageable
{
    public float shield_amount;
    public delegate void OnHitHeader(float amount);
    public event OnHitHeader UpdateScoreUI;
    public bool shield_active_;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void TakeDamage(int damage, GameObject causer){
        if(damage < 0) return;
        if(damage < shield_amount ){
            shield_amount -= damage;
        }else{
            shield_amount = 0.0f;
            shield_active_ = false;
        }
        UpdateScoreUI(shield_amount);
    }

    public void RegenerateShield(float heal){
        shield_amount += heal;
        if(shield_amount >= 100.0f) shield_amount = 100.0f;

        UpdateScoreUI(shield_amount);
    }
}
