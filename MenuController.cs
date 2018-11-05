using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour {
    public int MenuSelect;//Ana Menudeki buttonlardan referans numarası alıp butonların hangi menuyu açıcağına karar veren script.
    public void SelectMenu(int MenuId)
    {   if(MenuId==1)
        {
            SceneManager.LoadScene(0);
        }
        else if(MenuId==2)
        {
            SceneManager.LoadScene(1);
        }
        else if(MenuId==3)
        {
            SceneManager.LoadScene(2);
        }
        else if(MenuId==4)
        {
            SceneManager.LoadScene(4);
        }
        else if(MenuId==0)
        {
            Application.Quit();//Unity'nin oyundan çıkma fonksiyonu
        }
    }
}
