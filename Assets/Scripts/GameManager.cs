using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] GameObject cam;
    [SerializeField] Transform computer;
    [SerializeField] Transform desk;
    [SerializeField] GameObject computerScreen;
    [SerializeField] Text[] textFields = new Text[6];
    [SerializeField] Text totalText;
    [SerializeField] Text taxableTotalText;
    [SerializeField] Spawner taxSpawner;
    [SerializeField] Spawner quotaSpawner;
    [SerializeField] Spawner propertySpawner;
    [SerializeField] Spawner complaintSpawner;
    [SerializeField] Spawner pinkSlipSpawner;
    [SerializeField] Spawner lateSlipSpawner;
    [SerializeField] Spawner mismatchSlipSpawner;
    [SerializeField] Spawner arrestSpawner;
    [SerializeField] Spawner winSpawner;
    [SerializeField] GameObject pauseScreen;
    float taxTimer;
    [SerializeField] float propertyTimer;
    [SerializeField] Text taxTimerText;
    [SerializeField] Text propertyTimerText;
    float taxOver;
    float propertyOver;
    public bool taxSubmitted = false;
    public bool propertySubmitted = false;
    public int quota;
    public int[] values = new int[6];
    public int total;
    public bool holdingTax = false;
    public bool holdingQuota;
    public bool holdingSlip;
    public bool holdingProperty;
    public string playerName;
    public int income;
    public int writeoff;
    public int propertyValue;
    public int taxableTotal;
    public int writeoffTotal;
    public GameObject propertyTax;
    public GameObject tax;
    float delay;
    float quotaDelay;
    bool startSpawn = true;
    public bool pause = false;
    bool taxO = true;
    bool pO = true;
    bool checkOnce = true;
    int originalTotal;
    GameObject[] pinkSlips;
    public int pinkSlipCount;
    // Start is called before the first frame update
    void Start()
    {
        if(level != 6)
        {
            taxTimer = 100 / level;
            taxOver = Time.time + taxTimer;
            propertyOver = Time.time + propertyTimer;
        }
        else
        {
            taxOver = 9999999999999999999;
            propertyOver = 9999999999999999999;
        }


        delay = Time.time + 1f;
        quotaDelay = Time.time + 3f;
        if(level != 6)
        {
            for (int i = 0; i < textFields.Length; i++)
            {
                if (i != 4 && i != 0)
                {
                    values[i] = Random.Range(0, level * 3) * 100;
                }
                else if (i == 4)
                {
                    values[i] = 69;
                }
                else if (i == 0)
                {
                    values[i] = Random.Range(2, level * 3) * 100;
                }
            }
        }
        else
        {
            for (int i = 0; i < textFields.Length; i++)
            {
                if (i != 4 && i != 0)
                {
                    values[i] = Random.Range(0, 3) * 100;
                }
                else if (i == 4)
                {
                    values[i] = 69;
                }
                else if (i == 0)
                {
                    values[i] = Random.Range(2, 3) * 100;
                }
            }
        }

        originalTotal = values[0] - values[1] + values[2] + values[3] - values[4] + values[5];
    }

    // Update is called once per frame
    void Update()
    {
        if(taxOver > Time.time && taxSubmitted == false)
        {
            taxTimerText.text = "Tax: " + $"{(int)taxOver - (int)Time.time}";
        }
        if (propertyOver > Time.time && propertySubmitted == false && level > 3)
        {
            propertyTimerText.text = "Property Tax: " + $"{(int)propertyOver - (int)Time.time}";
        }
        if(taxSubmitted == true)
        {
            taxTimerText.text = "Tax: Submitted";
        }
        if(propertySubmitted == true && level > 3)
        {
            propertyTimerText.text = "Property Tax: Submitted";
        }
        if (taxOver <= Time.time && taxO == true && tax != null && taxSubmitted == false)
        {
            tax.GetComponent<TaxSlip>().Overdue();
            taxTimerText.color = Color.red;
            taxTimerText.text = "Tax: Overdue!!!";
            taxO = false;
        }
        if(propertyOver <= Time.time && pO == true && propertyTax != null && propertySubmitted == false && level > 3)
        {
            propertyTimerText.color = Color.red;
            propertyTimerText.text = "Property Tax: Overdue!!!";
            propertyTax.GetComponent<PropertyTax>().Overdue();
            pO = false;
        }
        if (pause == true)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
        Debug.Log(income.ToString());
        Debug.Log(writeoff.ToString());
        Debug.Log(propertyValue.ToString());
        if (delay <= Time.time && startSpawn == true)
        {
            taxSpawner.spawn = true;
            quotaSpawner.spawn = true;
            if (level > 3)
            {
                propertySpawner.spawn = true;
            }
            startSpawn = false;
        }
        for (int i = 0; i < textFields.Length; i++)
        {
            textFields[i].text = values[i].ToString();
        }
        total = values[0] - values[1] + values[2] + values[3] - values[4] + values[5];
        taxableTotal = values[0] + values[3];
        writeoffTotal = values[1] + values[2];
        taxableTotalText.text = taxableTotal.ToString();
        if (originalTotal <= 100)
        {
            if (level != 6)
            {
                for (int i = 0; i < textFields.Length; i++)
                {
                    if (i != 4 && i != 0)
                    {
                        values[i] = Random.Range(0, level * 3) * 100;
                    }
                    else if (i == 4)
                    {
                        values[i] = 69;
                    }
                    else if (i == 0)
                    {
                        values[i] = Random.Range(2, level * 3) * 100;
                    }
                }
            }
            else
            {
                for (int i = 0; i < textFields.Length; i++)
                {
                    if (i != 4 && i != 0)
                    {
                        values[i] = Random.Range(0, 3) * 100;
                    }
                    else if (i == 4)
                    {
                        values[i] = 69;
                    }
                    else if (i == 0)
                    {
                        values[i] = Random.Range(2, 3) * 100;
                    }
                }
            }
            total = values[0] - values[1] + values[2] + values[3] - values[4] + values[5];
            originalTotal = values[0] - values[1] + values[2] + values[3] - values[4] + values[5];
        }
        totalText.text = total.ToString();
        if (Input.GetKeyDown(KeyCode.A) && holdingTax == false && holdingQuota == false && holdingSlip == false && pause == false && holdingProperty == false)
        {
            cam.transform.position = computer.position;
            computerScreen.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.D) && holdingTax == false && holdingQuota == false && holdingSlip == false && pause == false && holdingProperty == false)
        {
            cam.transform.position = desk.position;
            computerScreen.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }
        if (total != originalTotal)
        {
            totalText.color = Color.red;
        }
        else
        {
            totalText.color = Color.black;
        }
        if (taxableTotal > quota)
        {
            taxableTotalText.color = Color.red;
        }
        else if(taxableTotal <= quota)
        {
            taxableTotalText.color = Color.green;
        }
        if(holdingProperty == true && pause == false && propertySubmitted == false && propertyTax != null && level > 3)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                holdingProperty = false;
                propertyTax.GetComponent<PropertyTax>().Submit();
                propertySubmitted = true;
                if (propertyValue != values[5])
                {
                    mismatchSlipSpawner.spawn = true;
                }
            }
        }
        if (holdingTax == true && pause == false && taxSubmitted == false && tax != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                holdingTax = false;
                tax.GetComponent<TaxSlip>().Submit();
                taxSubmitted = true;
                if(income > quota)
                {
                    complaintSpawner.spawn = true;
                }
                if(income < taxableTotal)
                {
                    pinkSlipSpawner.spawn = true;
                }
                if(writeoff != writeoffTotal)
                {
                    mismatchSlipSpawner.spawn = true;
                }
            }
        }
        if(taxO == false)
        {
            lateSlipSpawner.spawn = true;
            taxO = true;
        }
        if(pO == false && level > 3)
        {
            lateSlipSpawner.spawn = true;
            pO = true;
        }
        if(tax == null && propertyTax == null && quotaDelay<=Time.time)
        {
            checkOnce = false;
            pinkSlips = GameObject.FindGameObjectsWithTag("PinkSlip");
            pinkSlipCount = pinkSlips.Length;
            Invoke("SlipCheck", 0.5f);

        }
    }
    void SlipCheck()
    {
        if (pinkSlipCount > 0)
        {
            arrestSpawner.spawn = true;
        }
        else
        {
            winSpawner.spawn = true;
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
