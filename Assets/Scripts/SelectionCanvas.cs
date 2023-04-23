using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionCanvas : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fadeCanvas;
    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private GameObject textChoose;
    [SerializeField] private GameObject textSlime;


    public GameObject buttonKnight;
    public GameObject buttonMage;
    public GameObject buttonSlime;


    // Start is called before the first frame update
    void Awake()
    {

        player.SetActive(false);

        fadeCanvas.SetActive(false);
        hudCanvas.SetActive(false);

        textChoose.SetActive(true);
        textSlime.SetActive(false);
    }

    public void OnButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case "CardKnight":
                buttonKnight.SetActive(false);
                buttonMage.SetActive(false);
                buttonSlime.SetActive(true);

                textChoose.SetActive(false);
                textSlime.SetActive(true);

                break;

            case "CardMage":
                buttonKnight.SetActive(false);
                buttonMage.SetActive(false);
                buttonSlime.SetActive(true);

                textChoose.SetActive(false);
                textSlime.SetActive(true);

                break;

            case "CardSlime":
                player.SetActive(true);
                fadeCanvas.SetActive(true);
                hudCanvas.SetActive(true);
                gameObject.SetActive(false);
                break;
        }
    }



}
