using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManager : BaseCounter
{
    public List<GameObject> deliveryItemsList;

    [SerializeField] private RecipeSO recipeListSO;
    private List<KitchenObjectSO> waitingRecipeSOList;
    [SerializeField] private int KitchenObjectCount;

    public Sprite checksprite;

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
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            deliveryItemsList[i].GetComponent<SpriteRenderer>().sprite = waitingRecipeSOList[i].sprite;
        }
    }
        public void DeliverCheck()
        {
            if (waitingRecipeSOList.Count != 0)
            {
                foreach (var food in deliveryItemsList)
                {
                    if (food.GetComponent<SpriteRenderer>().sprite == Player.Instance.GetKitchenObject().kitchenObjectSO.sprite)
                    {
                        //True Item
                        Debug.Log("True Item");
                        food.GetComponent<SpriteRenderer>().sprite = checksprite;
                        
                }
                    else
                    {
                        //Wrong Item
                        Debug.Log("Wrong Item");
                        Debug.Log(food + " " + Player.Instance.GetKitchenObject().kitchenObjectSO);
                    }
                }
            }
        }
    }


