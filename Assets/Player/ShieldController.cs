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
    public ParticleSystem shield_particles_;
    // Start is called before the first frame update
    void Start(){
        shield_active_ = false;
        shield_particles_ = GetComponent<ParticleSystem>();
        shield_particles_.Stop();
    }

    // Update is called once per frame
    void Update(){
        if(shield_active_){
            GetComponent<Collider2D>().enabled = true;
        }else{
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void TakeDamage(float damage, GameObject causer){
        if(damage < 0) return;
        if(damage < shield_amount ){
            shield_amount -= damage;
        }else{
            shield_amount = 0.0f;
            shield_active_ = false;
            shield_particles_.Stop();
        }
        UpdateShieldEmissionParticles();
        UpdateScoreUI(shield_amount);
    }

    public void RegenerateShield(float heal){
        shield_amount += heal;
        if(shield_amount >= 100.0f) shield_amount = 100.0f;
        if(shield_amount >= 0.0f) shield_particles_.Play(); shield_active_ = true;
        UpdateShieldEmissionParticles();
        UpdateScoreUI(shield_amount);
    }

    void UpdateShieldEmissionParticles(){
        var emission = shield_particles_.emission;

        if(shield_amount >= 100.0f){
            emission.rateOverTime = 4000.0f;
        }else if(shield_amount >= 75.0f){
            emission.rateOverTime = 3000.0f;
        }else if(shield_amount >= 50.0f){
            emission.rateOverTime = 2000.0f;
        }else if(shield_amount > 0.0f){
            emission.rateOverTime = 1000.0f;
        }else{
            emission.rateOverTime = 0.0f;
        }
    }
}
