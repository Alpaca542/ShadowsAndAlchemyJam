using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class grapherScript : MonoBehaviour
{
    //public GameObject graph;
    // public GameObject preLine;
    bool AmIFilled;
    public Image shower;
    public Slider sliderA;
    public Slider sliderB;
    public Slider sliderC;
    float rndA;
     float rndB;
     float rndC;
    float rndAUSER;
    float rndBUSER;
    float rndCUSER;
    public GameObject lp;
    public Transform[] randomPoint;
    public Image glass;
    public GameObject indicator;
    public GameObject requirerLL;
    public GameObject requireL;
    // public Image Indicator;
    // public Text loss;
    //public Text loss1;
    // public Text loss2;
    // public Text totalLoss;
    //float blue1;
    // float blue2;
    // float red;
    bool IHaveBlue;
    // GameObject CircleRed;
    // GameObject CircleGreen;
    // GameObject CircleBlue;
    bool clicked = false;
   public LineRenderer lineRenderer;

    public int resolution = 10; // Количество точек на графике
    public float xMin = -5f;     // Минимальное значение по оси X
    public float xMax = 5f;      // Максимальное значение по оси X
    public float yMin = -5f;     // Минимальное значение по оси Y
    public float yMax = 5f;      // Максимальное значение по оси Y
    //public Material lineMaterial; // Материал для линии
    bool check;
    public LineRenderer lineRenderer2;

    public int resolution2 = 10; // Количество точек на графике
    public float xMin2 = -160f;     // Минимальное значение по оси X
    public float xMax2 = 160f;      // Максимальное значение по оси X
    public float yMin2 = -160f;     // Минимальное значение по оси Y
    public float yMax2 = 160f;      // Максимальное значение по оси Y
   // public Material lineMaterial2; // Материал для линии

    void Start()
    {
        
        //LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        //lineRenderer.material = lineMaterial;
        // lineRenderer.widthMultiplier = 0.1f;

    }

    float Function(float x)
    {
        // Например, квадратичная функция
        return x * x;
        // Вы можете изменить этот код, чтобы определить свою функцию.
    }


    //public Text ClickRate;
    float clickrate = 0f;
    /*
    void Start()
    {

        indicator.GetComponent<Image>().color = Color.red;
        InvokeRepeating(nameof(StopClick), 1f, 1f);
        InvokeRepeating(nameof(Winner), 5f, 5f);
        InvokeRepeating(nameof(BRRRR), 0.3f, 0.3f);

        //check = true;

    }
    */

    public GameObject shrederUI;
    bool interact = false;
    public GameObject tube;
    Collision2D collision;
    public BoilerScript boiler;
    // bool check = false;

    bool blueNum = false;
    bool redNum = false;
    private void Winner()
    {
        if (clickrate >= 5f)
        {
            check = false;

            //shrederUI.SetActive(false);
            collision.gameObject.GetComponent<CookScript>().UnFreeze();

            // Indicator.color = Color.green;

            Invoke(nameof(turnTube), 2f);
            //tube.GetComponent<SpriteRenderer>().color = Color.red;
            // check = false;
            Invoke(nameof(DestroyUI), 2f);
            boiler.enabled = true;

        }
    }
    void Check()
    {

        // check = true;
        // float speed = clickrate / 3;
        //  glass.color = new Color(0, 1 * clickrate / 5 + 1, 0, 1 * clickrate / 5 + 1);
        // CircleBlue.transform.Rotate(0,0,0);


        // totalLoss.text = "Average loss: " + Convert.ToString((((int)(requirer.value * 100 - blue1 * 100)) + ((int)(requirer1.value * 100 - blue2 * 100)) + ((int)(requirer2.value * 100 - red * 100))) / 3);
        //  collision.gameObject.GetComponent<CookScript>().Freeze();
        // Indicator.color = new Color(Mathf.Abs((int)(requirer1.value * 100 - blue2 * 100)) / 255.0f, Mathf.Abs((int)(requirer.value * 100 - blue1 * 100)) / 255.0f, Mathf.Abs((int)(requirer2.value * 100 - red * 100)) / 255.0f, 1);
        // Debug.Log("Checking");


        //Debug.Log(((requirer.value * 100 - blue1 * 100) + (requirer1.value * 100 - blue2 * 100) + (requirer2.value * 100 - red * 100) / 3));
        
        lineRenderer2.positionCount = resolution2 + 1; // +1 для завершения линии

        for (int i = 0; i <= resolution2; i++)
        {
            float x = Mathf.Lerp(xMin2, xMax2, i / (float)resolution2);
            float y = fUSER(x); // Ваша функция
                            // y = Mathf.Clamp(y, yMin, yMax);
            lineRenderer2.SetPosition(i, new Vector3(x / 100.0f, y / 100.0f, 0));
        }
        if(CalculateAverage()<10f)
        {
            
            check = false;
            Invoke(nameof(DestroyUI), 1f);
            indicator.gameObject.GetComponent<Image>().color = Color.red;
            shower.color = Color.green;
            blueNum = false;
            redNum = false;
            requirerLL.GetComponent<Image>().color = Color.white;
            requireL.GetComponent<Image>().color = Color.white;
            AmIFilled = true;
        }
    }
    void DestroyUI()
    {
        shrederUI.SetActive(false);
        collision.gameObject.GetComponent<CookScript>().UnFreeze();
    }
    void turnTube()
    {

        tube.GetComponent<SpriteRenderer>().color = Color.white;
        boiler.enabled = true;
    }

    float CalculateAverage()
    {
        int k = 0;
        float sm = 0;
        for (int i = 0; i <= 10;i++)
        {
            sm += (float)Math.Pow((lineRenderer.GetPosition(i) - lineRenderer2.GetPosition(i)).magnitude,2);
            k += 1;
        }
        return sm / (float)k;
    }
    bool checkIfSlotIsFull(Dictionary<string, int> inv, int slot)
    {
        return slot < inv.Count;
    }

    public void turnBlue()
    {
        IHaveBlue = true;
        requireL.GetComponent<Image>().color = Color.blue;
    }

    public float f(float x)
    {
        return (x * x * rndA + x * rndB + rndC);
    }
    public float fUSER(float x)
    {
        return (x * x * sliderA.value + x * sliderB.value + sliderC.value);
    }
    void Update()
    {
        

        if (collision != null && collision.gameObject.tag == "Cook")
        {
            CookScript cook = collision.gameObject.GetComponent<CookScript>();
            if (interact && ((checkIfSlotIsFull(cook.inventory, cook.ActiveSlot) && cook.inventory.ElementAt(cook.ActiveSlot).Key == "green")||((checkIfSlotIsFull(cook.inventory, cook.ActiveSlot) && cook.inventory.ElementAt(cook.ActiveSlot).Key == "greenANDblue"))))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //shrederUI.SetActive(true);
                    //collision.gameObject.GetComponent<CookScript>().Freeze();
                    if (checkIfSlotIsFull(cook.inventory, cook.ActiveSlot))
                    {
                        if (!AmIFilled)
                        {


                            if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "green")
                            {
                                blueNum = true;
                                collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                                requirerLL.GetComponent<Image>().color = Color.green;

                            }
                            if (cook.inventory.ElementAt(cook.ActiveSlot).Key == "greenANDblue")
                            {
                                redNum = true;
                                collision.gameObject.GetComponent<CookScript>().RemoveItem(cook.inventory.ElementAt(cook.ActiveSlot).Key);
                                requireL.GetComponent<Image>().color = Color.cyan;

                            }
                        }
                    }
                    if ((blueNum&&redNum))
                    {
                        rndA = UnityEngine.Random.Range(10, 100) / 100.0f;
                        rndB = UnityEngine.Random.Range(-6, 6);
                        rndC = UnityEngine.Random.Range(-100, -10);
                        collision.gameObject.GetComponent<CookScript>().Freeze();
                        check = true;
                        lineRenderer.positionCount = resolution + 1; // +1 для завершения линии

                        for (int i = 0; i <= resolution; i++)
                        {
                            float x = Mathf.Lerp(xMin, xMax, i / (float)resolution);
                            float y = f(x); // Ваша функция
                                            // y = Mathf.Clamp(y, yMin, yMax);
                            lineRenderer.SetPosition(i, new Vector3(x / 100.0f, y / 100.0f, 0));
                        }
                        shrederUI.SetActive(true);
                    }


                }

            }
            if (AmIFilled && (Input.GetKeyDown(KeyCode.Space)))
            {
                cook.GetItem("Graphed");
                AmIFilled = false;
                indicator.GetComponent<Image>().color = Color.white;

            }
        }
        if (check && (collision != null))
        {
            Check();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cook"))
        {
            interact = true;
            this.collision = collision;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cook"))
        {
            interact = false;
            collision = null;
        }
    }
}
