using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour {
    public Button button;
    public Text buttonText;
    private GameController gameController;//GameControllerAI sınıfının nesnesinin yaratılması

    public void SetSpace()//Butonların alanını ayarlayan sınıf.
    {
        {
            buttonText.text = gameController.GetPlayerSide();
            button.interactable = false;
            gameController.EndTurn();
        }


    }
    public void SetGameControllerReference(GameController controller)//GameControllerAI sınıfı private olduğundan setter fonksiyonunun değişmiş hali
    {
        gameController = controller;
    }
}
