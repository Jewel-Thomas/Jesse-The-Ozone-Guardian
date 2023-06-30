using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Manager : MonoBehaviour
{
   public void Exit()
   {
       Application.Quit();
       Debug.Log("Game has quitted");
   }
}
