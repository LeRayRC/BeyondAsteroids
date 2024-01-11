using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PowerUps{

    
    public float doubleFire;
    public float tripleFire;
    public float shotgunFire;
    public float laser;

    public PowerUps(){
        doubleFire  = 0.0f;
        tripleFire  = 0.0f;
        shotgunFire = 0.0f;
        laser       = 0.0f;
    }
}

public class PlayerShooting : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ship_;
    public GameObject bullet_;
    public GameObject centralWeapon_;
    public GameObject leftWeapon_;
    public GameObject rightWeapon_;
    public float bulletSpeed_;
    public float bulletDamage_;

    public float fireCooldown_;
    public float timeSinceLastFire_;

    public float standardfireCooldown_;
    public float laserFireCooldown_;

    public float maxPowerUpAmount_;
    public PowerUps powerUps_ = new PowerUps();
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gamePaused_ ) return;
        if(Input.GetKeyDown(KeyCode.F1)){
            powerUps_.doubleFire += 10.0f; 
        }

        if(Input.GetKeyDown(KeyCode.F2)){
            powerUps_.tripleFire += 10.0f; 
        }

        if(Input.GetKeyDown(KeyCode.F3)){
            powerUps_.shotgunFire += 10.0f; 
        }

        if(Input.GetKeyDown(KeyCode.F4)){
            powerUps_.laser += 10.0f; 
        }

        timeSinceLastFire_ += Time.deltaTime;
        if(timeSinceLastFire_ >= fireCooldown_){
            timeSinceLastFire_ = 0.0f;
            Fire();

        }

        if(powerUps_.laser > 0.0f) powerUps_.laser -= Time.deltaTime;
        if(powerUps_.doubleFire > 0.0f) powerUps_.doubleFire -= Time.deltaTime;
        if(powerUps_.tripleFire > 0.0f) powerUps_.tripleFire -= Time.deltaTime;
        if(powerUps_.shotgunFire > 0.0f) powerUps_.shotgunFire -= Time.deltaTime;

        if(powerUps_.laser > 0.0f){
            fireCooldown_ = laserFireCooldown_;
        }else{
            fireCooldown_ = standardfireCooldown_;
        }
    }

    public void Fire(){
        if(powerUps_.tripleFire > 0.0f){
            if(powerUps_.shotgunFire > 0.0f){
                InitBullet(rightWeapon_,bullet_, ship_.transform.up, 0.0f, bulletSpeed_, bulletDamage_);
                InitBullet(rightWeapon_,bullet_, ship_.transform.up,(float)Math.PI / 6, bulletSpeed_, bulletDamage_);
                InitBullet(rightWeapon_,bullet_, ship_.transform.up,-1.0f * (float)Math.PI / 6, bulletSpeed_, bulletDamage_);

                InitBullet(leftWeapon_,bullet_,  ship_.transform.up,0.0f, bulletSpeed_, bulletDamage_);
                InitBullet(leftWeapon_,bullet_,  ship_.transform.up,(float)Math.PI / 6, bulletSpeed_, bulletDamage_);
                InitBullet(leftWeapon_,bullet_,  ship_.transform.up,-1.0f * (float)Math.PI / 6,bulletSpeed_, bulletDamage_);

                InitBullet(centralWeapon_,bullet_,  ship_.transform.up,0.0f, bulletSpeed_, bulletDamage_);
                InitBullet(centralWeapon_,bullet_,  ship_.transform.up,(float)Math.PI / 6,bulletSpeed_, bulletDamage_);
                InitBullet(centralWeapon_,bullet_,  ship_.transform.up,-1.0f * (float)Math.PI / 6,bulletSpeed_, bulletDamage_);

            }else{
                InitBullet(rightWeapon_,bullet_, ship_.transform.up,0.0f, bulletSpeed_, bulletDamage_);
                InitBullet(leftWeapon_,bullet_, ship_.transform.up,0.0f, bulletSpeed_, bulletDamage_);
                InitBullet(centralWeapon_,bullet_, ship_.transform.up,0.0f, bulletSpeed_, bulletDamage_);
            }
        }
        if(powerUps_.doubleFire > 0.0f){
            if(powerUps_.shotgunFire > 0.0f){
                InitBullet(rightWeapon_,bullet_, ship_.transform.up,0.0f,bulletSpeed_, bulletDamage_);
                InitBullet(rightWeapon_,bullet_, ship_.transform.up,(float)Math.PI / 6, bulletSpeed_, bulletDamage_);
                InitBullet(rightWeapon_,bullet_, ship_.transform.up,-1.0f * (float)Math.PI / 6, bulletSpeed_, bulletDamage_);

                InitBullet(leftWeapon_,bullet_, ship_.transform.up,0.0f, bulletSpeed_, bulletDamage_);
                InitBullet(leftWeapon_,bullet_, ship_.transform.up,(float)Math.PI / 6, bulletSpeed_, bulletDamage_);
                InitBullet(leftWeapon_,bullet_, ship_.transform.up,-1.0f * (float)Math.PI / 6, bulletSpeed_, bulletDamage_);

            }else{
                InitBullet(rightWeapon_,bullet_ ,ship_.transform.up,0.0f, bulletSpeed_, bulletDamage_);
                InitBullet(leftWeapon_,bullet_  ,ship_.transform.up,0.0f, bulletSpeed_, bulletDamage_);
            }
        }else{
            if(powerUps_.shotgunFire > 0.0f){
                InitBullet(centralWeapon_,bullet_, ship_.transform.up,0.0f, bulletSpeed_, bulletDamage_);
                InitBullet(centralWeapon_,bullet_, ship_.transform.up,(float)Math.PI / 6,bulletSpeed_, bulletDamage_);
                InitBullet(centralWeapon_,bullet_, ship_.transform.up,-1.0f * (float)Math.PI / 6,bulletSpeed_, bulletDamage_);
            }else{
                InitBullet(centralWeapon_,bullet_, ship_.transform.up,0.0f,bulletSpeed_, bulletDamage_);
            }
        }
    }

    public static GameObject InitBullet(GameObject prefab, GameObject bullet_prefab, Vector3 bulletDir, float angle = 0.0f, float speed=20.0f, float damage=0.0f){
        GameObject go;
        Vector3 newDir = new Vector3();
        newDir.x = bulletDir.x * (float)Math.Cos(angle) - bulletDir.y * (float)Math.Sin(angle);
        newDir.y = bulletDir.x * (float)Math.Sin(angle) + bulletDir.y * (float)Math.Cos(angle);
        go = Instantiate<GameObject>(bullet_prefab, prefab.transform.position,prefab.transform.rotation);
        go.transform.Rotate(new Vector3(0.0f,0.0f,angle * 180 / (float)Math.PI));
        go.GetComponent<BulletController>().bulletDir_ = newDir;
        go.GetComponent<BulletController>().bulletSpeed_ = speed;
        go.GetComponent<BulletController>().damage_ = damage;
        if(prefab.GetComponent<ParticleSystem>() != null){
            prefab.GetComponent<ParticleSystem>().Play();
        }
        // GameManager.AddDeletableGameObject(go);
        return go;
    }

    public void SetPowerUp(PowerUpsTypes type, float amount){
        switch (type){
            case PowerUpsTypes.PowerUpsTypes_DoubleShot:
                powerUps_.doubleFire += amount;
                if(powerUps_.doubleFire > maxPowerUpAmount_) powerUps_.doubleFire = maxPowerUpAmount_;
                break;
            case PowerUpsTypes.PowerUpsTypes_TripleShot:
                powerUps_.tripleFire += amount;
                if(powerUps_.tripleFire > maxPowerUpAmount_) powerUps_.tripleFire = maxPowerUpAmount_;
                break;
            case PowerUpsTypes.PowerUpsTypes_ShotgunShot:
                powerUps_.shotgunFire += amount;
                if(powerUps_.shotgunFire > maxPowerUpAmount_) powerUps_.shotgunFire = maxPowerUpAmount_;
                break;
            case PowerUpsTypes.PowerUpsTypes_Laser:
                powerUps_.laser += amount;
                if(powerUps_.laser > maxPowerUpAmount_) powerUps_.laser = maxPowerUpAmount_;
                break;
            
        }
    }

    public void ResetPowerUps(){
        powerUps_.doubleFire  = 0.0f;
        powerUps_.tripleFire  = 0.0f;
        powerUps_.shotgunFire = 0.0f;
        powerUps_.laser       = 0.0f;
    }


}
