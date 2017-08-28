using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListPokedex : MonoBehaviour {

    public InputField InputSearch;
    public Transform contentPanel;
    private List<Pokemon> itemList;
    private PersistantData data;

    private ButtonPool buttonObjectPool;

    // Use this for initialization
    void Start()
    {
        data = PersistantData.instance;
        itemList = new List<Pokemon>();
        buttonObjectPool = GetComponent<ButtonPool>();
        
        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        RemoveButtons();
        if (InputSearch.text.Length > 0)
        {
            itemList = PokedexManager.instance.Search(InputSearch.text);
            SetupButtons();
        }
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }

    private void SetupButtons()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            Pokemon item = itemList[i];

            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            PokedexItem sampleButton = newButton.GetComponent<PokedexItem>();
            sampleButton.Setup(item);
        }
    }
}
