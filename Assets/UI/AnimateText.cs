using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateText : MonoBehaviour
{
    // Start is called before the first frame update
    Transform tr_;
    Vector3 initScale_;
    Vector3 nextScale_;

    public float timeAnimation_;
    float timer_;
    bool scaling;
    void Start()
    {
        tr_ = GetComponent<Transform>();
        initScale_ = tr_.transform.localScale;
        nextScale_ = initScale_*1.2f;
        timer_ = 0.0f;
        scaling = true;
    }

    // Update is called once per frame
    void Update(){
        if(scaling){
            timer_ += Time.deltaTime;
            if(timer_ >= timeAnimation_){
                timer_ = timeAnimation_;
                scaling = false;
            }
        }else{
            timer_ -= Time.deltaTime;
            if(timer_ <= 0.0f){
                timer_ = 0.0f;
                scaling = true;
            }
        }

        tr_.transform.localScale = Vector3.Lerp(initScale_, nextScale_, timer_ / timeAnimation_);
        
    }
}
