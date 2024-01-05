using UnityEngine;

public interface IDamageable
{
    // Start is called before the first frame update
    public void TakeDamage(float damage, GameObject causer);
}
