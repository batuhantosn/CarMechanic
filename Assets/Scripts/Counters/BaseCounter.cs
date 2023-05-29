using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IMechanicObjectParent{

    [SerializeField] private Transform counterTopPoint;

    protected MechanicObject mechanicObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interacrt();");
    }

    public Transform GetMechanicObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetMechanicObject(MechanicObject kitchenObject)
    {
        this.mechanicObject = kitchenObject;
    }
    public MechanicObject GetMechanicObject()
    {
        return mechanicObject;
    }
    public void ClearMechanicObject()
    {
        mechanicObject = null;
    }
    public bool HasMechanicObject()
    {
        return mechanicObject != null;
    }
}
