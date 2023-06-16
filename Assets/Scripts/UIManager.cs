using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
	public Button MarketButton;
	public Button IdleButton;
	public Button ComingSoonButton;

	public GameObject MarketPanel;
	public Button CarButton1, CarButton2, CarButton3, CarButton4;
	public int CarCost1, CarCost2, CarCost3, CarCost4;
	public bool isCarUnlocked1, isCarUnlocked2, isCarUnlocked3, isCarUnlocked4;



	private void Start()
	{
		IdleButton.gameObject.SetActive(false);

		CarButton1.onClick.AddListener(() => { BuyCar(0, CarCost1); });
		CarButton2.onClick.AddListener(() => { BuyCar(1, CarCost2); });
		CarButton3.onClick.AddListener(() => { BuyCar(2, CarCost3); });
		CarButton4.onClick.AddListener(() => { BuyCar(3, CarCost4); });

		CheckIsUnlockeds();

		CheckMarketButtonAvailability();
	}

	private void CheckIsUnlockeds()
	{
		isCarUnlocked1 = PlayerPrefs.GetInt("CarUnlocked1", 0) != 0;
		isCarUnlocked2 = PlayerPrefs.GetInt("CarUnlocked2", 0) != 0;
		isCarUnlocked3 = PlayerPrefs.GetInt("CarUnlocked3", 0) != 0;
		isCarUnlocked4 = PlayerPrefs.GetInt("CarUnlocked4", 0) != 0;
	}

	private void CheckMarketButtonAvailability()
	{
		CarButton1.interactable = isCarUnlocked1 || CarGameManager.Instance.Money >= CarCost1;
		CarButton2.interactable = isCarUnlocked2 || CarGameManager.Instance.Money >= CarCost2;
		CarButton3.interactable = isCarUnlocked3 || CarGameManager.Instance.Money >= CarCost3;
		CarButton4.interactable = isCarUnlocked4 || CarGameManager.Instance.Money >= CarCost4;

		if (isCarUnlocked1) CarButton1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Play";
		if (isCarUnlocked2) CarButton2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Play";
		if (isCarUnlocked3) CarButton3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Play";
		if (isCarUnlocked4) CarButton4.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Play";
	}

	public void SwitchToRaceMode()
	{
		MarketButton.gameObject.SetActive(false);
		ComingSoonButton.gameObject.SetActive(false);

		DriveableCarManager.Instance.SwitchGameMode(GameMode.Race);

		DOVirtual.DelayedCall(1f, () => { IdleButton.gameObject.SetActive(true); });
	}

	public void SwitchToIdleMode()
	{
		IdleButton.gameObject.SetActive(false);
		ComingSoonButton.gameObject.SetActive(true);

		DriveableCarManager.Instance.SwitchGameMode(GameMode.Idle);

		DOVirtual.DelayedCall(1f, () => { MarketButton.gameObject.SetActive(true); });
	}

	public void BuyCar(int index, int cost)
	{
		if (PlayerPrefs.GetInt("CarUnlocked" + (index + 1).ToString()) == 1)
		{
			DriveableCarManager.Instance.ChangeCarSkin(index);
			DriveableCarManager.Instance.SwitchGameMode(GameMode.Race);
			DOVirtual.DelayedCall(1f, () => { IdleButton.gameObject.SetActive(true); });
			DOVirtual.DelayedCall(0f, () => { MarketButton.gameObject.SetActive(false); });
			DOVirtual.DelayedCall(0f, () => { ComingSoonButton.gameObject.SetActive(false); });
			DOVirtual.DelayedCall(0f, () => { MarketPanel.SetActive(false); });

			print("Car " + index + " is already unlocked");
		}
		else if (CarGameManager.Instance.SpendMoney(cost))
		{
			PlayerPrefs.SetInt("CarUnlocked" + (index + 1).ToString(), 1);

			CheckIsUnlockeds();
			CheckMarketButtonAvailability();

			DriveableCarManager.Instance.ChangeCarSkin(index);
			DriveableCarManager.Instance.SwitchGameMode(GameMode.Race);
			DOVirtual.DelayedCall(1f, () => { IdleButton.gameObject.SetActive(true); });
			DOVirtual.DelayedCall(0f, () => { MarketButton.gameObject.SetActive(false); });
			DOVirtual.DelayedCall(0f, () => { ComingSoonButton.gameObject.SetActive(false); });
			DOVirtual.DelayedCall(0f, () => { MarketPanel.SetActive(false); });
			print("Car " + index + " is bought");
		}
	}

	public void MarketInteraction()
	{
		CheckMarketButtonAvailability();
		MarketPanel.SetActive(!MarketPanel.activeSelf);
	}

	public void CloseMarket()
	{
		MarketPanel.SetActive(false);
	}
}
