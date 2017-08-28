using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonInfosHunt : MonoBehaviour {

    public Text PokemonName;
    public Text GameName;
    public Text MethodName;
    public Text Title;
    public Text Proba;
    public Image PokemonImage;

	// Use this for initialization
	void Start () {
        HuntData d = PersistantData.instance.data.GetActiveHunt();

        PokemonName.text = PokedexManager.instance.GetPokemon(d.pokemonNumber);
        GameName.text = DataPkm.GetGameVersionString(d.Game);
        MethodName.text = DataPkm.GetHuntingModeString(d.Method);
        Title.text = d.Name;

        float denom = 1f / d.prob;
        denom = Mathf.CeilToInt(denom);
        Proba.text = "1 / " + denom.ToString();

        PokemonImage.sprite = PokedexManager.instance.GetPokemonEntity(d.pokemonNumber).image;
    }
}
