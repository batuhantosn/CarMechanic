using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicObject : MonoBehaviour
{
    public KitchenObjectSO mechanicObjectSO;
    

    private IMechanicObjectParent mechanicObjectParent;
    public KitchenObjectSO GetMechanicObjectSO()
    {
        return mechanicObjectSO;
    }

    public void SetKitchenObjectParent(IMechanicObjectParent mechanicObjectParent)
    {
        if (this.mechanicObjectParent != null)
        {
            this.mechanicObjectParent.ClearMechanicObject();
        }
        this.mechanicObjectParent = mechanicObjectParent;

        if (mechanicObjectParent.HasMechanicObject())
        {
            Debug.LogError("Counter already has an Object");
        }
        
        mechanicObjectParent.SetMechanicObject(this);

        transform.parent = mechanicObjectParent.GetMechanicObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }


    public IMechanicObjectParent GetMechanicObjectParent()
    {
        return mechanicObjectParent;
    }
    public void DestroySelf()
    {
        mechanicObjectParent.ClearMechanicObject();
        Destroy(gameObject);
    }
}
