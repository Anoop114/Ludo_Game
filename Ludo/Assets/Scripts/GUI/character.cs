using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class character : MonoBehaviour
{
    [Header("Sprite To Change")]
    public SpriteRenderer avtar;
    
    [Header("Sprite To Cycle")]
    public List<Sprite> options = new List<Sprite>();
    private int count = 0;
    // Start is called before the first frame update
    public void Nextoption(){
        count++;
        if(count >= options.Count){
            count = 0;
        }
        avtar.sprite = options[count];
    }
    public void prevoption(){
        count--;
        if(count <= 0){
            count = options.Count-1;
        }
        avtar.sprite = options[count];
    }
}
