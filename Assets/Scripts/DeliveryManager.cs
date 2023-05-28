using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : BaseCounter
{
    [SerializeField] private RecipeSO recipeListSO;
    private List<KitchenObjectSO> waitingRecipeSOList;
    [SerializeField] private int KitchenObjectCount;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //No object here
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                DeliverCheck();
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

    private void Awake()
    {
        GetReciveFromCar();
    }
    public void GetReciveFromCar()
    {
        waitingRecipeSOList = new List<KitchenObjectSO>();
        for (int i = 0; i < KitchenObjectCount; i++)
        {
            waitingRecipeSOList.Add(recipeListSO.GetRandomKitchenObject());
            Debug.Log(waitingRecipeSOList[i]);
        }

    }
    public void DeliverCheck()
    {
        if (waitingRecipeSOList.Count != 0)
        {
            foreach (var food in waitingRecipeSOList)
            {
                if (food == Player.Instance.GetKitchenObject().kitchenObjectSO)
                {
                    //True Ýtem
                    Debug.Log("True Item");
                    //waitingRecipeSOList.Remove(food);

                }
                else
                {
                    Debug.Log("Wrong Item");
                    Debug.Log(food + " " + Player.Instance.GetKitchenObject().kitchenObjectSO);
                }
            }
        }
    }
}

