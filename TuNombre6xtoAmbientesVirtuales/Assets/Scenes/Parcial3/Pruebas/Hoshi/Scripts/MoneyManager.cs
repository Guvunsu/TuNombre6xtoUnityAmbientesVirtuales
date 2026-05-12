using UnityEngine;
using TMPro;
using System;

public class MoneyManager : MonoBehaviour
{

    //TPGG: cambie esto para evitar errores
    //[SerializeField] public static MoneyManager moneyManagerInstance;
    public static MoneyManager moneyManagerInstance;

    [SerializeField] float currentMoney;
    [SerializeField] TextMeshProUGUI currencyText;


    private void Awake()
    {

        //TPGG: cambie esto para evitar errores
        /*
        if (moneyManagerInstance != null && moneyManagerInstance != this) {
            Destroy(moneyManagerInstance);
        } else {
            moneyManagerInstance = this;
        }
        */

        if (moneyManagerInstance != null && moneyManagerInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            moneyManagerInstance = this;
        }
    }

    public void GiveMoney(float gains)
    {
        currentMoney += gains;
        currencyText.text = currentMoney.ToString();
    }

    public void LoseMoney(float liability)
    {

        //TPGG: cambie esto para evitar errores 
        /*
        if (currentMoney > 0) {
            currentMoney -= liability;
            currencyText.text = currentMoney.ToString();
        }
        */

        currentMoney = Mathf.Max(0, currentMoney - liability);
        currencyText.text = currentMoney.ToString();
    }
}