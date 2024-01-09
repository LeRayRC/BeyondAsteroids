using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class BossController : MonoBehaviour, IDamageable
{
    public enum Phases{
        Phases_Shield,
        Phases_Minions,
        Phases_Regenerate,

    }
    public GameObject minionPrefab_;
    Phases actualPhase_;
    float shieldAmount_;
    BossShieldController bsc_;
    public SplineContainer minionsSpline_;
    float maxHealth_;
    public float currentHealth_;
    public int minionSpawnAmount_;

    public float fireCooldown_;
    public float healCooldown_;

    float timeToShoot_;
    float timeToHeal_;
    
    public int fireShootsAmount_;
    public GameObject bulletPrefab_;
    public float bulletSpeed_;
    public float bulletDamage_;

    public SplineContainer movingSpline_;
    bool followSpline;
    public Transform tr_;

    FollowSplineContainer fsc_;

    public GameObject explosionParticles_;
    // Start is called before the first frame update
    void Start(){
        if(movingSpline_ == null){
            movingSpline_ = GameManager.instance.bossMovingSpline_;
        } 
        maxHealth_ = currentHealth_;
        bsc_ = GetComponentInChildren<BossShieldController>();
        actualPhase_ = Phases.Phases_Shield;
        bsc_.ShieldDestroyed += ShieldDestroyed;
        timeToShoot_ = 3*fireCooldown_;
        timeToHeal_ = healCooldown_;
        tr_ = GetComponent<Transform>();
        fsc_ = GetComponent<FollowSplineContainer>();
        // fsc_.following_spline = false;
    }

    // Update is called once per frame
    void Update(){

        timeToShoot_ -= Time.deltaTime;
        if(timeToShoot_ <= 0.0f){
            Fire();
            timeToShoot_ = fireCooldown_;
        }
        switch(actualPhase_){
            case Phases.Phases_Shield:
                var knot0 = movingSpline_.Spline.ToArray()[0];
                // Vector3 position = new Vector3(knot0.Position.x, knot0.Position.y,knot0.Position.z);
                Vector3 position = new Vector3(knot0.Position.x, knot0.Position.y,knot0.Position.z) + movingSpline_.gameObject.transform.position;
                tr_.position = Vector3.Lerp(tr_.position, position,Time.deltaTime);
                break;
            case Phases.Phases_Minions:
                break;
            case Phases.Phases_Regenerate:
                break;
        }
        if(currentHealth_ / maxHealth_ <= 0.25f){
            fsc_.enabled = true;
            fsc_.following_spline = true;
            actualPhase_ = Phases.Phases_Regenerate;
            timeToHeal_ -= Time.deltaTime;
            if(timeToHeal_ <= 0.0f){
                timeToHeal_ = healCooldown_;
                currentHealth_ += currentHealth_*0.05f;
            }
        }else{
            fsc_.enabled = false;
            fsc_.following_spline = false;
            var knot0 = movingSpline_.Spline.ToArray()[0];
            // Vector3 position = new Vector3(knot0.Position.x, knot0.Position.y,knot0.Position.z);
            Vector3 position = new Vector3(knot0.Position.x, knot0.Position.y,knot0.Position.z) + movingSpline_.gameObject.transform.position;
            tr_.position = Vector3.Lerp(tr_.position, position,Time.deltaTime);
            // actualPhase_ = Phases.Phases_Shield;
        }
    }

    public void TakeDamage(float damage, GameObject causer){

        if(damage <= 0.0f) return;
        if(damage < currentHealth_ ) currentHealth_ -= damage;
        else{
            //Spawn Boss Explosion
            GameObject go_ = Instantiate<GameObject>(explosionParticles_, tr_.transform.position, tr_.transform.rotation);
            go_.GetComponent<ParticleSystem>().Play();
            Destroy(go_, go_.GetComponent<ParticleSystem>().main.duration);
            Destroy(gameObject);
        }
    }

    public void ShieldDestroyed(){
        Debug.Log("Changing phase");
        //Launch 4 shooters
        actualPhase_ = Phases.Phases_Minions;
        StartCoroutine(SpawnMinions());
    }

    IEnumerator SpawnMinions(){
        for(int i=0;i<minionSpawnAmount_;i++){
            GameObject go_ = Instantiate<GameObject>(minionPrefab_, gameObject.transform.position, gameObject.transform.rotation);
            FollowSpline fs_ = go_.GetComponent<FollowSpline>();
            fs_.spline_ = minionsSpline_;
            yield return new WaitForSeconds(1.0f);
        }
    }

    void Fire(){
        if(fireShootsAmount_ <= 0) fireShootsAmount_ = 1;
        fireShootsAmount_ = 8 + Random.Range(1,10);
        float step = 6.28f/fireShootsAmount_;
        for(int i=0;i<fireShootsAmount_;i++){
            Vector3 dir = new Vector3((float)Mathf.Cos(step * i), (float)Mathf.Sin(step * i));
            PlayerShooting.InitBullet(gameObject,bulletPrefab_,dir,0.0f,bulletSpeed_,bulletDamage_);
        }
    }
}