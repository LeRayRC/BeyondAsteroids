using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public GameObject center;
    public float orbit_speed_;
    Transform tr_;
    // Start is called before the first frame update
    void Start(){
        tr_ = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update(){
        tr_.RotateAround(center.transform.position,Vector3.forward,orbit_speed_ * Time.deltaTime);
    }
}
