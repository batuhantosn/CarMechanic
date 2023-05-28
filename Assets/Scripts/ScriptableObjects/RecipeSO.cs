using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectSOList;

    public KitchenObjectSO GetRandomKitchenObject()
    {
        return kitchenObjectSOList[Random.Range(0,kitchenObjectSOList.Count)];
    }
}
