using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewMachineOrder", menuName = "AddMachine/MachineOrder")]
public class MachineScriptableScript : ScriptableObject
{
    public string machineName; 
    public string orderCode; // sipraiş kodu
    public int orderCodeIncreaseValue;
    
    public string productCode; // malzeme kodu
    public int productCodeIncreaseValue;

    public int orderQuantityValue;
    public int orderQuantityMax; // max sipariş miktarı
    public int orderQuantityIncreaseValue;
    
    public int OEEValue;
    public int OEEMax;
    public int OEEMinRange;
    public int OEEMaxRange;
    
    
    public float temperature; 
    public float temperatureMinRange;
    public float temperatureMaxRange;
    
    public float weight;
    public float weightMinRange; 
    public float weightMaxRange; 

    public float upperTemperature;
    public float upperTemperatureMinRange;
    public float upperTemperatureMaxRange;
    
    public float downTemperature;
    public float downTemperatureMinRange;
    public float downTemperatureMaxRange;

    
    public float acceralation;
    public float acceralationMinRange;
    public float acceralationMaxRange;

    public float pressure; 
    public float pressureMinRange;
    public float pressureMaxRange;
    
    
}