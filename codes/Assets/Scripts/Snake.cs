using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{

    //Tail Prefab
    public GameObject tailPrefab;
    //Tail list
    List<Transform> tail = new List<Transform>();
    
    //Sound source
    public AudioSource EatingSound;
    public AudioSource GameOverSound;
    public SS sliderScript;

    //variable for global direction indepedent of head oriantation 
    uint gamedir;

    //Direction values
    const uint UP = 0;
    const uint RIGHT = 1;
    const uint DOWN = 2;
    const uint LEFT = 3;

    //boolean flag for food collision
    bool ate = false;

    //isStart variable will be used to check game started or not
    public static bool isStart = false;
    //isGameOver variable will be used to stop food spawning from SpawnFood script when the game ends 
    public static bool isGameOver;

    public MenuScript menuScript;
    public GameOverScreen GameOverScreen;

    // slider value
    public float oldSliderValue;

    // Start is called before the first frame update
    void Start()
    {
        if(isStart == false)
        {
            menuScript.Setup();
        }
        else
        {
            gameStart();
        }
    }
    void gameStart()
    {
        sliderScript.updateSliderWithMenuValues();
        //Initialization
        oldSliderValue = sliderScript.getsnakeSpeedSliderValue();
        //dir = Vector2.down;
        gamedir = DOWN;
        isGameOver = false;
        
        // cheking for new slider value after every 0.2 second
        InvokeRepeating("speedSlider", 0.2f, 0.2f);

        print("Initial Snake speed: " + oldSliderValue);
        InvokeRepeating("Move", oldSliderValue, oldSliderValue);
    }

    // Update is called once per Frame
    void Update()
    {
        
        // Move in a new Direction
        if (Input.GetKey(KeyCode.RightArrow))
        {
        
            if (gamedir == DOWN)
            {
                moveLeftofHead();
                gamedir = RIGHT;
            }
            else if(gamedir == UP)
            {
                gamedir = RIGHT;
                moveRightofHead();
            }
        }
            
        else if (Input.GetKey(KeyCode.DownArrow))
        {
         
            if (gamedir == RIGHT)
            {
                gamedir = DOWN;
                moveRightofHead();
            }
            else if (gamedir == LEFT)
            {
                gamedir = DOWN;
                moveLeftofHead();
            }
        }
               
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (gamedir == UP)
            {
                gamedir = LEFT;
                moveLeftofHead();
            }
            else if (gamedir == DOWN)
            {
                gamedir = LEFT;
                moveRightofHead();
            }
        }
            
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (gamedir == LEFT)
            {
                gamedir = UP;
                moveRightofHead();
            }
            else if (gamedir == RIGHT)
            {
                gamedir = UP;
                moveLeftofHead();
            }
        }
    }

    void moveLeftofHead()
    {
        transform.Rotate(0, 0, 90);
    }

    void moveRightofHead()
    {
        transform.Rotate(0, 0, -90);
    }

    public void speedSlider()
    {
        //if slider value is changed
        if (sliderScript.getsnakeSpeedSliderValue() != oldSliderValue)
        {
            //Canceling invoking with previous slider value
            CancelInvoke("Move");

            //start invoking with current slider value
            InvokeRepeating("Move", sliderScript.getsnakeSpeedSliderValue(), sliderScript.getsnakeSpeedSliderValue());
            oldSliderValue = sliderScript.getsnakeSpeedSliderValue();
        }
    }

    void Move()
    {
        // Save current position
        Vector2 v = transform.position;
        
        // Move head into new direction
        transform.Translate(Vector2.down);

        // if Ate something then insert new element into gap
        if (ate)
        {          
            EatingSound.Play();

            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);
            
            // Reset the flag
            ate = false;

            //incrementing score
            ScoreScript.scoreValue++;

            Debug.Log("score: " + tail.Count);
        }
        // if we have Tail
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Food
        if (collision.name.StartsWith("food"))
        {
            ate = true;
            // destroy game obj (food)
            Destroy(collision.gameObject);
        }
      
        //border
        else if (collision.name.StartsWith("Wall"))
            onGameOver();

        //itself
        else if (collision.name.StartsWith("tail"))
            onGameOver();
    }
    public void onGameOver()
    {
        stopProcessing();
        GameOverSound.Play();
        GameOverScreen.Setup(tail.Count);      
    }

    public void stopProcessing()
    {
        //reset slider values
        sliderScript.setsnakeSpeedSliderValue(0.3f);
        sliderScript.setFoodRateSliderValue(3);
        //if on restart we want to have the slider values we set on sliders in the game, just remove above two lines

        //Stop moving snake as well as checking for slider when the game ends
        CancelInvoke("Move");
        CancelInvoke("speedSlider");
        isGameOver = true;
    }
    public void QuitInMid()
    {
        stopProcessing();
        menuScript.Setup();
    }
    
}