using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMechanicObjectParent
{

    public Transform GetMechanicObjectFollowTransform();
    public void SetMechanicObject(MechanicObject mechanicObject);
    public MechanicObject GetMechanicObject();
    public void ClearMechanicObject();
    public bool HasMechanicObject();
}
