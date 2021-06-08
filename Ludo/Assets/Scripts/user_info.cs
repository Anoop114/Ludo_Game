using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class user_info : MonoBehaviour
{
    // public GameObject userInfo;
    public Text name;
    public Text country;
    public InputField display_name;
    public InputField display_country;
    // Start is called before the first frame update
    void Start()
    {
        name.text = PlayerPrefs.GetString("username");
        country.text = PlayerPrefs.GetString("country");
        
    }

    // Update is called once per frame
    void Create()
    {
        name.text = display_name.text;
        country.text = display_country.text;
        PlayerPrefs.SetString("username",name.text);
        PlayerPrefs.SetString("country",country.text);
        
    }
}
