using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    public Camera camera_;
    Vector3 mousePos_;
    Vector3 worldPos_;
    Vector3 forwardDir_;
    Transform shipTr_;
    Rigidbody rb_;

    public float force_;
    public ParticleSystem propulsion_;
    void Start(){
        shipTr_ = gameObject.GetComponent<Transform>();
        

    }

    // Update is called once per frame
    void Update()
    {   
        mousePos_ = Input.mousePosition;
        worldPos_ = camera_.ScreenToWorldPoint(mousePos_);
        worldPos_.x = Mathf.Clamp(worldPos_.x, camera_.ViewportToWorldPoint(new Vector2(0.05f,0.0f)).x  , camera_.ViewportToWorldPoint(Vector2.one *0.95f).x);
        worldPos_.y = Mathf.Clamp(worldPos_.y, camera_.ViewportToWorldPoint(new Vector2(0.0f,0.05f)).y  , camera_.ViewportToWorldPoint(Vector2.one *0.95f).y);
        // Debug.Log();
        worldPos_.z = shipTr_.position.z;
        // Debug.Log(worldPos_);
        //Rotate to mouse
        forwardDir_ = worldPos_ - shipTr_.position;
        forwardDir_.Normalize();
        shipTr_.up = forwardDir_;

        if(Input.GetButton("Fire1")){
            

            // Debug.Log(shipTr_.up);
            // Debug.Log(forwardDir_);
            
            // rb_.AddForce(forwardDir_ * force_, ForceMode.VelocityChange);
            shipTr_.position = Vector3.Lerp(shipTr_.position,new Vector3(worldPos_.x, worldPos_.y, shipTr_.position.z), 0.01f);
            var emission = propulsion_.emission;
            // propulsion_.Play();
            emission.rateOverTime = 75.0f;
        }else{
            
            // rb_.velocity = new Vector3(rb_.velocity.x - (rb_.velocity.x * 0.99f * Time.deltaTime), rb_.velocity.y - ( rb_.velocity.y * 0.99f * Time.deltaTime),0.0f);
            // if(rb_.velocity.x < 0.0f) rb_.velocity = new Vector3(0.0f, rb_.velocity.y,0.0f);
            // if(rb_.velocity.y < 0.0f) rb_.velocity = new Vector3(rb_.velocity.x, 0.0f,0.0f);
            var emission = propulsion_.emission;
            emission.rateOverTime = 0.0f;
            // propulsion_.Stop();

        }

        // Vector3 velocity = rb_.velocity;
        //     if( velocity.x > 0){
        //         velocity.x -= Time.deltaTime;
        //     }else{
        //         velocity.x += Time.deltaTime;
        //     }

        //     if( velocity.y > 0){
        //         velocity.y -= Time.deltaTime;
        //     }else{
        //         velocity.y += Time.deltaTime;
        //     }

        //     rb_.velocity = velocity;
    }

    public void TakeDamage(float damage, GameObject causer){
        //Set gameover
        // Debug.Log("Hitted by " + causer.name);
        gameObject.SetActive(false);
    }
}
