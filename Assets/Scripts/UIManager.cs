using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using DIG.Tools;
using DIG.UIExpansion;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    [SerializeField] private GameObject generalPanel;
    
    [SerializeField] private TextMeshProUGUI machineNameText;
    [SerializeField]  private TextMeshProUGUI orderCodeText; // sipraiş kodu
    [SerializeField]  private TextMeshProUGUI productCodeText; // malzeme kodu
    [SerializeField]  private TextMeshProUGUI orderQuantityText; // max sipariş miktarı

    [SerializeField]  private TextMeshProUGUI oeeValueText; 
    
    [SerializeField]  private TextMeshProUGUI temperatureText; 
    [SerializeField]  private TextMeshProUGUI weightText; 

    [SerializeField]  private TextMeshProUGUI upperTemperatureText; 
    [SerializeField]  private TextMeshProUGUI downTemperatureText; 
    [SerializeField]  private TextMeshProUGUI acceralationText; 
    
    [SerializeField]  private TextMeshProUGUI pressureText; 

    private MachineScriptableScript _scriptable;

    private readonly string tempetureSymbol=" C°";
    private readonly string weightSymbol=" Kg";
    private readonly string acceralationSymbol=" mm/s2";
    private readonly string pressureSymbol=" /bar";

    
    [SerializeField] private ProgresBar m_ProgresBar;
    [SerializeField] private ProgresBar m_circularBar;
    
    
    //Machine
    
    private OEESpeed m_OeeSpeed;
    private Temperature m_Temperature;
    private Weight m_Weight;
    private UpperTemperature m_UpperTemperature;
    private DownTemperature m_DownTemperature;
    private Acceralation m_Acceralation;
    private Pressure m_Pressure;
    
    private void OnDisable()
    {
        EventController.keyStateAction  -= GetKeyStateAction;
    }

    private void Awake()
    {
        EventController.keyStateAction += GetKeyStateAction;
        EventController.BackChoseAction += StopAllnvokes;
    }

    private void StopAllnvokes()
    {
        CancelInvoke();
    }

    private void GetKeyStateAction(KeyState key)
    {
        switch (key)
        {
            case KeyState.X:
                // CanIncreaseOrderQuantityValue fonksiyonunu çağırmak yerine şart ifadesine alalım
                if (CanIncreaseOrderQuantityValue())
                {
                    // IncreaseOrderCode fonksiyonunu çağır
                    IncreaseOrderCode();
                
                    // IncreaseProductCode fonksiyonunu çağır
                    IncreaseProductCode();

                    // IncreaseOrderQuantityValue fonksiyonunu çağır
                    IncreaseOrderQuantityValue();  
                }             
                break;
        }
    }

    private void IncreaseOrderQuantityValue()
    {
        // Şart kontrolü IncreaseOrderQuantityValue fonksiyonunun içine alındı
        if (CanIncreaseOrderQuantityValue())
        {
            _scriptable.orderQuantityValue =
                IncreaseValue(_scriptable.orderQuantityValue, _scriptable.orderQuantityIncreaseValue);
            UpdateBarValue(m_ProgresBar, _scriptable.orderQuantityValue);
            SetOrderQuantityText(_scriptable.orderQuantityValue, _scriptable.orderQuantityMax);
        }
    }

    private bool CanIncreaseOrderQuantityValue()
    {
        if (_scriptable.orderQuantityValue+_scriptable.orderQuantityIncreaseValue > _scriptable.orderQuantityMax)
        {
            return false;
        }

        return true;
    }

   private void UpdateBarValue(ProgresBar m_bar, int _value )
   {
       m_bar.Value = _value;
   }

   private void UpdateBarMaxValue(ProgresBar m_bar, int _value )
   {
       m_bar.MaxFill = _value;
   }
   private void IncreaseProductCode()
    {
        string[] splitStringByDash= SplitStringByDash(_scriptable.productCode);
        
        int newOrderValue=int.Parse(splitStringByDash[2]);

        splitStringByDash[2]=IncreaseValue(newOrderValue, _scriptable.productCodeIncreaseValue).ToString();

        UpdatePanelText(productCodeText, string.Concat(splitStringByDash));
        
        _scriptable.productCode = string.Concat(splitStringByDash);
    }

    private void IncreaseOrderCode()
    {
        string[] splitStringByDash= SplitStringByDash(_scriptable.orderCode);
        
        int newOrderValue=int.Parse(splitStringByDash[2]);

        splitStringByDash[2]=IncreaseValue(newOrderValue, _scriptable.orderCodeIncreaseValue).ToString();

        UpdatePanelText(orderCodeText, string.Concat(splitStringByDash));

        _scriptable.orderCode = string.Concat(splitStringByDash);
    }

    
    private int IncreaseValue(int _value, int _count)
    {
        return _value + _count;
    }

    public void GetMachineScriptable( MachineScriptableScript _scriptable)
    {
       this. _scriptable = _scriptable;
    }

    public void SetMachinePanelFromScriptable()
    {
        machineNameText.text = _scriptable.machineName;
        orderCodeText.text = _scriptable.orderCode;
        productCodeText.text = _scriptable.productCode;

        SetOrderQuantityText(_scriptable.orderQuantityValue,_scriptable.orderQuantityMax);
        UpdateBarMaxValue(m_ProgresBar, _scriptable.orderQuantityMax);
        UpdateBarValue(m_ProgresBar, _scriptable.orderQuantityValue);
        
        
        UpdateBarMaxValue(m_circularBar, _scriptable.OEEMax);
        UpdateBarValue(m_circularBar, _scriptable.OEEValue);
        SetOEEText(_scriptable.OEEValue);

        SetTemperatureText(temperatureText,_scriptable.temperature);
        SetWeightText(weightText,_scriptable.weight);
        SetTemperatureText(upperTemperatureText,_scriptable.upperTemperature);
        SetTemperatureText(downTemperatureText,_scriptable.downTemperature);
        SetAcceralationText(acceralationText, _scriptable.acceralation);
        SetPressureText(pressureText,_scriptable.pressure);
    }

    private void SetOrderQuantityText(int _value,int _orderQuantityMax)
    {
        UpdatePanelText(orderQuantityText, _value.ToString() + "/" + _orderQuantityMax.ToString());
    }
    
    private void SetOEEText(int _oeeValue)
    {
        UpdatePanelText(oeeValueText,  "%" + _oeeValue.ToString());
    }

    private void SetTemperatureText(TextMeshProUGUI _text,float _value)
    {
        UpdatePanelText(_text, _value.ToString() + tempetureSymbol);
    }
    private void SetPressureText(TextMeshProUGUI _text,float _value)
    {
        UpdatePanelText(_text, _value.ToString() + pressureSymbol);
    }
    private void SetAcceralationText(TextMeshProUGUI _text,float _value)
    {
        UpdatePanelText(_text, _value.ToString() + acceralationSymbol);
    }
    
    private void SetWeightText(TextMeshProUGUI _text,float _value)
    {
        UpdatePanelText(_text, _value.ToString() + weightSymbol);
    }
    
    private void UpdatePanelText(TextMeshProUGUI _text, string temp)
    {
        _text.text = temp;
    }
    
    
    public string[] SplitStringByDash(string str)
    {
        string[] result = new string[3];

        // "-" karakterini bul
        int indexOfDash = str.IndexOf('-');
        if (indexOfDash != -1)
        {
            // "-" den öncesini ve sonrasını ayır
            result[0] = str.Substring(0, indexOfDash); // - den önceki kısmı al
            result[1]="-";
            result[2] = str.Substring(indexOfDash + 1); // - den sonraki kısmı al
        }
        else
        {
            Debug.LogWarning("Dash character not found in the string.");
        }

        return result;
    }




    public void InitializeMachineProperty()
    {
        m_OeeSpeed = new OEESpeed();
        m_Temperature = new Temperature();
        m_Weight = new Weight();
        m_UpperTemperature = new UpperTemperature();
        m_DownTemperature = new DownTemperature();
        m_Acceralation = new Acceralation();
        m_Pressure = new Pressure();

      
        InvokeRepeating(nameof(UpdateValuesInMinute), 60f, 60f); // Dakikada bir
        InvokeRepeating(nameof(UpdateValuesInSecond), 1f, 1f); // Saniyede bir

    }
    
    
    private void UpdateValuesInMinute()
    {
        m_OeeSpeed.UpdateValue(_scriptable.OEEMinRange,_scriptable.OEEMaxRange);
        
        SetOEEText(m_OeeSpeed.Value);
        
        UpdateBarValue(m_circularBar,m_OeeSpeed.Value);
    }

    private void UpdateValuesInSecond()
    {
        m_Temperature.UpdateValue(_scriptable.temperatureMinRange,_scriptable.temperatureMaxRange);
        m_Weight.UpdateValue(_scriptable.weightMinRange,_scriptable.weightMaxRange);
        m_UpperTemperature.UpdateValue(_scriptable.upperTemperatureMinRange,_scriptable.upperTemperatureMaxRange);
        m_DownTemperature.UpdateValue(_scriptable.downTemperatureMinRange,_scriptable.downTemperatureMaxRange);
        m_Acceralation.UpdateValue(_scriptable.acceralationMinRange,_scriptable.acceralationMaxRange);
        m_Pressure.UpdateValue(_scriptable.pressureMinRange,_scriptable.pressureMaxRange);
        
        SetTemperatureText(temperatureText,m_Temperature.Value);
        SetWeightText(weightText,m_Weight.Value);
        SetTemperatureText(upperTemperatureText,m_UpperTemperature.Value);
        SetTemperatureText(downTemperatureText,m_DownTemperature.Value);
        SetAcceralationText(acceralationText,m_Acceralation.Value);
        SetPressureText(pressureText,m_Pressure.Value);
        
    }
    
    public void OpenGoogle()
    {
        Application.OpenURL("https://www.google.com");
    }
    
    
}


// Class


// Aralık kontrolü için kullanılacak yardımcı sınıf
public static class RandomRangeHelper
{
    public static int GetRandomInt(int min, int max)
    {
        return UnityEngine.Random.Range(min, max + 1);
    }

    public static float GetRandomFloat(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}

// Her bir değişken için ayrı bir sınıf oluşturuldu
public class OEESpeed
{
    public int Value { get; private set; }
    
    public void UpdateValue(int OEEMinRange,int OEEMaxRange)
    {
        Value = RandomRangeHelper.GetRandomInt(OEEMinRange, OEEMaxRange);
    }
}

public class Temperature
{
    public float Value { get; private set; }

    public void UpdateValue(float TempetureMinRange,float TempetureMaxRange)
    {
        Value = RandomRangeHelper.GetRandomFloat(TempetureMinRange, TempetureMaxRange);
    }
}

public class Weight
{
    public float Value { get; private set; }

    public void UpdateValue(float weightMinRange,float weightMaxRange)
    {
        Value = RandomRangeHelper.GetRandomFloat(weightMinRange, weightMaxRange);
    }
}

public class UpperTemperature
{
    public float Value { get; private set; }

    public void UpdateValue(float upperTemperatureMinRange,float upperTemperatureMaxRange)
    {
        Value = RandomRangeHelper.GetRandomFloat(upperTemperatureMinRange, upperTemperatureMaxRange);
    }
}

public class DownTemperature
{
    public float Value { get; private set; }

    public void UpdateValue(float downTemperatureMinRange,float downTemperatureMaxRange)
    {
        Value = RandomRangeHelper.GetRandomFloat(downTemperatureMinRange, downTemperatureMaxRange);
    }
}

public class Acceralation
{
    public float Value { get; private set; }

    public void UpdateValue(float acceralationMinRange,float acceralationMaxRange)
    {
        Value = RandomRangeHelper.GetRandomFloat(acceralationMinRange, acceralationMaxRange);
    }
}

public class Pressure
{
    public float Value { get; private set; }

    public void UpdateValue(float pressureMinRange,float pressureMaxRange)
    {
        Value = RandomRangeHelper.GetRandomFloat(pressureMinRange, pressureMaxRange);
    }
}