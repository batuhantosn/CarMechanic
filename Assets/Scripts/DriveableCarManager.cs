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
	public GameObject DriveableCar;
	public RideableCar CurCarData;
	public GameObject CarStartPoint;
	public GameMode CurrentMode;
	public ArcadeVehicleController CurrentCarArcadeVC;

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
		for (int i = 0; i < CurCarData.CarSkins.Length; i++)
		{
			if (i != index) CurCarData.CarSkins[i].SetActive(false);
			else CurCarData.CarSkins[i].SetActive(true);
		}
	}

	public void PrepareCarToRide()
	{
		DriveableCar.transform.position = CarStartPoint.transform.position;
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
