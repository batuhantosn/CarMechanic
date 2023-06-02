using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManager : BaseCounter
{
    public List<GameObject> deliveryItemsList;

    [SerializeField] private RecipeSO recipeListSO;
    private List<KitchenObjectSO> waitingRecipeSOList;
    [SerializeField] private int KitchenObjectCount;

    public Sprite checksprite;
    bool deliverCheckBool = false;

    private string eqweqw;

    public GameObject deliveryCanvas;

    public override void Interact(Player player)
    {
        if (!HasMechanicObject())
        {
            //No object here
            if (player.HasMechanicObject())
            {
                //Player is carrying something
                DeliverCheck();
                player.GetMechanicObject().SetKitchenObjectParent(this);
                if (deliverCheckBool)
                {
                    this.mechanicObject.DestroySelf();
                }
                

            }
            else
            {
                //Player has nothing
            }

        }
        else
        {
            //There is a object 
            if (player.HasMechanicObject())
            {
                //Player is carrying smthng
            }
            else
            {
                //PLayer is not carrying anything
                GetMechanicObject().SetKitchenObjectParent(player);
            }
        }

    }

    private void Awake()
    {
        GetReciveFromCar();
        SetDeliveryItems();
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
    public void SetDeliveryItems()
    {
        foreach (var item in deliveryCanvas.GetComponentsInChildren<MechanicObject>())
        {
            deliveryItemsList.Add(item.gameObject);
        }

        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            
            deliveryItemsList[i].GetComponent<SpriteRenderer>().sprite = waitingRecipeSOList[i].sprite;
            deliveryItemsList[i].GetComponent<MechanicObject>().SetKitchenObject(waitingRecipeSOList[i]);
        }
    }
    public bool DeliverCheck()
    {
        
        if (waitingRecipeSOList.Count != 0)
        {
            for (int i = 0; i < deliveryItemsList.Count; i++)
            {
                if (deliveryItemsList[i].GetComponent<MechanicObject>().GetMechanicObjectSO() == Player.Instance.GetMechanicObject().mechanicObjectSO)
                {
                    //True Item

                    Debug.Log("True Item");
                    deliveryItemsList[i].GetComponent<SpriteRenderer>().sprite = checksprite;

                    deliveryItemsList.RemoveAt(i);

                    if (deliveryItemsList.Count==0)
                    {
                        GetReciveFromCar();
                        SetDeliveryItems();
                        //Give Gold
                    }
                    return deliverCheckBool = true;
                }
                else
                {
                    //Wrong Item
                    Debug.Log("Wrong Item");
                }
            }
        }
        Debug.LogError("waitingRecipeSOList.Count: " + waitingRecipeSOList.Count);
        return deliverCheckBool = false;
    } 
}


