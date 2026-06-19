using UnityEngine;

public class IJaSBurnEffect : IJaSStatusEffect
{
    public override void StatusEffect()
    {
        target.TakeDamage(Random.Range(7, 16)); //Take burn damage
    }
}
