using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormHunt : MonoBehaviour {

    public ManagerGeneral manager;

    public Dropdown GameVersionDD;
    public Dropdown MethodVersionDD;

    public DataPkm.GameVersion DefaultGame = DataPkm.GameVersion.SUN_MOON;
    public DataPkm.HuntingMode DefaultHunt = DataPkm.HuntingMode.WILD;

    public Text Stats;
    public Toggle ShinyCharmToggle;
    public InputField NameInput;
    public int PkmId = -1;
    public Canvas PokemonSelectionCanvas;
    public PokedexItem SelectionPokemon;


    private bool ShinyCharm = false;
    private Dictionary<string, DataPkm.GameVersion> TmpValueGame = new Dictionary<string, DataPkm.GameVersion>();
    private Dictionary<string, DataPkm.HuntingMode> TmpValueHunt = new Dictionary<string, DataPkm.HuntingMode>();
    private Dictionary<DataPkm.HuntingMode, int> TmpValueHuntDD = new Dictionary<DataPkm.HuntingMode, int>();
    private float prob;

    private PersistantData data;

	// Use this for initialization
	void Start () {

        data = PersistantData.instance;
        PokemonSelectionCanvas.gameObject.SetActive(false);

        foreach (KeyValuePair<DataPkm.GameVersion, string> dataPair in DataPkm.GameVersionStringKey)
        {
            if (dataPair.Key != DataPkm.GameVersion.UNKNOW)
            {
                GameVersionDD.options.Add(new Dropdown.OptionData() { text = dataPair.Value });
                TmpValueGame[dataPair.Value] = dataPair.Key;
            }

        }

        if (data.data.HuntActive == -1)
        {
            GameVersionDD.value = (int)DefaultGame;
            GameVersionDD.captionText.text = DataPkm.GameVersionStringKey[DefaultGame];
        }
        else
        {
            GameVersionDD.value = (int)data.data.GetActiveHunt().Game;
            GameVersionDD.captionText.text = DataPkm.GameVersionStringKey[data.data.GetActiveHunt().Game];
        }

        GenerateMethod(true);
    }

    public void GenerateMethod(bool first = false)
    {
        if (GameVersionDD.value <= (int)DataPkm.GameVersion.HEARTGOLD_SOULSILVER)
            ShinyCharmToggle.gameObject.SetActive(false);
        else
            ShinyCharmToggle.gameObject.SetActive(true);

        while (MethodVersionDD.options.Count > 0)
            MethodVersionDD.options.RemoveAt(0);

        if (!DataPkm.HuntingMethodByGeneration.ContainsKey((DataPkm.GameVersion)GameVersionDD.value))
            Debug.LogError("Cannot find the Game Version");

        
        int TmpIntValue = 0;
        foreach (DataPkm.HuntingMode me in DataPkm.HuntingMethodByGeneration[(DataPkm.GameVersion)GameVersionDD.value])
        {
            MethodVersionDD.options.Add(new Dropdown.OptionData() { text = DataPkm.HuntingModeStringKey[me] });
            TmpValueHunt[DataPkm.HuntingModeStringKey[me]] = me;
            TmpValueHuntDD[me] = TmpIntValue;
            TmpIntValue++;
        }

        MethodVersionDD.value = 0;

        if (data.data.HuntActive == -1)
        {
            MethodVersionDD.value = (int)DefaultHunt;
            MethodVersionDD.captionText.text = DataPkm.HuntingModeStringKey[DefaultHunt];
        }
        else if (first)
        {
            MethodVersionDD.value = TmpValueHuntDD[data.data.GetActiveHunt().Method];
            MethodVersionDD.captionText.text = DataPkm.HuntingModeStringKey[data.data.GetActiveHunt().Method];

            if (data.data.GetActiveHunt().ShinyCharm)
            {
                ShinyCharmToggle.isOn = true;
                ShinyCharm = true;
            }
        }

        GenerateStat();
    }

    public void GenerateStat()
    {
        float stat = 0f;
        DataPkm.GameVersion CurrentVersion = TmpValueGame[GameVersionDD.options[GameVersionDD.value].text];
        DataPkm.HuntingMode CurrentMethod = TmpValueHunt[MethodVersionDD.options[MethodVersionDD.value].text];

        if (CurrentVersion <= DataPkm.GameVersion.BLACK_WHITE_2)
            stat = DataPkm.ShinyRatesGeneral["BEFORE_6G"];
        else
            stat = DataPkm.ShinyRatesGeneral["AFTER_6G"];

        if (ShinyCharm)
            stat *= DataPkm.ShinyCharmMultiplier;

        if (CurrentMethod == DataPkm.HuntingMode.HORDE)
            stat *= DataPkm.HordeMutiplier;

        // Case Exception
        if (CurrentMethod == DataPkm.HuntingMode.FRIEND_SAFARI)
            stat = DataPkm.FriendSafariShinyRate;
        else if (CurrentMethod == DataPkm.HuntingMode.BREEDING_MASUDA && !ShinyCharm)
            stat = DataPkm.ShinyRateByGenerationMasuda[CurrentVersion];
        else if (CurrentVersion >= DataPkm.GameVersion.BLACK_WHITE && CurrentMethod == DataPkm.HuntingMode.BREEDING_MASUDA && ShinyCharm)
            stat = DataPkm.ShinyRateByGenerationMasudaChroma[CurrentVersion];


        float denom = 1f / stat;
        denom = Mathf.CeilToInt(denom);

        prob = stat; 

        Stats.text = "1 / " + denom.ToString();
    }

    public void ShowPokemonSelection()
    {
        PokemonSelectionCanvas.gameObject.SetActive(true);
    }

    public void ValidatePokemonChoice(int id)
    {
        PkmId = id;
        PokemonSelectionCanvas.gameObject.SetActive(false);
        if (id  != -1)
            SelectionPokemon.Setup(PokedexManager.instance.GetPokemonEntity(id));
    }

    public void ToggleShinyCharm()
    {
        ShinyCharm = !ShinyCharm;
        GenerateStat();
    }

    public void Validate()
    {
        HuntData hunt = null;

        if (data.data.HuntActive == -1)
            hunt = new HuntData();
        else
            hunt = data.data.GetActiveHunt();

        DataPkm.GameVersion CurrentVersion = TmpValueGame[GameVersionDD.options[GameVersionDD.value].text];
        DataPkm.HuntingMode CurrentMethod = TmpValueHunt[MethodVersionDD.options[MethodVersionDD.value].text];

        hunt.Game = CurrentVersion;
        hunt.Method = CurrentMethod;
        hunt.ShinyCharm = ShinyCharm;
        hunt.prob = prob;
        if (NameInput.text.Length > 0)
            hunt.Name = NameInput.text;
        hunt.pokemonNumber = PkmId;

        if (data.data.HuntActive == -1)
        {
            data.data.Hunts.Add(hunt);
            data.data.HuntActive = hunt.id;
        }

        data.Save();
        Debug.Log("Hunt active is : " + data.data.HuntActive);
        manager.ShowHunt();
    }
}
