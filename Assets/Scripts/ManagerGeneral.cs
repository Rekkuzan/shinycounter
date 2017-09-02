using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerGeneral : MonoBehaviour {

    PersistantData data;
    
	void Start () {
        data = PersistantData.instance;	
	}
	
    public void AddHunt()
    {
        data.data.HuntActive = -1;
        SceneManager.LoadScene("FormHunt");
    }

    public void EditHunt()
    { 
        SceneManager.LoadScene("FormHunt");
    }

    public void ShowHunt()
    {
        data.Save();
        SceneManager.LoadScene("Hunt");
    }

    public void ShowList()
    {
        SceneManager.LoadScene("ListHunt");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void About()
    {
        SceneManager.LoadScene("About");
    }
}
