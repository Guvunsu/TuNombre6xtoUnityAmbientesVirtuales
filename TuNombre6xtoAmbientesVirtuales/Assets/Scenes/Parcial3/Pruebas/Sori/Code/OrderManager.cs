using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TPDev;
using UnityEngine;
using static UnityEditor.Progress;

public class OrderManager : MonoBehaviour
{
    //Referencias al UI
    [SerializeField] TMP_Text TortillaType0;
    [SerializeField] TMP_Text TortillaType1;
    [SerializeField] TMP_Text TortillaType2;
    [SerializeField] TMP_Text TortillaType3;

    [SerializeField] TMP_Text BeansType0;
    [SerializeField] TMP_Text BeansType1;
    [SerializeField] TMP_Text BeansType2;
    [SerializeField] TMP_Text BeansType3;

    [SerializeField] TMP_Text Ingridients0;
    [SerializeField] TMP_Text Ingridients1;
    [SerializeField] TMP_Text Ingridients2;
    [SerializeField] TMP_Text Ingridients3;

    [SerializeField] MoneyManager moneyManager;

    [SerializeField] List<IngredientWrapper<IngredientType>> orderIngredients = new();
    [SerializeField] TlayudaManager tlayudaManager;

    List<IngredientType> GenerateRandomIngredientList()
    {
        var result = new List<IngredientType>();

        var tortillas = new[] { IngredientType.SmallTortilla, IngredientType.MediumTortilla, IngredientType.BigTortilla };
        var beans = new[] { IngredientType.NormalBeans, IngredientType.Bayos, IngredientType.Puercos };
        var misc = new List<IngredientType> {
            IngredientType.Lechuga, IngredientType.Quesillo, IngredientType.Cebolla,
            IngredientType.Tomate, IngredientType.SalsaRoja, IngredientType.Aguacate
        };

        result.Add(tortillas[UnityEngine.Random.Range(0, tortillas.Length)]);
        result.Add(beans[UnityEngine.Random.Range(0, beans.Length)]);
        result.Add(IngredientType.Carne);

        int additionalCount = UnityEngine.Random.Range(1, misc.Count + 1);

        for (int i = 0; i < misc.Count; i++)
        {
            IngredientType temp = misc[i];
            int randomIndex = UnityEngine.Random.Range(i, misc.Count);
            misc[i] = misc[randomIndex];
            misc[randomIndex] = temp;
        }

        result.AddRange(misc.GetRange(0, additionalCount));

        return result;
    }

    void FillUI(int numOfOrder)
    {
        BeanType bean = BeanType.None;
        TortillaSize torilla = TortillaSize.None;
        List<IngredientType> ingredients = GenerateRandomIngredientList();
        orderIngredients.Add(new() { wrapperIngredients = ingredients });

        switch (numOfOrder)
        {
            case 0:
                {
                    TortillaType0.text = "Tortilla: " + torilla;
                    BeansType0.text = "Beans: " + bean;
                    Ingridients0.text = "Ingredientes: " + string.Join(", ", ingredients);
                    break;
                }
            case 1:
                {
                    TortillaType1.text = "Tortilla: " + torilla;
                    BeansType1.text = "Beans: " + bean;
                    Ingridients1.text = "Ingredientes: " + string.Join(", ", ingredients);
                    break;
                }
            case 2:
                {
                    TortillaType2.text = "Tortilla: " + torilla;
                    BeansType2.text = "Beans: " + bean;
                    Ingridients2.text = "Ingredientes: " + string.Join(", ", ingredients);
                    break;
                }
            case 3:
                {
                   
                    TortillaType1.text = "Tortilla: " + torilla;
                    BeansType1.text = "Beans: " + bean;
                    Ingridients1.text = "Ingredientes: " + string.Join(", ", ingredients);
                   
                    break;
                }
        }
    }

    public void CheckOrdenes()
    {
        if (tlayudaManager.TlayudaIngredientList.Count <= 0f)
        {
            print("Void Tlayuda");
        }

        bool orderFound = false;
        int indexRemoved = -1;

        for (int i = 0; i < orderIngredients.Count; i++)
        {
            if (orderFound = isSameOrder(tlayudaManager.TlayudaIngredientList, orderIngredients[i].wrapperIngredients))
            {
                indexRemoved = i;
                break;
            }
        }

        if (orderFound)
        {
            foreach (IngredientType item in tlayudaManager.TlayudaIngredientList.ToArray())
            {
                if (!isTortilla(item) && !isBeans(item))
                {
                    tlayudaManager.ingredientToAdd = item;
                    tlayudaManager.RemoveIngredient();
                }
            }

            foreach (IngredientType item in tlayudaManager.TlayudaIngredientList.ToArray())
            {
                if (isBeans(item))
                {
                    tlayudaManager.ingredientToAdd = item;
                    tlayudaManager.RemoveIngredient();
                }
            }

            foreach (IngredientType item in tlayudaManager.TlayudaIngredientList.ToArray())
            {
                if (isTortilla(item))
                {
                    tlayudaManager.ingredientToAdd = item;
                    tlayudaManager.RemoveIngredient();
                }
            }

            orderFound = false;
            moneyManager.GiveMoney(orderIngredients[indexRemoved].price);
            orderIngredients.RemoveAt(indexRemoved);
        }
        else
        {
            print("No Order found");
            moneyManager.LoseMoney(orderIngredients[UnityEngine.Random.Range(0, orderIngredients.Count)].price / 2);
        }
    }

    bool isBeans(IngredientType item)
    {
        return item == IngredientType.NormalBeans ||
               item == IngredientType.Bayos ||
               item == IngredientType.Puercos;
    }

    bool isTortilla(IngredientType item)
    {
        return item == IngredientType.SmallTortilla ||
               item == IngredientType.MediumTortilla ||
               item == IngredientType.BigTortilla;
    }

    bool isSameOrder(List<IngredientType> _tlayudaMade, List<IngredientType> _tlayudaOrdered)
    {
        if (_tlayudaMade.Count != _tlayudaOrdered.Count) return false;

        var SetOfTlayudaMade = new HashSet<IngredientType>(_tlayudaMade);
        var SetOfTlayudaOrdered = new HashSet<IngredientType>(_tlayudaOrdered);
        return SetOfTlayudaMade.SetEquals(SetOfTlayudaOrdered);
    }

    IEnumerator CreateOrder()
    {
        int numOfOrder = 0;

        //TPGG: cambie esto para que funcione con mi logica
        FillUI(numOfOrder);
        numOfOrder++;

        while (true)
        {
            yield return new WaitForSeconds(90);

            FillUI(numOfOrder);
            numOfOrder++;
        }
    }

    public void DebugCreateOrder()
    {
        StartCoroutine(CreateOrder());
    }

    private void Start()
    {
        //StartCoroutine(CreateOrder());

        StartCoroutine(CreateOrder());
    }
}

[Serializable]
public class IngredientWrapper<T>
{
    [SerializeField] public int price = 1000;
    [SerializeField] public List<T> wrapperIngredients = new();
}