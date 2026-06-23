using UnityEngine;

public class IJaSBurnEffect : IJaSStatusEffect
{
    [SerializeField] int remainingHits = 3;

    public override void SetupStatus()
    {
        statusType = "Burn";
        Debug.Log("Hello");
    }

    public override void StatusEffect()
    {
        int damageDealt = Random.Range(3, 7);
        target.TakeDamage(damageDealt); //Take burn damage

        IJaSTypewriter.Instance.queuedDialogue.Add($"{target.Identifier} took {damageDealt} burn damage");

        remainingHits--;

        if (remainingHits is 0) target.statuses.Remove(this); //Remove if time is up
    }
}
