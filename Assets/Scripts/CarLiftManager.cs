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
	private void Start()
	{
		if (CurrentCar == null)
		{
			CurrentCar = Instantiate(CarGameManager.Instance.CarPrefab, SpawnPoint.transform.position, CarGameManager.Instance.CarPrefab.transform.rotation);
		}
		if (!IsLocked)
			SpawnCar();
	}

	public void UnlockLift()
	{
		if (CarGameManager.Instance.SpendMoney(Cost))
		{
			LockObject.transform.DOScale(Vector3.zero * 0.001f, 0.5f).OnComplete(() =>
			{
				LockObject.SetActive(false);
				IsLocked = false;
			});
		}
	}

	public void RemoveCar()
	{
		LiftDown();
	}

	public void SpawnCar()
	{
		CurrentCar.transform.position = SpawnPoint.transform.position;
		CurrentCar.transform.localEulerAngles = Vector3.zero;
		CurrentCar.transform.DOMove(transform.position + Vector3.forward * 2f, 1f)
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
			removedCar.transform.DOLookAt(GetOutPoints[2].transform.position, 0.3f);
			removedCar.transform.DOMove(GetOutPoints[1].transform.position, 1f)
			.SetEase(Ease.Linear)
			.OnComplete(() =>
			{
				removedCar.transform.DOMove(GetOutPoints[2].transform.position, 1f)
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
