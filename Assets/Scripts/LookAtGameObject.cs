using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtGameObject : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform lookAtTr_;
    Transform tr_;
    Vector3 forwardDir_ = new Vector3();
    void Start()
    {
        tr_ = GetComponent<Transform>();
        if(lookAtTr_ == null){
            lookAtTr_ = GameManager.instance.player_.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(lookAtTr_ != null){
            forwardDir_ = lookAtTr_.position - tr_.position;
            forwardDir_.Normalize();
            tr_.up = forwardDir_;
        }
    }
}
