using UnityEngine;
using System;

public enum ControllerState
{
    Default,
    PanelOpen
}

public enum KeyState
{
    X,
    A,
    Default
}

public class EventController : MonoBehaviour
{
    public static event Action MachineChoseAction;

    public static event Action BackChoseAction;
    
    private Ray ray;
    RaycastHit hit;
    
    private ControllerState currentState = ControllerState.Default;

    public static event Action<KeyState> keyStateAction;
    
    private void OnEnable()
    {
        BackChoseAction += OnPanelClosed;
        MachineChoseAction += OnPanelOpened;
    }

    private void OnDisable()
    {
        BackChoseAction -= OnPanelClosed;
        MachineChoseAction -= OnPanelOpened;
    }

    void Update()
    {
        switch (currentState)
        {
            case ControllerState.Default:
                CheckMouseButtonToRay();
                break;
            case ControllerState.PanelOpen:
                PanelOpenStateUpdate();
                break;
        }
    }
    
    private void CheckMouseButtonToRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                IClickable clickable = hit.collider.gameObject.GetComponent<IClickable>();
                if (clickable != null)
                {
                    hit.collider.gameObject.GetComponent<CollectMachine>().OnClick();
                    MachineChoseClicked();
                    OnPanelOpened();
                }
            }
        }
    }
    
    public void MachineChoseClicked()
    {
        MachineChoseAction?.Invoke();
    }

    public void BackClicked()
    {
        BackChoseAction?.Invoke();
    }
    
    private void PanelOpenStateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            keyStateAction?.Invoke((KeyState.X));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            keyStateAction?.Invoke((KeyState.A));
        }
    }

    
    public void OnPanelOpened()
    {
        currentState = ControllerState.PanelOpen;
    }

    
    public void OnPanelClosed()
    {
        currentState = ControllerState.Default;
    }
    
    
    
}
