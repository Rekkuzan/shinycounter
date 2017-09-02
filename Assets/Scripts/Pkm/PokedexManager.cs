using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Linq;

public class PokedexManager : MonoBehaviour {

    public static PokedexManager instance;

    public TextAsset pokemons;

    public List<Pokemon> FinalPokemons = new List<Pokemon>();
    public Sprite DefaultSprite;

    public Texture2D texture;
    private Sprite[] sprites;

    private Dictionary<int, int> SpecialForms = new Dictionary<int, int>
    {
        {19, 2 },
        {20, 2 },
        {26, 2 },
        {27, 2 },
        {28, 2 },
        {37, 2 },
        {38, 2 },
        {50, 2 },
        {51, 2 },
        {52, 2 },
        {53, 2 },
        {74, 2 },
        {75, 2 },
        {76, 2 },
        {88, 2 },
        {89, 2 },
        {103, 2 },
        {105, 2 },
        {412, 3 },
        {413, 3 },
        {421, 2 },
        {422, 2 },
        {423, 2 },
        {492, 2 },
        {521, 2 },
        {550, 2 },
        {555, 2 },
        {592, 2 },
        {593, 2 },
        {641, 2 },
        {642, 2 },
        {645, 2 },
        {646, 3 },
        {647, 2 },
        {648, 2 },
        {668, 2 },
        {678, 2 },
        {718, 3 },
        {720, 2 },
        {745, 2 },
        {746, 2 },
    };

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
        int indexSprite = 0;
        int maxForm = 0;

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

            int pkmId = System.Int32.Parse(values[0]);

            if (SpecialForms.ContainsKey(pkmId))
                maxForm = SpecialForms[pkmId];
            else
                maxForm = 1;

            Pokemon pkm = null;
            for (int j = 0; j < maxForm; j++)
            {
                pkm = new Pokemon();
                pkm.id = pkmId;
                pkm.english = values[1];
                pkm.french = values[2];

                pkm.image = sprites[indexSprite];
                indexSprite++;
                FinalPokemons.Add(pkm);
            }
        }
    }

    public List<Pokemon> Search(string match, int limit = 15)
    {
        List<Pokemon> result = new List<Pokemon>();

        foreach (Pokemon D in FinalPokemons)
        {
            if (Regex.IsMatch(GetPokemon(D.id), match, RegexOptions.IgnoreCase))
            {
                result.Add(D);
            }
            if (result.Count >= limit)
                break;
        }

        return result;
    }

    public string GetPokemon(int id)
    {
        Pokemon d = FinalPokemons.Where(obj => obj.id == id).ToList()[0];

        if (PersistantData.instance.data.Lang == GameData.LANG.FR)
            return d.french;
        return d.english;
    }

    public Pokemon GetPokemonEntity(int id)
    {
        return FinalPokemons.Where(obj => obj.id == id).ToList()[0];
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
