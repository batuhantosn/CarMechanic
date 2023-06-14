using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
	public int Cost;
	public GameObject LockPicture;

	[SerializeField] private KitchenObjectSO kitchenObjectSO;



	public override void Interact(Player player)
	{
		if (!HasMechanicObject())
		{
			//No object here
			if (player.HasMechanicObject())
			{
				//Player is carrying something
				player.GetMechanicObject().SetKitchenObjectParent(this);
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

}
