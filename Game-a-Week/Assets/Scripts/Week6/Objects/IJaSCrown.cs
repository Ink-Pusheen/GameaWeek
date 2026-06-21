using System.Collections;
using System.Security.Principal;
using UnityEngine;

public class IJaSCrown : MonoBehaviour
{
    private IJaSHealth curInteractionTarget;

    [SerializeField] Sprite crownedSlime; //Change the slime sprite

    private void OnCollisionEnter(Collision collision)
    {
        IJaSHealth hitEntity = collision.gameObject.GetComponent<IJaSHealth>();

        if (hitEntity is not null)
        {
            curInteractionTarget = hitEntity;

            //Debug.Log($"Do stuff w/ {hitEntity.Identifier}"); //Testing

            if (hitEntity.Identifier is "Slime" && !hitEntity.bHasCrown)
            {
                GetComponent<MeshRenderer>().enabled = false; //Hide crown and remove interaction
                GetComponent<Collider>().enabled = false;

                hitEntity.GetComponent<SpriteRenderer>().sprite = crownedSlime;

                StartCoroutine("SlimeInteraction");

                hitEntity.MultiplyHealth(3);

                hitEntity.bHasCrown = true;
            }
            else if(hitEntity.Identifier is "Slime" && hitEntity.bHasCrown)
            {
                hitEntity.Heal(999);

                //Play heal sound [Future me]

                IJaSTypewriter.Instance.queuedDialogue.Add("The Slime got full healed from the crown!");
                IJaSTypewriter.Instance.DisplayDialogue();

                Destroy(gameObject);
            }

            if(hitEntity.Identifier is "Player")
            {
                hitEntity.TakeDamage(15);

                IJaSTypewriter.Instance.queuedDialogue.Add("The player took 15 damage from the bonk of the crown!");
                IJaSTypewriter.Instance.DisplayDialogue();

                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log($"Womp womp, {collision.gameObject.name}");

            IJaSTypewriter.Instance.queuedDialogue.Add("The fallng crown missed!");
            IJaSTypewriter.Instance.DisplayDialogue();

            Destroy(gameObject);
        }

        
    }

    IEnumerator SlimeInteraction()
    {
        curInteractionTarget.gameObject.transform.parent.transform.localScale = new Vector3(2, 2, 2);

        yield return new WaitForSeconds(0.15f);

        curInteractionTarget.gameObject.transform.parent.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);

        yield return new WaitForSeconds(0.15f);

        curInteractionTarget.gameObject.transform.parent.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);

        yield return new WaitForSeconds(0.15f);

        curInteractionTarget.gameObject.transform.parent.transform.localScale = new Vector3(4f, 4f, 4f);

        yield return new WaitForSeconds(0.15f);

        curInteractionTarget.gameObject.transform.parent.transform.localScale = new Vector3(5f, 5f, 5f);

        IJaSTypewriter.Instance.queuedDialogue.Add("The Slime grew from the crown!");
        IJaSTypewriter.Instance.DisplayDialogue();

        Destroy(gameObject);
    }
}
