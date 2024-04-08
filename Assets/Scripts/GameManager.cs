using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
     public int[] correctSequence; // Array to store the correct sequence
     private int nextStatueIndex = 0; // Index of the next statue in the sequence

     public StatueInteraction[] statues; // Array to store references to all statue objects

     // Method to handle statue interactions
     // Method to handle statue interactions
     public void HandleStatueInteraction(int statueNumber)
     {
         if (statueNumber == correctSequence[nextStatueIndex])
         {
             nextStatueIndex++;

             if (nextStatueIndex == correctSequence.Length)
             {
                 Debug.Log("You win!");
                 // Trigger win event
             }
         }
         else
         {
             nextStatueIndex = 0;
             ResetSequence(); // Reset statues from the incorrect statue onwards
         }
     }

     private void ResetSequence()
     {
        foreach (var statue in statues)
        {
             statue.ResetInteraction();
        }
     }
     
}
