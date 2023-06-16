using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using ArcadeVP;
using DG.Tweening;

public enum GameMode
{
	Race,
	Idle
}
public class DriveableCarManager : Singleton<DriveableCarManager>
{
	public CinemachineVirtualCamera IdleCam;
	public CinemachineVirtualCamera RaceCam;
	public GameObject[] DriveableCars;
	public GameObject CurrentDriveableCar;
	public ArcadeVehicleController CurrentCarArcadeVC;

	public GameObject CarStartPoint;
	public GameMode CurrentMode;


	// Start is called before the first frame update
	void Start()
	{
		CurrentMode = GameMode.Idle;
		SwitchGameMode(CurrentMode);
	}


	public void SwitchGameMode(GameMode mode)
	{
		if (GameMode.Idle == mode)
		{
			IdleCam.Priority = 11;
			RaceCam.Priority = 10;
			CurrentMode = GameMode.Idle;

			StopDriving();
		}
		else if (GameMode.Race == mode)
		{
			IdleCam.Priority = 10;
			RaceCam.Priority = 11;
			CurrentMode = GameMode.Race;

			StartDriving();
		}
	}

	public void ChangeCarSkin(int index)
	{
		for (int i = 0; i < DriveableCars.Length; i++)
		{
			if (i != index)
			{
				DriveableCars[i].SetActive(false);
			}
			else
			{
				CurrentDriveableCar = DriveableCars[i];
				CurrentDriveableCar.SetActive(true);
				CurrentCarArcadeVC = CurrentDriveableCar.GetComponent<ArcadeVehicleController>();

				PrepareCarToRide();
			}

		}
	}

	public void PrepareCarToRide()
	{
		if (CurrentDriveableCar != null)
		{
			CurrentDriveableCar.transform.position = CarStartPoint.transform.position;
			print("Ready to drive.");
		}
	}

	public void StartDriving()
	{
		CurrentCarArcadeVC.enabled = true;
	}

	public void StopDriving()
	{
		CurrentCarArcadeVC.enabled = false;
		PrepareCarToRide();
	}
}
