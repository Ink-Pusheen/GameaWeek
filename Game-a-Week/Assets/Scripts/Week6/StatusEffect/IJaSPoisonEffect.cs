using UnityEngine;

public class IJaSPoisonEffect : IJaSStatusEffect
{
    public override void StatusEffect()
    {
        target.TakeDamage(Random.Range(4, 9)); //Take poison damage
    }
}
