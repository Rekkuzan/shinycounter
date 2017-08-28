using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListHunt : MonoBehaviour
{
    public GameObject prefabButton;

    public Transform contentPanel;
    private List<HuntData> itemList;
    private PersistantData data;


    // Use this for initialization
    void Start()
    {
        data = PersistantData.instance;
        itemList = data.data.Hunts;
        PersistantData.instance.data.HuntActive = -1;

        RefreshDisplay();
    }

    void RefreshDisplay()
    {
        SetupButtons();
    }

    private void SetupButtons()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            HuntData item = itemList[i];
            GameObject newButton = (GameObject)GameObject.Instantiate(prefabButton, contentPanel);

            ItemListHunt sampleButton = newButton.GetComponent<ItemListHunt>();
            sampleButton.Setup(item);
        }
    }
}
