using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObj : MonoBehaviour

{
    public Vector3 pos;

    public Color hoverColor;
    Color startColor;

    GameObject turret;
    Renderer objRend;

    Gamemanager gamemanager;
    GameSelectTurrets gameTurrets;

    private void Awake()
    {
        gamemanager = GameObject.FindWithTag("Gamemanager").GetComponent<Gamemanager>();
        gameTurrets = GameObject.FindWithTag("Gamemanager").GetComponent<GameSelectTurrets>();

    }

    void Start()
    {
        objRend = GetComponent<Renderer>();
        startColor = objRend.material.color;
    }

    private void OnMouseDown()
    {
        // avoid to create a turret on the same position
        if (turret)
        {
            return;
        }

       GameObject turretToBuild = gamemanager.GetTurretToBuild();
       turret = Instantiate(turretToBuild, transform.position +pos, transform.rotation);
       gameTurrets.SpawnTankSelected();
    }

    private void OnMouseEnter()
    {
        objRend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        objRend.material.color = startColor;
    }
}
