using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TPDev {
    [Serializable] public enum BeanType { None, Normal, Bayos, Puercos }
    [Serializable] public enum TortillaSize { None, Small, Medium, Big }

    [Serializable]
    public enum ToppingType {
        Carne,
        Lechuga,
        Quesillo,
        Cebolla,
        Tomate,
        SalsaRoja,
        Aguacate
    }

    [Serializable]
    public enum IngredientType {
        None,
        SmallTortilla,
        MediumTortilla,
        BigTortilla,
        NormalBeans,
        Bayos,
        Puercos,
        Carne,
        Lechuga,
        Quesillo,
        Cebolla,
        Tomate,
        SalsaRoja,
        Aguacate
    }

    public class TlayudaManager : MonoBehaviour {
        [SerializeField] private List<IngredientType> tlayudaIngredientList;
        public List<IngredientType> TlayudaIngredientList { get => tlayudaIngredientList; private set => tlayudaIngredientList = value; }
        [SerializeField] private BeanType currentBeans = BeanType.None;
        [SerializeField] private TortillaSize currentTortilla = TortillaSize.None;

        [SerializeField] public Vector3 smallSize = new Vector3(1, 1, 1);
        [SerializeField] public Vector3 mediumSize = new Vector3(1.5f, 1, 1.5f);
        [SerializeField] public Vector3 bigSize = new Vector3(2, 1, 2);
        [SerializeField] private GameObject tortillaObj;

        [SerializeField] private GameObject beansNormalObj;
        [SerializeField] private GameObject beansBayosObj;
        [SerializeField] private GameObject beansPuercosObj;

        public bool carne;
        public bool lechuga;
        public bool quesillo;
        public bool cebolla;
        public bool tomate;
        public bool salsaRoja;
        public bool aguacate;

        [SerializeField] private GameObject carneObj;
        [SerializeField] private GameObject lechugaObj;
        [SerializeField] private GameObject quesilloObj;
        [SerializeField] private GameObject cebollaObj;
        [SerializeField] private GameObject tomateObj;
        [SerializeField] private GameObject salsaRojaObj;
        [SerializeField] private GameObject aguacateObj;

        [SerializeField] public IngredientType ingredientToAdd;

        [SerializeField] private bool addIngredientNow;
        [SerializeField] private bool removeIngredientNow;
        //[SerializeField] private bool applyTortillaNow;
        //[SerializeField] private bool applyBaseNow;

        void Update() {
            //if (applyTortillaNow) {
            //    applyTortillaNow = false;
            //    ChangeTortilla();
            //}

            //if (applyBaseNow) {
            //    applyBaseNow = false;
            //    ChangeBase();
            //}

            if (addIngredientNow) {
                addIngredientNow = false;
                AddIngredient();
            }

            if (removeIngredientNow) {
                removeIngredientNow = false;
                RemoveIngredient();
            }
        }

        public void ChangeTortilla() {
            if (currentTortilla == TortillaSize.None) {
                //SetActiveSafe(tortillaObj, false);
                return;
            }

            //SetActiveSafe(tortillaObj, true);

            switch (currentTortilla) {
                case TortillaSize.Small:
                    transform.localScale = smallSize;
                    if (!tlayudaIngredientList.Contains(IngredientType.SmallTortilla)) {
                        tlayudaIngredientList.Add(IngredientType.SmallTortilla);
                        tlayudaIngredientList.Remove(IngredientType.MediumTortilla);
                        tlayudaIngredientList.Remove(IngredientType.BigTortilla);
                    }
                    break;

                case TortillaSize.Medium:
                    transform.localScale = mediumSize;
                    if (!tlayudaIngredientList.Contains(IngredientType.MediumTortilla)) {
                        tlayudaIngredientList.Add(IngredientType.MediumTortilla);
                        tlayudaIngredientList.Remove(IngredientType.SmallTortilla);
                        tlayudaIngredientList.Remove(IngredientType.BigTortilla);
                    }
                    break;

                case TortillaSize.Big:
                    transform.localScale = bigSize;
                    if (!tlayudaIngredientList.Contains(IngredientType.BigTortilla)) {
                        tlayudaIngredientList.Add(IngredientType.BigTortilla);
                        tlayudaIngredientList.Remove(IngredientType.MediumTortilla);
                        tlayudaIngredientList.Remove(IngredientType.SmallTortilla);
                    }
                    break;
            }
        }

        public void ChangeBase() {
            if (currentTortilla == TortillaSize.None)
                return;

            //SetActiveSafe(beansNormalObj, false);
            //SetActiveSafe(beansBayosObj, false);
            //SetActiveSafe(beansPuercosObj, false);

            switch (currentBeans) {
                case BeanType.Normal:
                    //SetActiveSafe(beansNormalObj, true);
                    if (!tlayudaIngredientList.Contains(IngredientType.NormalBeans)) {
                        tlayudaIngredientList.Add(IngredientType.NormalBeans);
                    }
                    break;

                case BeanType.Bayos:
                    //SetActiveSafe(beansBayosObj, true);
                    if (!tlayudaIngredientList.Contains(IngredientType.Bayos)) {
                        tlayudaIngredientList.Add(IngredientType.Bayos);
                    }
                    break;

                case BeanType.Puercos:
                    //SetActiveSafe(beansPuercosObj, true);
                    if (!tlayudaIngredientList.Contains(IngredientType.Puercos)) {
                        tlayudaIngredientList.Add(IngredientType.Puercos);
                    }
                    break;
            }
        }

        public void AddIngredient() {
            switch (ingredientToAdd) {
                case IngredientType.SmallTortilla:
                    transform.localScale = smallSize;
                    if (!tlayudaIngredientList.Contains(IngredientType.SmallTortilla)) {
                        tlayudaIngredientList.Add(IngredientType.SmallTortilla);
                        currentTortilla = TortillaSize.Small;
                        tlayudaIngredientList.Remove(IngredientType.MediumTortilla);
                        tlayudaIngredientList.Remove(IngredientType.BigTortilla);
                    }
                    break;
                case IngredientType.MediumTortilla:
                    transform.localScale = mediumSize;
                    if (!tlayudaIngredientList.Contains(IngredientType.MediumTortilla)) {
                        tlayudaIngredientList.Add(IngredientType.MediumTortilla);
                        currentTortilla = TortillaSize.Medium;
                        tlayudaIngredientList.Remove(IngredientType.SmallTortilla);
                        tlayudaIngredientList.Remove(IngredientType.BigTortilla);
                    }
                    break;
                case IngredientType.BigTortilla:
                    transform.localScale = bigSize;
                    if (!tlayudaIngredientList.Contains(IngredientType.BigTortilla)) {
                        tlayudaIngredientList.Add(IngredientType.BigTortilla);
                        currentTortilla = TortillaSize.Big;
                        tlayudaIngredientList.Remove(IngredientType.MediumTortilla);
                        tlayudaIngredientList.Remove(IngredientType.SmallTortilla);
                    }
                    break;

                default:
                    Debug.LogWarning("Null operation: Must select Tortilla");
                    break;
            }

            if (currentTortilla == TortillaSize.None) return;

            switch (ingredientToAdd) {
                case IngredientType.NormalBeans:
                    //SetActiveSafe(beansNormalObj, true);
                    if (!tlayudaIngredientList.Contains(IngredientType.NormalBeans)) {
                        tlayudaIngredientList.Add(IngredientType.NormalBeans);
                        currentBeans = BeanType.Normal;
                        tlayudaIngredientList.Remove(IngredientType.Bayos);
                        tlayudaIngredientList.Remove(IngredientType.Puercos);
                    }
                    break;

                case IngredientType.Bayos:
                    //SetActiveSafe(beansBayosObj, true);
                    if (!tlayudaIngredientList.Contains(IngredientType.Bayos)) {
                        tlayudaIngredientList.Add(IngredientType.Bayos);
                        currentBeans = BeanType.Bayos;
                        tlayudaIngredientList.Remove(IngredientType.NormalBeans);
                        tlayudaIngredientList.Remove(IngredientType.Puercos);
                    }
                    break;

                case IngredientType.Puercos:
                    //SetActiveSafe(beansPuercosObj, true);
                    if (!tlayudaIngredientList.Contains(IngredientType.Puercos)) {
                        tlayudaIngredientList.Add(IngredientType.Puercos);
                        currentBeans = BeanType.Puercos;
                        tlayudaIngredientList.Remove(IngredientType.NormalBeans);
                        tlayudaIngredientList.Remove(IngredientType.Bayos);
                    }
                    break;
                default:
                    Debug.LogWarning("Null operation: Must select Base");
                    break;
            }

            if (currentTortilla == TortillaSize.None || currentBeans == BeanType.None) return;

            switch (ingredientToAdd) {
                case IngredientType.Carne:
                    carne = true;
                    //SetActiveSafe(carneObj, true);
                    if (!tlayudaIngredientList.Contains(IngredientType.Carne)) {
                        tlayudaIngredientList.Add(IngredientType.Carne);
                    }
                    break;

                case IngredientType.Lechuga:
                    lechuga = true;
                    //SetActiveSafe(lechugaObj, true);
                    if (!tlayudaIngredientList.Contains(IngredientType.Lechuga)) {
                        tlayudaIngredientList.Add(IngredientType.Lechuga);
                    }
                    break;

                case IngredientType.Quesillo:
                    quesillo = true;
                    //SetActiveSafe(quesilloObj, true);
                    if (!tlayudaIngredientList.Contains(IngredientType.Quesillo)) {
                        tlayudaIngredientList.Add(IngredientType.Quesillo);
                    }
                    break;

                case IngredientType.Cebolla:
                    cebolla = true;
                    //SetActiveSafe(cebollaObj, true);
                    if (!tlayudaIngredientList.Contains(IngredientType.Cebolla)) {
                        tlayudaIngredientList.Add(IngredientType.Cebolla);
                    }
                    break;

                case IngredientType.Tomate:
                    tomate = true;
                    //SetActiveSafe(tomateObj, true);
                    if (!tlayudaIngredientList.Contains(IngredientType.Tomate)) {
                        tlayudaIngredientList.Add(IngredientType.Tomate);
                    }
                    break;

                case IngredientType.SalsaRoja:
                    salsaRoja = true;
                    //SetActiveSafe(salsaRojaObj, true);
                    if (!tlayudaIngredientList.Contains(IngredientType.SalsaRoja)) {
                        tlayudaIngredientList.Add(IngredientType.SalsaRoja);
                    }
                    break;

                case IngredientType.Aguacate:
                    aguacate = true;
                    //SetActiveSafe(aguacateObj, true);
                    if (!tlayudaIngredientList.Contains(IngredientType.Aguacate)) {
                        tlayudaIngredientList.Add(IngredientType.Aguacate);
                    }
                    break;
            }
        }

        public void RemoveIngredient() {
            switch (ingredientToAdd) {
                case IngredientType.SmallTortilla:
                    if (tlayudaIngredientList.Count > 1) {
                        Debug.LogError("Cannot Delete Tortilla!");
                    } else {
                        tlayudaIngredientList.Clear();
                        //SetActiveSafe(tortillaObj, false);
                        currentTortilla = TortillaSize.None;
                    }
                    break;
                case IngredientType.MediumTortilla:
                    if (tlayudaIngredientList.Count > 1) {
                        Debug.LogError("Cannot Delete Tortilla!");
                    } else {
                        tlayudaIngredientList.Clear();
                        //SetActiveSafe(tortillaObj, false);
                        currentTortilla = TortillaSize.None;
                    }
                    break;
                case IngredientType.BigTortilla:
                    if (tlayudaIngredientList.Count > 1) {
                        Debug.LogError("Cannot Delete Tortilla!");
                    } else {
                        tlayudaIngredientList.Clear();
                        //SetActiveSafe(tortillaObj, false);
                        currentTortilla = TortillaSize.None;
                    }
                    break;
                case IngredientType.NormalBeans:
                    if (tlayudaIngredientList.Count > 2) {
                        Debug.LogError("Cannot Delete Base!");
                    } else {
                        tlayudaIngredientList.Remove(IngredientType.NormalBeans);
                        //SetActiveSafe(beansNormalObj, false);
                        currentBeans = BeanType.None;
                    }
                    break;
                case IngredientType.Bayos:
                    if (tlayudaIngredientList.Count > 2) {
                        Debug.LogError("Cannot Delete Base!");
                    } else {
                        tlayudaIngredientList.Remove(IngredientType.Bayos);
                        //SetActiveSafe(beansBayosObj, false);
                        currentBeans = BeanType.None;
                    }
                    break;
                case IngredientType.Puercos:
                    if (tlayudaIngredientList.Count > 2) {
                        Debug.LogError("Cannot Delete Base!");
                    } else {
                        tlayudaIngredientList.Remove(IngredientType.Puercos);
                        //SetActiveSafe(beansPuercosObj, false);
                        currentBeans = BeanType.None;
                    }
                    break;
                case IngredientType.Carne:
                    carne = false;
                    //SetActiveSafe(carneObj, false);
                    if (tlayudaIngredientList.Contains(IngredientType.Carne)) {
                        tlayudaIngredientList.Remove(IngredientType.Carne);
                    }
                    break;

                case IngredientType.Lechuga:
                    lechuga = false;
                    //SetActiveSafe(lechugaObj, false);
                    if (tlayudaIngredientList.Contains(IngredientType.Lechuga)) {
                        tlayudaIngredientList.Remove(IngredientType.Lechuga);
                    }
                    break;

                case IngredientType.Quesillo:
                    quesillo = false;
                    //SetActiveSafe(quesilloObj, false);
                    if (tlayudaIngredientList.Contains(IngredientType.Quesillo)) {
                        tlayudaIngredientList.Remove(IngredientType.Quesillo);
                    }
                    break;

                case IngredientType.Cebolla:
                    cebolla = false;
                    //SetActiveSafe(cebollaObj, false);
                    if (tlayudaIngredientList.Contains(IngredientType.Cebolla)) {
                        tlayudaIngredientList.Remove(IngredientType.Cebolla);
                    }
                    break;

                case IngredientType.Tomate:
                    tomate = false;
                    //SetActiveSafe(tomateObj, false);
                    if (tlayudaIngredientList.Contains(IngredientType.Tomate)) {
                        tlayudaIngredientList.Remove(IngredientType.Tomate);
                    }
                    break;

                case IngredientType.SalsaRoja:
                    salsaRoja = false;
                    //SetActiveSafe(salsaRojaObj, false);
                    if (tlayudaIngredientList.Contains(IngredientType.SalsaRoja)) {
                        tlayudaIngredientList.Remove(IngredientType.SalsaRoja);
                    }
                    break;

                case IngredientType.Aguacate:
                    aguacate = false;
                    //SetActiveSafe(aguacateObj, false);
                    if (tlayudaIngredientList.Contains(IngredientType.Aguacate)) {
                        tlayudaIngredientList.Remove(IngredientType.Aguacate);
                    }
                    break;
            }
        }


        private void SetActiveSafe(GameObject obj, bool state) {
            if (obj != null)
                obj.SetActive(state);
        }
    }
}