using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OvenStateController : MonoBehaviour
{
    public enum TlayudaState
    {
        EMPTY,
        BAKING,
        ALERT,
        BURNED
    }
    public enum OvenMode
    {
        OVEN,
        DELIVERY
    }

    [Header("Referencias")]
   public TlayudaManager tlayudaManager;       
    public OrderManager orderManager;               
    public MoneyManager moneyManager;               
    public GameObject alarmUI;                     
    public GameObject tlayudaGameObject;            

    [Header("Configuración de tiempos segundos")]
   //Tiempo que la tlayuda permanece horneando
    public float bakingDuration = 5f;
    //Duración de la alerta
    public float alertDuration = 3f;
    //Tiempo total hasta quemado
    public float totalToBurn => bakingDuration + alertDuration;

    [Header("Modo horno")]
    public OvenMode currentMode = OvenMode.OVEN;
    public float modeDuration = 4f;
    public bool deactivateOnBurn = true;//el objeto tlayuda se desactiva al quemarse

    // Estado interno
    public TlayudaState currentTlayudaState = TlayudaState.EMPTY;
    float stateTimer = 0f;
    Coroutine modeCoroutine;

    // Eventos opcionales para conectar desde inspector
    public UnityEvent OnEnterBaking;
    public UnityEvent OnEnterAlert;
    public UnityEvent OnEnterBurned;
    public UnityEvent OnEnterEmpty;
    public UnityEvent OnModeChangedToOven;
    public UnityEvent OnModeChangedToEntrega;

    void Start()
    {
        if (alarmUI != null) alarmUI.SetActive(false);
        RefreshInitialState();
    }

    void Update()
    {
        VerificationTlayudaState();
    }
    public void VerificationTlayudaState()
    {
        bool hasTlayuda = tlayudaManager != null && tlayudaManager.TlayudaIngredientList.Count > 0;

        switch (currentTlayudaState)
        {
            case TlayudaState.EMPTY:
                if (hasTlayuda)
                {
                    EnterState(TlayudaState.BAKING);
                }
                break;
            case TlayudaState.BAKING:
                if (!hasTlayuda)
                {
                    EnterState(TlayudaState.EMPTY);
                    break;
                }
                stateTimer += Time.deltaTime;
                if (stateTimer >= bakingDuration)
                {
                    EnterState(TlayudaState.ALERT);
                }
                break;
            case TlayudaState.ALERT:
                if (!hasTlayuda)
                {
                    EnterState(TlayudaState.EMPTY);
                    break;
                }
                stateTimer += Time.deltaTime;
                if (stateTimer >= totalToBurn)
                {
                    EnterState(TlayudaState.BURNED);
                }
                break;
            case TlayudaState.BURNED:
                HandleBurned();
                break;
        }
    }

    void EnterState(TlayudaState newState)
    {
        currentTlayudaState = newState;

        if (newState == TlayudaState.BAKING) stateTimer = 0f;
        else if (newState == TlayudaState.ALERT) stateTimer = bakingDuration;
        else if (newState == TlayudaState.BURNED) stateTimer = totalToBurn;
        else stateTimer = 0f;

        switch (newState)
        {
            case TlayudaState.BAKING:
                OnEnterBaking?.Invoke();
                if (alarmUI != null) alarmUI.SetActive(false);
                Debug.Log("[Oven] Tlayuda horneado.");
                break;

            case TlayudaState.ALERT:
                OnEnterAlert?.Invoke();
                if (alarmUI != null) alarmUI.SetActive(true);
                Debug.Log("[Oven] Alerta: retira tlayuda.");
                break;

            case TlayudaState.BURNED:
                OnEnterBurned?.Invoke();
                if (alarmUI != null) alarmUI.SetActive(false);
                Debug.Log("[Oven] Tlayuda quemada.");
                break;

            case TlayudaState.EMPTY:
                OnEnterEmpty?.Invoke();
                if (alarmUI != null) alarmUI.SetActive(false);
                Debug.Log("[Oven] vacío.");
                break;
        }
    }

    void HandleBurned()
    {
        if (tlayudaManager != null)
        {
            tlayudaManager.TlayudaIngredientList.Clear();
        }

        if (tlayudaGameObject != null && deactivateOnBurn)
        {
            tlayudaGameObject.SetActive(false);
        }

        EnterState(TlayudaState.EMPTY);
    }

    // PARA EL BOTON XR :3
    public void ToggleModeByXR()
    {
        if (modeCoroutine != null) StopCoroutine(modeCoroutine);
        modeCoroutine = StartCoroutine(TemporaryModeSwitch());
    }

    IEnumerator TemporaryModeSwitch()
    {
        if (currentMode == OvenMode.OVEN) SetMode(OvenMode.DELIVERY);
        else SetMode(OvenMode.OVEN);

        yield return new WaitForSeconds(modeDuration);

        SetMode(OvenMode.OVEN);
        modeCoroutine = null;
    }

    void SetMode(OvenMode mode)
    {
        currentMode = mode;

        if (mode == OvenMode.DELIVERY)
        {
            OnModeChangedToEntrega?.Invoke();
            Debug.Log("Modo Entrega activado");
            TryDeliverTlayuda();
        } else
        {
            OnModeChangedToOven?.Invoke();
            Debug.Log("Modo Horno activado");
        }
    }

    // DELIVERY 
    void TryDeliverTlayuda()
    {
        if (tlayudaManager == null || orderManager == null || moneyManager == null)
        {
            Debug.LogWarning("[Oven] Faltan referencias para entregar");
            return;
        }

        if (tlayudaManager.TlayudaIngredientList == null || tlayudaManager.TlayudaIngredientList.Count == 0)
        {
            Debug.Log("[Oven] No hay tlayuda lista para entregar.");
            return;
        }
        orderManager.CheckOrdenes();

        // Opcional: desactivar el objeto tlayuda físico si existe
        if (tlayudaGameObject != null)
        {
            tlayudaGameObject.SetActive(false);
        }
        // limío y entro en estadoo
        tlayudaManager.TlayudaIngredientList.Clear();
        EnterState(TlayudaState.EMPTY);
    }

    void RefreshInitialState()
    {
        if (tlayudaManager != null && tlayudaManager.TlayudaIngredientList.Count > 0)
            currentTlayudaState = TlayudaState.BAKING;
        else
            currentTlayudaState = TlayudaState.EMPTY;
    }
}