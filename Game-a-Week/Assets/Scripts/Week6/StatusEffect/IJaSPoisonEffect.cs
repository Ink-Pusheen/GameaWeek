using UnityEngine;

public class IJaSPoisonEffect : IJaSStatusEffect
{
    [SerializeField] int remainingHits = 2;

    public override void SetupStatus()
    {
        statusType = "Poison";
    }

    public override void StatusEffect()
    {
        int damageDealt = Random.Range(4, 9);
        target.TakeDamage(damageDealt); //Take poison damage

        IJaSTypewriter.Instance.queuedDialogue.Add($"{target.Identifier} took {damageDealt} poison damage");

        remainingHits--;

        if (remainingHits is 0) target.statuses.Remove(this); //Remove if time is up
    }
}
