using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CScript : MonoBehaviour
{
    NavMeshAgent agent;
    public Camera cam;
    public LayerMask ground;
    public LayerMask inter;
    public LayerMask openfloor;
    public LayerMask enemy;
    Animator anim;
    public bool running = false;
    public bool attack = false;
    public bool onOpenFloor;
    RaycastHit hit;
    public float interactionRange = 5.0f;
    IObject obj;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("OpenFloor"))
        {
            onOpenFloor = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "OpenFloor")
        {
            onOpenFloor = false;
        }
    }
    public void Attack()
    {
            anim.SetBool("Att", attack);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        anim.SetBool("Run", running);
        if (Input.GetAxis("Fire2") > 0)
        {
            if (!attack)
                attack = true;
            else 
                attack = false;
            Attack();
        }
        if (Input.GetAxis("Fire1") > 0)
        {
            //walk = true;
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            attack = false;
            Attack();
            if (Physics.Raycast(ray, out hit, 100, inter)) //if click popal v inter
            {
                obj = hit.transform.GetComponent<IObject>();
                Vector3 objPos = hit.transform.GetComponent<IObject>().getPoint(transform.position);
                if (onOpenFloor == true)
                    anim.SetInteger("State", (int)obj.getAction());
                double dist = Vector3.Distance(objPos, transform.position);
                if(dist<interactionRange)
                {
                    hit.transform.GetComponent<IObject>().interact();
                }
                else
                {
                    agent.SetDestination(objPos);
                }

                if (onOpenFloor == true)
                {
                    //anim.SetInteger("State", hit.transform.GetComponent<IObject>().getAction());
                   
                }


            } else
            if (Physics.Raycast(ray, out hit, 100, ground)) //if click popal v ground
            {
                agent.SetDestination(hit.point); //point kuda poidet player
            }
        }
        if (agent.velocity.magnitude > 0.1)
        {
            running = true;
        }
        else
        {
            running = false;
        }
    }

    //public void Open()
    //{
    //    hit.transform.GetComponent<IObject>().interact();
    //    anim.SetTrigger("Opening");
    //}
    public void Kick()
    {
        hit.transform.GetComponent<IObject>().interact();
        anim.SetTrigger("Kick");
    }

    public void Interact() //открытие двери
    {
        hit.transform.GetComponent<IObject>().interact();
        anim.SetBool("Att", true);
    }

}

