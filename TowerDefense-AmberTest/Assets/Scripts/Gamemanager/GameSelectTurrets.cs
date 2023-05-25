using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameSelectTurrets : MonoBehaviour
{
    public GameObject[] btnPowers, prefabTurrets;

    public bool isWaiting = false;

    Gamemanager gamemanager;

    int buttonIndex = 0;


    private void Awake()
    {
        gamemanager = GameObject.FindWithTag("Gamemanager").GetComponent<Gamemanager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // add to the buttons actions to select a turret
        btnPowers[0].GetComponent<Button>().onClick.AddListener(SelectSimpleTurret);
        btnPowers[1].GetComponent<Button>().onClick.AddListener(SelectDoubleTurret);
        btnPowers[2].GetComponent<Button>().onClick.AddListener(SelectSpecialTurret);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting)
        {
            //loop for each button after a selected turret
            for (int i = 0; i < 3; i++)
            {
                btnPowers[i].GetComponent<Image>().fillAmount += Time.deltaTime /10 ;
                if (btnPowers[i].GetComponent<Image>().fillAmount == 1)
                {
                    btnPowers[i].GetComponent<Button>().interactable = true;
                }
            }

        }
    }

    void SelectSimpleTurret()
    {
        buttonIndex = 0;
        gamemanager.SetTurretToBuild(prefabTurrets[0]);
    }

    void SelectDoubleTurret()
    {
        buttonIndex = 1;
        gamemanager.SetTurretToBuild(prefabTurrets[1]);
    }

    void SelectSpecialTurret()
    {
        buttonIndex = 2;
        gamemanager.SetTurretToBuild(prefabTurrets[2]);
    }

    public void SpawnTankSelected()
    {
        btnPowers[buttonIndex].GetComponent<Button>().interactable = false;
        btnPowers[buttonIndex].GetComponent<Image>().fillAmount = 0;
        if (isWaiting == false)
        {
            StartCoroutine(WaitingToRefresh(10));
            isWaiting = true;
        }
    }

    IEnumerator WaitingToRefresh(float _waitTime)
    {
        yield return new WaitForSeconds(_waitTime);
    }
}
