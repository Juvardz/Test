using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{


    // https://www.dafont.com/
    //FIND A FONT AN APPLY TO THE TEXTNESHPRO => 10 PUNTOS
   [SerializeField]
   TextMeshProUGUI scoreTextbox;

   [SerializeField]
   Transform livesContainer;

   bool hasLives = true;
    private static UIController _instance;
   private void Awake()
   {
    //Implement Singleton (instance) TO INVOKE IncreaseScore => 10 PUNTOS
    _instance = this;
   }

   public void IncreaseScore(float points)
   {
    float score = float.Parse(scoreTextbox.text);
    score += points;
    scoreTextbox.text = score.ToString();
   }

   public void DecreaseLives()
   {
    int maxLiveNumber = 0;
    int liveNumber = 0;
    Image [] liveImages= livesContainer.GetComponentsInChildren<Image>();
    Image maxLiveImage = null;

    foreach(Image liveImage in liveImages)
    {
        if(liveImage.name.StartsWith("Live-") && liveImage.enabled)
        {
            liveNumber = int.Parse(liveImage.name.Remove(0, 5));
            if(maxLiveNumber == 0 || liveNumber > maxLiveNumber)
            {
                maxLiveNumber = liveNumber;
                maxLiveImage = liveImage;
            }
        }
    }

    if(maxLiveImage != null)
    {
        maxLiveImage.enabled = false;
    }

    hasLives = maxLiveNumber > 0;

   }
    public static UIController Instance
    {
        get {return _instance;}
    }
   public bool HasLives()
   {
    return hasLives;
   }
}
