using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpsTypes{
        PowerUpsTypes_DoubleShot,
        PowerUpsTypes_TripleShot,
        PowerUpsTypes_ShotgunShot,
        PowerUpsTypes_Laser
    }

public class PowerUpController : MonoBehaviour
{
    // Start is called before the first frame update
    

    public PowerUpsTypes powerUp_;
    public float amount_;

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.layer == LayerMask.NameToLayer("Player")){
            PlayerShooting ps_ = col.GetComponent<PlayerShooting>();
            ps_.SetPowerUp(powerUp_, amount_);
            Destroy(gameObject);
        }
    }
}
