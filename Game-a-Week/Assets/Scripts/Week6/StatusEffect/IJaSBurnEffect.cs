using UnityEngine;

public class IJaSBurnEffect : IJaSStatusEffect
{
    public override void StatusEffect()
    {
        target.TakeDamage(Random.Range(3, 7)); //Take burn damage
    }
}
