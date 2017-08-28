using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class PokedexManager : MonoBehaviour {

    public static PokedexManager instance;

    public TextAsset pokemons;

    public Dictionary<int, Pokemon> FinalPokemons = new Dictionary<int, Pokemon>();

    public Texture2D texture;
    private Sprite[] sprites;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != null)
            Destroy(this);
    }

    public void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Images/" + texture.name);

        string fs = pokemons.text;
        string[] fLines = Regex.Split(fs, "\n|\r|\r\n");

        for (int i = 0; i < fLines.Length; i++)
        {
            if (fLines[i].Length == 0)
                continue;
            string[] values = Regex.Split(fLines[i], ";");
            if (values.Length != 3)
            {
                Debug.LogError("INVALID LINE \"" + fLines[i] + "\"");
                continue;
            }
            Pokemon pkm = new Pokemon();
            pkm.id = System.Int32.Parse(values[0]);
            pkm.english = values[1];
            pkm.french = values[2];
            pkm.image = sprites[pkm.id - 1];

            FinalPokemons[pkm.id] = pkm;
        }

    }

    public List<Pokemon> Search(string match, int limit = 15)
    {
        List<Pokemon> result = new List<Pokemon>();

        foreach (KeyValuePair<int, Pokemon> D in FinalPokemons)
        {
            if (Regex.IsMatch(GetPokemon(D.Key), match, RegexOptions.IgnoreCase))
            {
                result.Add(D.Value);
                //Debug.Log("MATCH !");
            }
            if (result.Count >= limit)
                break;
        }

        return result;
    }

    public string GetPokemon(int id)
    {
        if (PersistantData.instance.data.Lang == GameData.LANG.FR)
            return FinalPokemons[id].french;
        return FinalPokemons[id].english;
    }

    public Pokemon GetPokemonEntity(int id)
    {
        return FinalPokemons[id];
    }
}

[System.Serializable]
public class Pokemon
{
    public int id;
    public string english;
    public string french;
    public Sprite image;
}
