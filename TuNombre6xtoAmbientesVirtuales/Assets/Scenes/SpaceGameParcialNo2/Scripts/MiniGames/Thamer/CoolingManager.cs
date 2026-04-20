
using UnityEngine;
using System.Collections.Generic;

#region Enums
public enum CoolingActions {
    Explode,
    Stabilize,
    Unstabilize
}
public enum CoolingStates {
    None,
    Exploded,
    Stable,
    Unstable
}

#endregion Enums
public class CoolingManager : MonoBehaviour {
    #region references
    #endregion references

    #region variables
    [SerializeField] private CoolingStates CurrentState;
    [SerializeField] private float heatlevel;
    [SerializeField] private float heatDownSpeed;
     
    [SerializeField] private List<CoolingContainer> coolingContainers;

    float maxHeat;
    float normalizedHeat;

    #endregion variables

    #region unityMethods
    void Start() {
        StartVariableDefinitions();
        StartGetReferences();
        StartStable();
    }

    void Update()
    {
        
        heatlevel = CalculateTotalHeat();
        normalizedHeat = heatlevel / maxHeat;

        if (normalizedHeat <= 0f)
        {
            CoolingActionToState(CoolingActions.Explode);
        }
        else if (normalizedHeat <= 0.5f)
        {
            CoolingActionToState(CoolingActions.Unstabilize);
        }
        else
        {
            CoolingActionToState(CoolingActions.Stabilize);
        }

        constantBehaviour(CurrentState);
    }

    #endregion unityMethods

    #region localMethods

    #region variablesAndReferences
    private void StartVariableDefinitions() {
        CurrentState = CoolingStates.Stable;
        heatlevel = coolingContainers.Count * 100;
        maxHeat = coolingContainers.Count * 100f;
    }

    private void StartGetReferences() {
    }
    #endregion variablesAndReferences

    #region FSMmethods

    public void CoolingActionToState(CoolingActions Action) {
        switch (Action) {
            case CoolingActions.Stabilize:
                if (CurrentState == CoolingStates.Stable) {
                    Debug.Log("Cooling System is already " + CurrentState);
                } else if (CurrentState == CoolingStates.Unstable) {
                    EndUnstable();
                    StartStable();
                    Debug.Log("Cooling System is now " + CurrentState);
                } else if (CurrentState == CoolingStates.Exploded) {
                    Debug.Log("Cooling System " + CurrentState + ", cant " + Action);
                }

                break;

            case CoolingActions.Unstabilize:
                if (CurrentState == CoolingStates.Stable) {
                    EndStable();
                    StartUnstable();
                    Debug.Log("Cooling System is now " + CurrentState);
                } else if (CurrentState == CoolingStates.Unstable) {
                    Debug.Log("Cooling System is already " + CurrentState);
                } else if (CurrentState == CoolingStates.Exploded) {
                    Debug.Log("Cooling System is " + CurrentState + ", cant " + Action);
                }
                break;

            case CoolingActions.Explode:
                if (CurrentState == CoolingStates.Stable) {
                    EndStable();
                    StartExploded();
                    Debug.Log("Cooling System is now " + CurrentState);
                } else if (CurrentState == CoolingStates.Unstable) {
                    EndUnstable();
                    StartExploded();
                    Debug.Log("Cooling System is now " + CurrentState);
                } else if (CurrentState == CoolingStates.Exploded) {
                    Debug.Log("Cooling System is already " + CurrentState);
                }
                break;

            default:
                Debug.LogWarning("Invalid EnemyAction");
                break;
        }

    }

    private void constantBehaviour(CoolingStates State) {
        switch (State) {
            case CoolingStates.Stable:
                Stable();
                break;
            case CoolingStates.Unstable:
                Unstable();
                break;
            case CoolingStates.Exploded:
                Exploded();
                break;
        }
    }

    #endregion FSMmethods

    #region Statemethods

    #region StableMethods
    private void StartStable() {
        CurrentState = CoolingStates.Stable;
     
    }

    private void Stable()
    {
        running();


    }
    private void EndStable() {

    }
    #endregion  StableMethods

    #region UnstableMethods
    private void StartUnstable() {
        CurrentState = CoolingStates.Unstable;
    }

    private void Unstable() {

        running();
        Debug.Log("DANGER, HIGH HEAT LEVELS");

    }
    private void EndUnstable() {

    }
    #endregion UnstableMethods

    #region ExplodeMethods
    private void StartExploded() {
        CurrentState = CoolingStates.Exploded;
    }

    private void Exploded() {

    }
    private void EndExploded() {

    }
    #endregion ExplodeMethods

    #endregion Statemethods

    float timer = 0f;
    int index = 0;
    private void running()
    {
        timer += Time.deltaTime;

        if (timer >= heatDownSpeed)
        {
            timer += Time.deltaTime;

            if (timer >= heatDownSpeed)
            {
                timer = 0f;

                if (coolingContainers.Count == 0) return;

                if (index >= coolingContainers.Count)
                    index = 0;

                var container = coolingContainers[index];

                if (container != null && container.currentCapsule != null)
                {
                    container.currentCapsule.decreaseHeatLevel();
                }

                index++;
            }
        }
    }

    private float CalculateTotalHeat()
    {
        float total = 0f;

        foreach (var container in coolingContainers)
        {
            if (container != null && container.currentCapsule != null)
            {
                total += container.currentCapsule.personalHeatLevel;
            }
        }

        return total;
    }

    #endregion localMethods

    #region DebugKnobs
    [Header("Debug Actions")]
    public bool testStabilize;
    public bool testUnstabilize;
    public bool testExplode;

    private void LateUpdate() {
        // Move
        if (testStabilize) {
            CoolingActionToState(CoolingActions.Stabilize);
            testStabilize = false;
        }

        // Eat
        if (testUnstabilize) {
            CoolingActionToState(CoolingActions.Unstabilize);
            testUnstabilize = false;
        }

        // Die
        if (testExplode) {
            CoolingActionToState(CoolingActions.Explode);
            testExplode = false;
        }
    }
    #endregion
}
