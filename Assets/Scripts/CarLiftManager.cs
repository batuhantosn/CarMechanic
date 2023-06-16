using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarLiftManager : MonoBehaviour
{
	public GameObject LiftBands;
	public GameObject SpawnPoint;
	public GameObject[] GetOutPoints;
	public GameObject CurrentCar;


	public GameObject LockObject;
	public bool IsLocked;
	public int Cost;

	public int LiftID;

	public DeliveryManager CurrentDeliveryManager;
	private void Start()
	{
		if (CurrentCar == null)
		{
			CurrentCar = Instantiate(CarGameManager.Instance.CarPrefab, SpawnPoint.transform.position, CarGameManager.Instance.CarPrefab.transform.rotation);
		}

		if (PlayerPrefs.HasKey(LiftID + "_isLocked")) // 0 is false, 1 is true
		{
			if (PlayerPrefs.GetInt(LiftID + "_isLocked") == 1)
				IsLocked = true;
			else
			{
				IsLocked = false;
				UnlockLift();
			}
		}
		else
		{
			PlayerPrefs.SetInt(LiftID + "_isLocked", 1);
			IsLocked = true;
		}

		CurrentDeliveryManager.InitilizeDeliveryManager();

		if (!IsLocked)
			SpawnCar();
	}

	public virtual void Interact(Player player)
	{
		Debug.LogError("Interacted with Lift.");
	}

	public void UnlockLift()
	{
		if (LockObject != null)
		{
			LockObject.transform.DOScale(Vector3.zero * 0.001f, 0.5f).OnComplete(() =>
			{
				LockObject.SetActive(false);
				IsLocked = false;
				PlayerPrefs.SetInt(LiftID + "_isLocked", 0);
			});
		}
		else
		{
			IsLocked = false;
			PlayerPrefs.SetInt(LiftID + "_isLocked", 0);
		}
		SpawnCar();
	}

	public void RemoveCar()
	{
		LiftDown();
	}

	public void SpawnCar()
	{
		print("Car spawned.");
		CurrentCar.transform.position = SpawnPoint.transform.position;
		CurrentCar.transform.localEulerAngles = Vector3.zero;
		CurrentCar.transform.DOMove(transform.position, 1f)
		.OnComplete(() =>
		{
			LiftUp(1f);
			CurrentCar.transform.DOMoveY(2f, 1f);
		});
	}
	public void LiftUp(float time)
	{
		LiftBands.transform.DOLocalMoveY(2f, time);
	}

	// bool canLoop = true;
	// private void LoopMovement()
	// {
	// 	if (LiftBands == null) return;

	// 	LiftBands.transform.DOMove(Vector3.forward * 10f, 1)
	// 	.OnComplete(() =>
	// 	{

	// 		LiftBands.transform.DOMove(Vector3.back * 10f, 1f)
	// 		.OnComplete(() =>
	// 		{
	// 			LoopMovement();
	// 		});
	// 	});
	// }

	public void LiftDown()
	{
		CurrentCar.transform.DOMoveY(0f, 1f);
		LiftBands.transform.DOLocalMoveY(0f, 1f)
		.OnComplete(() =>
		{
			ReleaseCar(CurrentCar);
		});
		CarGameManager.Instance.GainMoney(10);
	}

	public void ReleaseCar(GameObject removedCar)
	{
		removedCar.transform.DOMove(GetOutPoints[0].transform.position, 1f)
		.SetEase(Ease.Linear)
		.OnComplete(() =>
		{
			removedCar.transform.DOLookAt(GetOutPoints[2].transform.position, 0.6f);
			removedCar.transform.DOMove(GetOutPoints[1].transform.position, 1f)
			.SetEase(Ease.Linear)
			.OnComplete(() =>
			{
				removedCar.transform.DOMove(GetOutPoints[2].transform.position, 3f)
				.SetEase(Ease.Linear)
				.OnComplete(() =>
				{
					//Destroy(removedCar);

					SpawnCar();
				});
			});
		});
	}
}
