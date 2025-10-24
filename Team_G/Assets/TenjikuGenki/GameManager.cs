using UnityEngine;

public class GameManager : MonoBehaviour
{
   public int GameTimer, Faze = 0;
   public int Score;
   public int[] KillCnt = { 0, 0, 0 };

   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
   {
       // �t�F�[�Y�؂�ւ�
       Faze = 1;
   }

   // Update is called once per frame
   void Update()
   {

   }
}
