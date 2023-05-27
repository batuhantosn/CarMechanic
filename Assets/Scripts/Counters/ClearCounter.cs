using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter: BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;



    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //No object here
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player has nothing
            }
            
        }
        else
        {
            //There is a object 
            if (player.HasKitchenObject())
            {
                //Player is carrying smthng
            }
            else
            {
                //PLayer is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
