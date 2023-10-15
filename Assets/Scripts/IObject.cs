using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IObject : MonoBehaviour
{
    public abstract void interact();
    public abstract Actions getAction();
    public abstract Vector3 getPoint(Vector3 pos);
}
