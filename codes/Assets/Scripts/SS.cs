using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS : MonoBehaviour
{
    
    public static float FoodRateSliderValue = 3;
    public Slider foodSlider;

    public static float snakeSpeedSliderValue = 0.3f;
    public Slider snakeSlider;

    void Start()
    {
       
    }

    public float getFoodRateSliderValue()
    {
        return FoodRateSliderValue;
    }
    public void setFoodRateSliderValue(float value)
    {
        FoodRateSliderValue = value;
    }

    public float getsnakeSpeedSliderValue()
    {
        return snakeSpeedSliderValue;
    }
    public void setsnakeSpeedSliderValue(float value)
    {
        snakeSpeedSliderValue = value;
    }

    public void updateSliderWithMenuValues()
    {
        snakeSlider.SetValueWithoutNotify(snakeSpeedSliderValue);
        foodSlider.SetValueWithoutNotify(FoodRateSliderValue);
    }
    
    public void foodSliderControl()
    {
        FoodRateSliderValue = foodSlider.value; 
        Debug.Log("Food Creation rate: " + foodSlider.value);
    }

    public void snakeSliderControl()
    {
        snakeSpeedSliderValue = snakeSlider.value;      
        Debug.Log("Snake Speed: " + snakeSlider.value);
    }
}
