using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateShieldAmount : MonoBehaviour
{
    public ShieldController sc_;
    public Image shield_amount_image_;
    public Sprite shield_amount_0_;
    public Sprite shield_amount_25_;
    public Sprite shield_amount_50_;
    public Sprite shield_amount_75_;
    public Sprite shield_amount_100_;
    // Start is called before the first frame update
    void Start()
    {
        sc_.UpdateScoreUI += UpdateShieldAmountUI;
    }

    // Update is called once per frame
    void Update()
    {   
        // shield_amount_image_.sprite = 
    }

    void UpdateShieldAmountUI(float shield_amount){
        if(shield_amount >= 100.0f ){
            shield_amount_image_.sprite = shield_amount_100_;
        }else if(shield_amount >= 75.0f){
            shield_amount_image_.sprite = shield_amount_75_;
        }else if(shield_amount >= 50.0f){
            shield_amount_image_.sprite = shield_amount_50_;
        }else if(shield_amount > 0.0f ){
            shield_amount_image_.sprite = shield_amount_25_;
        }else {
            shield_amount_image_.sprite = shield_amount_0_;
        }
    }
}
