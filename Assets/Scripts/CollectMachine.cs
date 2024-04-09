using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMachine : MonoBehaviour,IClickable
{
    [SerializeField] private MachineScriptableScript _scriptable;
    public void OnClick()
    {
        UIManager.Instance.GetMachineScriptable(_scriptable);

        UIManager.Instance.SetMachinePanelFromScriptable();
        
        UIManager.Instance.InitializeMachineProperty();

    }
}
