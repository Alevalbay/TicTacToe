using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpaceAI : MonoBehaviour {
    public Button button;
    public Text buttonText;
    private GameControllerAI gameControllerAI; //GameControllerAI sınıfının nesnesinin yaratılması

    public void SetSpace()//Butonların alanını ayarlayan sınıf.
    {
        if (gameControllerAI.playerMove == true)
        {
            buttonText.text = gameControllerAI.GetPlayerSide();
            button.interactable = false;
            gameControllerAI.EndTurn();
        }


    }
    public void SetGameControllerReference(GameControllerAI controller)//GameControllerAI sınıfı private olduğundan setter fonksiyonunun değişmiş hali
    {
        gameControllerAI = controller;
    }

}
