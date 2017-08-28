using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Counter : MonoBehaviour {

    private int _Count;
    public int Count {
        set { if (value >= 0) _Count = value; UpdateResult(); }
        get { return _Count; }
    }

    private bool isEditing = false;
    private bool isDeleting = false;
    private Text ResultCount;

    private Image Background;
    private InputField Edit;
    private GameObject InputFieldText;

    private GameObject DeleteCanvas;

    private PersistantData data;

    // Use this for initialization
    void Start()
    {
        ResultCount = GetComponent<Text>();
        if (!ResultCount)
            ResultCount = GetComponentInChildren<Text>();
        if (!ResultCount)
            Debug.LogError("There is no TextComponent for CounterScript");
        

        Background = GameObject.Find("DarkBackground").GetComponent<Image>();
        
        InputFieldText = GameObject.Find("InputField").gameObject;
        Edit = InputFieldText.GetComponent<InputField>();

        DeleteCanvas = GameObject.Find("DeleteCanvas").gameObject;

        data = PersistantData.instance;
        if (data.data.GetActiveHunt() == null)
            Debug.LogError("There is no active Hunt");

        Background.gameObject.SetActive(false);
        InputFieldText.SetActive(false);
        DeleteCanvas.SetActive(false);

        Count = data.data.GetActiveHunt().totalCount;
    }

    private void UpdateResult()
    {
        ResultCount.text = Count.ToString();
        data.data.GetActiveHunt().totalCount = Count;
    }

    public void AddPoint()
    {
        Count++;
    }

    public void ResetPoint()
    {
        Count = 0;
    }

    public void ToggleEdit()
    {
        isEditing = !isEditing;

        Background.gameObject.SetActive(isEditing);
        InputFieldText.gameObject.SetActive(isEditing);

        if (!isEditing)
        {
            int value = Count;
            if (System.Int32.TryParse(Edit.text, out value))
                Count = value;
        } else
        {
            Edit.text = Count.ToString();
        }
    }

    public void Complete()
    {
        data.data.GetActiveHunt().Done = true;
        data.data.GetActiveHunt().Paused = false;

        SaveAndQuit();
    }

    public void Pause()
    {
        data.data.GetActiveHunt().Paused = true;
        SaveAndQuit();
    }

    public void EditHunt()
    {
        data.Save();
        SceneManager.LoadScene("FormHunt");
    }

    public void ToggleDeleteCanvas()
    {
        isDeleting = !isDeleting;

        DeleteCanvas.SetActive(isDeleting);
    }

    public void DeleteHunt()
    {
        // Raise Modal
        data.data.Hunts.Remove(data.data.GetActiveHunt());
        data.data.HuntActive = -1;
        data.Save();

        SceneManager.LoadScene("ListHunt");
    }

    private void SaveAndQuit()
    {
        data.Save();
        SceneManager.LoadScene("ListHunt");
    }
}
