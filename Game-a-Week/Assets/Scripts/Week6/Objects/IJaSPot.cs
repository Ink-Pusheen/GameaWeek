using UnityEngine;

public class IJaSPot : MonoBehaviour
{
    private IJaSHealth curInteractionTarget;

    private void OnCollisionEnter(Collision collision)
    {
        IJaSHealth hitEntity = collision.gameObject.GetComponent<IJaSHealth>();

        if (hitEntity is not null)
        {
            curInteractionTarget = hitEntity;

            //Debug.Log($"Do stuff w/ {hitEntity.Identifier}"); //Testing

            if (hitEntity.Identifier is "Slime")
            {
                hitEntity.TakeDamage(15);

                IJaSTypewriter.Instance.queuedDialogue.Add("The slime took 15 damage from the pot!");
                IJaSTypewriter.Instance.DisplayDialogue();
            }

            if (hitEntity.Identifier is "Player")
            {
                hitEntity.TakeDamage(15);
                IJaSTypewriter.Instance.queuedDialogue.Add("The player took 15 damage from the pot!");
                IJaSTypewriter.Instance.DisplayDialogue();
            }
        }
        else
        {
            Debug.Log($"Womp womp, {collision.gameObject.name}");

            IJaSTypewriter.Instance.queuedDialogue.Add("The falling pot missed!");
            IJaSTypewriter.Instance.DisplayDialogue();

            Destroy(gameObject);
        }

        Destroy(gameObject);

    }
}
