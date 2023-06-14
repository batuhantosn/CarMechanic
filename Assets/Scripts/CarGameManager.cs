using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CarGameManager : Singleton<CarGameManager>
{

	public GameObject[] LiftGameObjects;
	public GameObject CarPrefab;

	public int Money;
	public TextMeshProUGUI MoneyTextUI;


	// Start is called before the first frame update
	void Start()
	{
		Money = PlayerPrefs.GetInt("Money", 0);

		UpdateMoneyText();
	}

	public bool SpendMoney(int amount)
	{
		int curMoney = Money;
		curMoney -= amount;
		if (curMoney < 0) return false;
		else
		{
			Money -= amount;
			PlayerPrefs.SetInt("Money", Money);
			UpdateMoneyText();
			return true;
		}
	}

	public void GainMoney(int amount)
	{
		Money += amount;
		PlayerPrefs.SetInt("Money", Money);
		UpdateMoneyText();
	}

	private void UpdateMoneyText()
	{
		MoneyTextUI.text = ((int)Money).ToString();
	}


	// Update is called once per frame
	void Update()
	{

	}
}
