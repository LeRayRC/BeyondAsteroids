using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public GameObject center;
    public float orbit_speed_;
    Transform tr_;
    FollowSplineContainer fs_;
    // Start is called before the first frame update
    void Start(){
        tr_ = GetComponent<Transform>();
        // fs_ = GetComponent<FollowSpline>();
    }

    // Update is called once per frame
    void Update(){
        tr_.RotateAround(center.transform.position,Vector3.forward,orbit_speed_ * Time.deltaTime);
    }

    void SelectSpline(){
        // Select spline to follow from singleton variable
        
        // fs_.spline_ = GameManager.instance.splineOrbit_.Splines[0];
        //  
        // 
        // 
        // 
    }
}
