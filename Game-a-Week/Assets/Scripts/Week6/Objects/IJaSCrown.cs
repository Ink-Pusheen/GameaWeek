using System.Collections;
using System.Security.Principal;
using UnityEngine;

public class IJaSCrown : MonoBehaviour
{
    private IJaSHealth curInteractionTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

                StartCoroutine("SlimeInteraction");

                hitEntity.MultiplyHealth(3);

                hitEntity.bHasCrown = true;
            }
            else if(hitEntity.Identifier is "Slime" && hitEntity.bHasCrown)
            {
                hitEntity.Heal(999);

                //Play heal sound [Future me]

                Destroy(gameObject);
            }

            if(hitEntity.Identifier is "Player")
            {
                hitEntity.TakeDamage(15);

                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log($"Womp womp, {collision.gameObject.name}");

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

        Destroy(gameObject);
    }
}
