using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public SS sliderScript;
    // Food Prefab
    public GameObject foodPrefab;
    // slider value
    public float oldSliderValue;
    // Borders
    [SerializeField] Transform borderTop;
    [SerializeField] Transform borderBottom;
    [SerializeField] Transform borderLeft;
    [SerializeField] Transform borderRight;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("isStart",0.2f,0.2f);
    }
   
    public void isStart()
    {
        if (Snake.isStart)
        {
            CancelInvoke("isStart");
            FoodStart();
        }
    }
    public void FoodStart()
    {
        oldSliderValue = 3;
        // cheking for new slider value after every 0.2 second
        InvokeRepeating("SpawnSlider",0.2f,0.2f);

        //Intial slider value = 3
        print("Initial Food creation rate: "+ oldSliderValue);
        InvokeRepeating("Spawn", oldSliderValue, oldSliderValue);
    }

    
    public void SpawnSlider()
    {
        //if slider value is changed
        if( sliderScript.getFoodRateSliderValue() != oldSliderValue)
        {
            //Canceling invoking with previous slider value
            CancelInvoke("Spawn"); 

            //start invoking with current slider value
            InvokeRepeating("Spawn", sliderScript.getFoodRateSliderValue(), sliderScript.getFoodRateSliderValue());
            oldSliderValue = sliderScript.getFoodRateSliderValue();
        }
    }
    
     public void Spawn()
     {
        if (Snake.isGameOver == true)
        {
            CancelInvoke("Spawn");
            CancelInvoke("SpawnSlider");
        }
        else
        {
            // x position between left & right border
            int x = (int)Random.Range(borderLeft.position.x + 1, borderRight.position.x - 1);

            // y position between top & bottom border
            int y = (int)Random.Range(borderBottom.position.y + 1, borderTop.position.y - 1);

            // Instantiate the food at (x, y)
            Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity); // default rotation
        }
     }

    
}
