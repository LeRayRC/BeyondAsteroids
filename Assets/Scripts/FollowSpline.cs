using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class FollowSpline : MonoBehaviour{
    // public List<SplineContainer> splinesToFollow_;
    public SplineContainer spline_;
    // Spline input_spline_;
    private float percentage_;
    private float legnth_;

    public float speed_;
    public bool following_spline;
    Transform tr_;
    float3 nearest_point_ = new float3();
    Vector3 nearest_point_vector = new Vector3();
    float nearest_point_t_;
    // Start is called before the first frame update
    void Start(){
        percentage_ = 0.0f;
        tr_ = GetComponent<Transform>();
        legnth_ = spline_.CalculateLength();
        following_spline = false;
        SplineUtility.GetNearestPoint<Spline>(spline_.Spline,new float3(tr_.position.x, tr_.position.y, tr_.position.z),out nearest_point_, out nearest_point_t_,8);
        // Debug.Log(nearest_point_);
        
        nearest_point_vector.x = nearest_point_.x + spline_.gameObject.transform.position.x;
        nearest_point_vector.y = nearest_point_.y + spline_.gameObject.transform.position.y;
        nearest_point_vector.z = nearest_point_.z + spline_.gameObject.transform.position.z;

        // input_spline_ = spline_.Spline;

        // Debug.Log(nearest_point_vector);

        
    }

    // Update is called once per frame
    void Update(){
        if(!following_spline){
            
            if(Vector3.Distance(nearest_point_vector,tr_.position) > 0.1f){
                tr_.position = Vector3.Lerp(tr_.position, nearest_point_vector,0.1f);
            }else{
                following_spline = true;
                percentage_ = CalculatePercentage(tr_.position);
            }
        }else{
            percentage_ += (speed_ * Time.deltaTime) / legnth_;
            // Debug.Log(percentage_);
            // spline_.
            Vector3 currentPosition = spline_.EvaluatePosition(percentage_);
            tr_.position = currentPosition;

            if(percentage_ > 1.0f) {percentage_ = 0.0f;}

            // Debug.Log("Following spline");
        }
    }

    public float CalculatePercentage(Vector3 reference){
        float percentage_;
        Vector3 currentPosition_;
        for(percentage_ = 0.0f; percentage_ < 1.0f; percentage_ += 0.01f){
            currentPosition_ = spline_.EvaluatePosition(percentage_);
            if (Vector3.Distance(reference, currentPosition_) < 0.2f){
                // Debug.Log("Found at " + percentage_);
                break;
            }
        }
        // Debug.Log("Percentage calculated " + percentage_);
        return percentage_;
    }
}
