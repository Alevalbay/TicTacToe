using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class AIPlayer //AI scripti için yazilan script için player değişkenlerini tutan sınıf(AI yazmasının sebebi farklı class yaratmak için GameController Scriptindeki ile aynı işi görür)
{
    public Image panel;
    public Text text;
    public Button button;
}
[System.Serializable]
public class AIPlayerColor//Oyuncu buttonlarının renklerini belirleyen class aynı şekilde ai belirteci normal game controllerdan ayrılmak için yapıldı.
{
    public Color panelColor;
    public Color textColor;
}
public class GameControllerAI : MonoBehaviour
{   //Normal oyun değişkenleri PlayervsAI için
    public Text time;
    public int timeLeft = 10;
    public Text[] buttonList;
    public string playerSide;
    public GameObject gameOverPanel;
    public Text gameOverText;
    private int moveCount;
    public GameObject restartButton;
    public Player PlayerX;
    public Player PlayerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject returnMenu;
    //AI Değişkenleri
    private string computerSide;
    public bool playerMove;
    public float delay;
    private int value;


    void Awake() //Sahnenin ilk açıldığında çalışan unity fonksiyonu
    {
        SetGameControllerReferenceOnButtons();
        gameOverPanel.SetActive(false);//Game over yazisinin ilk başka gözükmemesi için kapatılır
        moveCount = 0; 
        restartButton.SetActive(false);//Restard buttonu yazsisinin kapatılması için
        returnMenu.SetActive(false);//Return menu butonunun gözükmemsi için
        playerMove = true;

    }
    public void SetStartingSide(string startingSide)//X veya O butonlarına basıldığında ilk kimin hamle yapıcağını belirleyen fonksyion
    {
        playerSide = startingSide;
        if (playerSide == "X")
        {
            computerSide = "O";
            SetPlayerColor(PlayerX, PlayerO);
        }

        else
        {
            computerSide = "X";
            SetPlayerColor(PlayerO, PlayerX);
        }
        StartGame();

    }
    public void StartGame()//Oyunu başlatan fonksiyon
    {
        SetBoardInteractable(true);//Oyun alanını etkileşebilir kılar.
        SetPlayerButtons(false);   //Oyuncu seçimini kitler
        StartTimer();              
    }
    void Update() //Unityinin her frame de (FPS) de çalışan fonksiyonudur.
    {
        if(playerMove==false)
        {
            delay += delay * Time.deltaTime;
            if(delay>=100)//Yapay zekanın hemen hamle yapmasını engeller delay 100 olunca yapay zeka hamlesini yapar
            {//Yapay zekanın hamlesini hesaplar
                value = Random.Range(0, 8);
                if(buttonList[value].GetComponentInParent<Button>().interactable==true)
                {
                    buttonList[value].text = GetComputerSide();
                    buttonList[value].GetComponentInParent<Button>().interactable = false;
                    EndTurn();
                }
            }
        }
    }
    public void SetGameControllerReferenceOnButtons()//GridSpaceAI scriptindeki butonlara bu scriptten ulaşılmasını sağlayan fonksiyon
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpaceAI>().SetGameControllerReference(this);

        }
    }
    public string GetPlayerSide()//Oyuncunun sırasını tutup döndüren fonksiyon
    {
        return playerSide;
    }
    public string GetComputerSide()//AI sırasını tutup döndüren fonksiyon
    {
        return computerSide;
    }
    public void EndTurn()//Bir kişi hamlesi bittiğinde çalışan fonksiyon
    {
        moveCount++;
        //Oyuncunun kazanma şartını kontrol eden kisim
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {                                                                                                 //|x|x|x|//
            GameOver(playerSide);                                                                         //| | | |//
        }                                                                                                 //| | | |// 
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {                                                                                                 //| | | |//
            GameOver(playerSide);                                                                         //|x|x|x|//
        }                                                                                                 //| | | |//
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {                                                                                                 //| | | |//
            GameOver(playerSide);                                                                         //| | | |//
        }                                                                                                 //|x|x|x|//
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {                                                                                                 //|x| | |//
            GameOver(playerSide);                                                                         //|x| | |//
        }                                                                                                 //|x| | |//
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {                                                                                                 //| |x| |//
            GameOver(playerSide);                                                                         //| |x| |//
        }                                                                                                 //| |x| |//
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {                                                                                                 //| | |x|//
            GameOver(playerSide);                                                                         //| | |x|//
        }                                                                                                 //| | |x|//
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {                                                                                                 //|x| | |//
            GameOver(playerSide);                                                                         //| |x| |//
        }                                                                                                 //| | |x|//
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {                                                                                                 //| | |x|//
            GameOver(playerSide);                                                                         //| |x| |//
        }                                                                                                 //|x| | |//

        //AI'ın kazanma durumu kontrol eden kısım
        else if (buttonList[0].text == computerSide && buttonList[1].text == computerSide && buttonList[2].text == computerSide)
        {                                                                                                 //|x|x|x|//
            GameOver(computerSide);                                                                       //| | | |//
        }                                                                                                 //| | | |// 
        else if (buttonList[3].text == computerSide && buttonList[4].text == computerSide && buttonList[5].text == computerSide)
        {                                                                                                 //| | | |//
            GameOver(computerSide);                                                                       //|x|x|x|//
        }                                                                                                 //| | | |//
        else if (buttonList[6].text == computerSide && buttonList[7].text == computerSide && buttonList[8].text == computerSide)
        {                                                                                                 //| | | |//
            GameOver(computerSide);                                                                       //| | | |//
        }                                                                                                 //|x|x|x|//
        else if (buttonList[0].text == computerSide && buttonList[3].text == computerSide && buttonList[6].text == computerSide)
        {                                                                                                 //|x| | |//
            GameOver(playerSide);                                                                         //|x| | |//
        }                                                                                                 //|x| | |//
        else if (buttonList[1].text == computerSide && buttonList[4].text == computerSide && buttonList[7].text == computerSide)
        {                                                                                                 //| |x| |//
            GameOver(computerSide);                                                                       //| |x| |//
        }                                                                                                 //| |x| |//
        else if (buttonList[2].text == computerSide && buttonList[5].text == computerSide && buttonList[8].text == computerSide)
        {                                                                                                 //| | |x|//
            GameOver(computerSide);                                                                       //| | |x|//
        }                                                                                                 //| | |x|//
        else if (buttonList[0].text == computerSide && buttonList[4].text == computerSide && buttonList[8].text == computerSide)
        {                                                                                                 //|x| | |//
            GameOver(computerSide);                                                                       //| |x| |//
        }                                                                                                 //| | |x|//
        else if (buttonList[2].text == computerSide && buttonList[4].text == computerSide && buttonList[6].text == computerSide)
        {                                                                                                 //| | |x|//
            GameOver(computerSide);                                                                       //| |x| |//
        }                                                                                                 //|x| | |//
        else if (moveCount >= 9)
        {

            GameOver("draw");

        }
        else//Eğer kimse kazanmadıysa el bitiminde eli AI a geçiren kisim
        {
            ChangeSides();
        }


    }
    void SetPlayerColor(Player newPlayer, Player oldPlayer) //X ve O butonlarının renklerini ayarlayan kısım
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }
    void GameOver(string winingPlayer)//Bir tarafın kazanma şartı olduğunda yada movecount 9 olup berabere bitme durumunda Oyunu bitiren fonksiyon
    {
        SetBoardInteractable(false);
        if (winingPlayer == "draw")
        {
            SetGameOverText("IT'S DRAW");
            SetPlayerColorsInactive();
        }
        else
        {
            SetGameOverText(winingPlayer + " Wins");
        }
        gameOverPanel.SetActive(true);
        restartButton.SetActive(true);
        returnMenu.SetActive(true);



    }
    void ChangeSides()//El değişimini kontrol eden fonksiyon
    {
        delay = 10;
        playerMove = (playerMove == true) ? false : true;
      if(playerMove==true)
        {
            
            StopCoroutine("LosingTime");//Timer i resetleyen kısım
            SetPlayerColor(PlayerX, PlayerO);
            StartTimer();
        }
        else
        {
            StopCoroutine("LosingTime");//Timer i resetleyen kısım
            SetPlayerColor(PlayerO, PlayerX);
            StartTimer();
        }

    }
    void SetGameOverText(string value)//Game Over Textini Görünür kılan fonksiyon
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }
    public void RestartGame()//Oyunu tekrar başlatılmasını sağlayan fonksiyon
    {
        moveCount = 0;
        gameOverPanel.SetActive(false);
        SetBoardInteractable(true);
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
        SetPlayerButtons(true);
        restartButton.SetActive(false);
        returnMenu.SetActive(false);
        SetPlayerColorsInactive();
        playerMove = true;
        delay = 10;

    }
    void SetBoardInteractable(bool toogle)//Oyun alanındaki butonları etkileşebilir kulan kısım
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toogle;

        }
    }
    void SetPlayerButtons(bool toogle)//X veya O butonunun etkileşimini ayarlayan fonksyion
    {
        PlayerX.button.interactable = toogle;
        PlayerO.button.interactable = toogle;
    }
    void SetPlayerColorsInactive()//Eli olmayan oyuncunun rengini belirleyen fonksiyon
    {
        PlayerX.panel.color = inactivePlayerColor.panelColor;
        PlayerX.text.color = inactivePlayerColor.textColor;
        PlayerO.panel.color = inactivePlayerColor.panelColor;
        PlayerO.panel.color = inactivePlayerColor.textColor;
    }
    void StartTimer()//Zamanlayıcıyı başlatan fonksiyon
    {
        timeLeft = 10;
        StartCoroutine("LosingTime");
    }
    void ControlTime()//Sürenin bitişini kontrol eden fonksiyon
    {
        if (timeLeft <= 0)
        {
            StopCoroutine("LosingTime");
            if (playerSide == "X")
            {
                playerSide = "O";
                GameOver(playerSide);
            }
            else
            {
                playerSide = "X";
                GameOver(playerSide);
            }
        }
    }
    IEnumerator LosingTime()// Süreyi Azaltan fonksiyon
    {
        timeLeft = 10;
        while (true)
        {
            time.text = timeLeft.ToString();
            ControlTime();
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

}