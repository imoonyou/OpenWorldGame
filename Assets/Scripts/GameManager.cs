using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    
     public int[] correctSequence; // Array to store the correct sequence
     private int nextStatueIndex = 0; // Index of the next statue in the sequence

     public StatueInteraction[] statues; // Array to store references to all statue objects

     public DayAndNight dayAndNight;

     public GameObject boss;

    [SerializeField] private Image winGame;
    [SerializeField] private GameObject healthbar;
    [SerializeField] private GameObject activeCount;
    [SerializeField] private GameObject activeLabel;
    [SerializeField] private TextMeshProUGUI activeStatue;
    [SerializeField] private GameObject crossHair;
    //private static int statueCount = 0;



    // Method to handle statue interactions

    public void HandleStatueInteraction(int statueNumber)
     {
         if (statueNumber == correctSequence[nextStatueIndex])
         {
             nextStatueIndex++;
             UpdateStatueActive();
            if (nextStatueIndex == correctSequence.Length)
             {
                //UpdateStatueActive();
                
               
                // Trigger win event
                dayAndNight.SetSkyToNight(5f);
                boss.SetActive(true);
                //Instantiate(boss,spawnPoint.transform.position,Quaternion.identity);
                FindObjectOfType<Boss>().OnBossDeath += HandleBossDeath;
                //summon boss

            }
         }
         else
         {
             nextStatueIndex = 0;
            
             ResetSequence(); // Reset statues from the incorrect statue onwards
         }
     }


    private void Update()
    {
        
    }

    private void ResetSequence()
     {
        foreach (var statue in statues)
        {
             statue.ResetInteraction();
        }
     }

    public void HandleBossDeath()
    {
        // Add any additional win condition handling here
        winGame.gameObject.SetActive(true);
        healthbar.SetActive(false);
        activeCount.SetActive(false);
        activeLabel.SetActive(false);
        crossHair.SetActive(false);
    }


    public void UpdateStatueActive()
    {
        string[] parts = activeStatue.text.Split('/');
        int currentValue = int.Parse(parts[0]);
        currentValue++;
        parts[0] = currentValue.ToString();
        activeStatue.text = string.Join("/", parts);
    }

    public void UpdateStatueCount()
    {
        activeStatue.text = nextStatueIndex.ToString();
    }
}
