using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTexture : MonoBehaviour
{
    public float scroll_x_;
    public float scroll_y_;
    // Start is called before the first frame update
    float offset_x_; 
    float offset_y_; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offset_x_ = Time.time * scroll_x_;
        offset_y_ = Time.time * scroll_y_;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset_x_,offset_y_);
        // GetComponent<SpriteRenderer>().material.mainTextureOffset = new Vector2(offset_x_,offset_y_);
    }
}
