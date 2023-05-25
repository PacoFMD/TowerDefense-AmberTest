using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameStatus : MonoBehaviour
{
    Image imgHealth;

    public float Health = 100;

    public GameObject loopUI;

    // Start is called before the first frame update
    private void Start()
    {
        imgHealth = GameObject.Find("HealthBar").GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        imgHealth.fillAmount = (Health / 100);
        GameOver();
    }

    public float TakeDamage(float _Amount)
    {
        Health -= _Amount;
        return Health;
    }

    public bool GameOver()
    {
        if (Health <= 0)
        {
            loopUI.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
