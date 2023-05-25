using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    int counter = 0;
    public int size = 10;

    public float spawnerDelay, Delay;

    public bool CanISpawn = true;
    bool stopSpawning = true;


    public GameObject Enemy1; // Enemigo Normal
    public GameObject Enemy2; // Enemigo Torreta
    public GameObject Enemy3; // Enemigo Misil
    GameObject aux;
    GameObject[] MyEnemies;

    Gamemanager gamemanager;


    int round = 1, rand;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("Gamemanager").GetComponent<Gamemanager>();
        MyEnemies = new GameObject[size]; //dynamic array
    }

    // Update is called once per frame
    void Update()
    {
        //the gameManager control de round loop game
        round = gamemanager.GetRound();

        if (round < 4)
        {
            if (counter >= size && round > 1 && stopSpawning)
            {
                // setting the next round to prepare the level
                counter = 0;
                size += (round * 2);
                MyEnemies = new GameObject[size];
                stopSpawning = false;
                Delay = Delay / 1.5f;
            }
            if (CanISpawn)
            {
                // spawning enemies with a delay between each one
                spawnerDelay += Time.deltaTime;
                if (counter < size)
                {
                    if (spawnerDelay > Delay)
                    {
                        if (round <= 1)
                        {
                            MyEnemies[counter] = Enemy1;
                        }
                        else if (round <= 2)
                        {
                            // we have more enemies to spawn in different rounds
                            // randomize the spawner
                            rand = Random.Range(0, MyEnemies.Length - 1);
                            switch (rand)
                            {
                                case 1:
                                    MyEnemies[counter] = Enemy1;
                                    break;
                                case 2:
                                    MyEnemies[counter] = Enemy2;
                                    break;
                            }
                        }
                        else
                        {
                            rand = Random.Range(0, MyEnemies.Length - 1);
                            switch (rand)
                            {
                                case 1:
                                    MyEnemies[counter] = Enemy1;
                                    break;
                                case 2:
                                    MyEnemies[counter] = Enemy2;
                                    break;
                                case 3:
                                    MyEnemies[counter] = Enemy3;
                                    break;
                            }
                        }
                        // finally after setting the enemy to spawn, let's instantiate it!
                        aux = Instantiate(MyEnemies[counter] == null ? Enemy1 : MyEnemies[counter], this.transform.position, Quaternion.Euler(0, 0, 180));
                        aux.SetActive(true);
                        spawnerDelay = 0;
                        counter++;
                    }
                }
                else
                {
                    // end loopgame
                    stopSpawning = true;
                    CanISpawn = false;
                }

            }
        }
        else
        {
            // if round is 4, no more enemies to spawn
            size = 0;
           
        }
        
    }

}
