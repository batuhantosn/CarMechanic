using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
	public Button RaceButton;
	public Button IdleButton;

	private void Start()
	{
		IdleButton.gameObject.SetActive(false);
	}
	public void SwitchToRaceMode()
	{
		RaceButton.gameObject.SetActive(false);
		DriveableCarManager.Instance.SwitchGameMode(GameMode.Race);

		DOVirtual.DelayedCall(1f, () => { IdleButton.gameObject.SetActive(true); });
	}

	public void SwitchToIdleMode()
	{
		IdleButton.gameObject.SetActive(false);
		DriveableCarManager.Instance.SwitchGameMode(GameMode.Idle);

		DOVirtual.DelayedCall(1f, () => { RaceButton.gameObject.SetActive(true); });
	}
}
