using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorScript : IObject
{
    public Animator anim; 
    public bool isOpen = false;
    public LayerMask inter;
    public LayerMask ground;
    public float interactionRange = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
       // anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        // interact();
        
    }
    public override void interact()
    {
        if (anim.GetInteger("State") > 0)
            anim.SetInteger("State", 0);
        else
            anim.SetInteger("State", 1);
    }
    RaycastHit hit;
    public Transform point1;
    public Transform point2;
    public override Vector3 getPoint(Vector3 pos)
    {
        var dist1 = Vector3.Distance(pos, point1.position);
        var dist2 = Vector3.Distance(pos, point2.position);
        if (dist1 < dist2)
            return point1.position;
        else
            return point2.position;
    }
    public void Open()
    {
        
        hit.transform.GetComponent<IObject>().interact();
        anim.SetInteger("State", 0);
    }
    public override Actions getAction()
    {
        return Actions.open; 
    }
}
