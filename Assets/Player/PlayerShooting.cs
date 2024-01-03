using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUps{

    
    public bool doubleFire;
    public bool tripleFire;
    public bool shotgunFire;
    public bool laser;

    public PowerUps(){
        doubleFire = false;
        tripleFire = false;
        shotgunFire = false;
        laser = false;
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

    public float fireCooldown_;
    public float timeSinceLastFire_;

    public float standardfireCooldown_;
    public float laserFireCooldown_;
    PowerUps powerUps_ = new PowerUps();
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)){
            powerUps_.doubleFire = !powerUps_.doubleFire;
        }

        if(Input.GetKeyDown(KeyCode.F2)){
            powerUps_.tripleFire = !powerUps_.tripleFire;
        }

        if(Input.GetKeyDown(KeyCode.F3)){
            powerUps_.shotgunFire = !powerUps_.shotgunFire;
        }

        if(Input.GetKeyDown(KeyCode.F4)){
            powerUps_.laser = !powerUps_.laser;
        }

        timeSinceLastFire_ += Time.deltaTime;
        if(timeSinceLastFire_ >= fireCooldown_){
            timeSinceLastFire_ = 0.0f;
            Fire();
        }

        if(powerUps_.laser){
            fireCooldown_ = laserFireCooldown_;
        }else{
            fireCooldown_ = standardfireCooldown_;
        }
    }

    public void Fire(){
        if(powerUps_.tripleFire){
            if(powerUps_.shotgunFire){
                InitBullet(rightWeapon_,bullet_, ship_.transform.up);
                InitBullet(rightWeapon_,bullet_, ship_.transform.up,(float)Math.PI / 6);
                InitBullet(rightWeapon_,bullet_, ship_.transform.up,-1.0f * (float)Math.PI / 6);

                InitBullet(leftWeapon_,bullet_,  ship_.transform.up);
                InitBullet(leftWeapon_,bullet_,  ship_.transform.up,(float)Math.PI / 6);
                InitBullet(leftWeapon_,bullet_,  ship_.transform.up,-1.0f * (float)Math.PI / 6);

                InitBullet(centralWeapon_,bullet_,  ship_.transform.up);
                InitBullet(centralWeapon_,bullet_,  ship_.transform.up,(float)Math.PI / 6);
                InitBullet(centralWeapon_,bullet_,  ship_.transform.up,-1.0f * (float)Math.PI / 6);

            }else{
                InitBullet(rightWeapon_,bullet_, ship_.transform.up);
                InitBullet(leftWeapon_,bullet_, ship_.transform.up);
                InitBullet(centralWeapon_,bullet_, ship_.transform.up);
            }
        }
        if(powerUps_.doubleFire){
            if(powerUps_.shotgunFire){
                InitBullet(rightWeapon_,bullet_, ship_.transform.up);
                InitBullet(rightWeapon_,bullet_, ship_.transform.up,(float)Math.PI / 6);
                InitBullet(rightWeapon_,bullet_, ship_.transform.up,-1.0f * (float)Math.PI / 6);

                InitBullet(leftWeapon_,bullet_, ship_.transform.up);
                InitBullet(leftWeapon_,bullet_, ship_.transform.up,(float)Math.PI / 6);
                InitBullet(leftWeapon_,bullet_, ship_.transform.up,-1.0f * (float)Math.PI / 6);

            }else{
                InitBullet(rightWeapon_,bullet_ ,ship_.transform.up);
                InitBullet(leftWeapon_,bullet_  ,ship_.transform.up);
            }
        }else{
            if(powerUps_.shotgunFire){
                InitBullet(centralWeapon_,bullet_, ship_.transform.up);
                InitBullet(centralWeapon_,bullet_, ship_.transform.up,(float)Math.PI / 6);
                InitBullet(centralWeapon_,bullet_, ship_.transform.up,-1.0f * (float)Math.PI / 6);
            }else{
                InitBullet(centralWeapon_,bullet_, ship_.transform.up);
            }
        }
    }

    public static void InitBullet(GameObject prefab, GameObject bullet_prefab, Vector3 bulletDir, float angle = 0.0f, float speed=20.0f){
        GameObject go;
        Vector3 newDir = new Vector3();
        newDir.x = bulletDir.x * (float)Math.Cos(angle) - bulletDir.y * (float)Math.Sin(angle);
        newDir.y = bulletDir.x * (float)Math.Sin(angle) + bulletDir.y * (float)Math.Cos(angle);
        go = Instantiate<GameObject>(bullet_prefab, prefab.transform.position,prefab.transform.rotation);
        go.transform.Rotate(new Vector3(0.0f,0.0f,angle * 180 / (float)Math.PI));
        go.GetComponent<BulletController>().bulletDir_ = newDir;
        go.GetComponent<BulletController>().bulletSpeed_ = speed;
        if(prefab.GetComponent<ParticleSystem>() != null){
            prefab.GetComponent<ParticleSystem>().Play();
        }
    }
}
