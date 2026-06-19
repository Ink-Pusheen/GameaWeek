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
            }

            if (hitEntity.Identifier is "Player")
            {
                hitEntity.TakeDamage(15);
            }
        }
        else
        {
            Debug.Log($"Womp womp, {collision.gameObject.name}");

            Destroy(gameObject);
        }

        Destroy(gameObject);

    }
}
