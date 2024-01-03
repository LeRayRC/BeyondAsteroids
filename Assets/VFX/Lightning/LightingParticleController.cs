using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingParticleController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj_1_;
    public GameObject obj_2_;
    public GameObject lightning_particles_;

    public float lightning_ratio_;
    public float elapsed_time_;
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        elapsed_time_ += Time.deltaTime;
        if(elapsed_time_ >= lightning_ratio_ ){
            elapsed_time_ = 0.0f;
            // double angle_ = Math.Acos((obj_2_.transform.position.x - obj_1_.transform.position.x) /  Vector3.Magnitude(obj_2_.transform.position - obj_1_.transform.position));
            float angle_ = Vector3.Angle(obj_2_.transform.position - obj_1_.transform.position, Vector3.up);
            // Debug.Log(angle_ * 180.0f / Math.PI);
            // Debug.Log(angle_ );
            // GameObject go_ = Instantiate<GameObject>(lightning_particles_prefab_,obj_1_.transform.position + ((obj_2_.transform.position - obj_1_.transform.position) * 0.5f),Quaternion.identity );
            // ParticleSystem lightning_particles_ = go_.GetComponent<ParticleSystem>();
            // var main = lightning_particles_.main;
            if(obj_2_.transform.position.x > obj_1_.transform.position.x){
                angle_ *= -1.0f;

            }
            // angle_ += 1.57f;
            // Debug.Log((float)(angle_ * 180.0f / Math.PI));
            // Debug.Log(angle_);
            // if(angle_ < 0.0f || angle_ > 180.0f){
            //     angle_ += 180.0f;
            // }

            lightning_particles_.transform.rotation = Quaternion.Euler(0.0f,0.0f,angle_);
            lightning_particles_.gameObject.transform.position = obj_1_.transform.position + ((obj_2_.transform.position - obj_1_.transform.position) * 0.5f);
            // // main.startRotation = (float)((angle_ * 180.0f / Math.PI));
            // main.startRotation = (float)(angle_ + 1.71f);

            // lightning_particles_.Play();
            // Destroy(go_, main.duration);

        }
    }
}
