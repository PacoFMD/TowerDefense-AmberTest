using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Gamemanager : MonoBehaviour
{
    int NumberEnemy, round = 1;

    public GameObject defaultTurret, victoryPanel;
    GameObject turretToBuild;

    public Transform[] wayPointList;

    public TextMeshProUGUI enemyOnRound, roundText;
    bool roundChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyOnRound.text = "Enemies Left: 0";
        turretToBuild = defaultTurret;
        NumberEnemy = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().size;

    }

    // Update is called once per frame
    void Update()
    {
        if (roundChanged)
        {
            NumberEnemy = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().size;
            roundChanged = false;
        }

        // condition to win
        if(round == 4 && NumberEnemy <= 0)
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0;
        }

        enemyOnRound.text = "Enemies Left: " + NumberEnemy.ToString();
        roundText.text = "Round: " + round.ToString();


    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject _turret)
    {
        turretToBuild = _turret;
    }

    public void EnemiesLeft()
    {
        // no more enemies after round 3
        if(round < 4)
        {
            NumberEnemy -= 1;
            if (NumberEnemy <= 0)
            {
                round++;
                GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().CanISpawn = true;
                roundChanged = true;
            }
        }
      
    }

    public int GetRound()
    {
        return round;
    }
}
