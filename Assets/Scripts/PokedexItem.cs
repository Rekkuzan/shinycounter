using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokedexItem : MonoBehaviour {

    public Text PokemonName;
    public Image PokemonImage;
    public int PokemonId;

    private FormHunt form;

    void Start()
    {
        form = GameObject.Find("Form").GetComponent<FormHunt>();
    }

    public void Setup(Pokemon pkm)
    {
        PokemonName.text = PokedexManager.instance.GetPokemon(pkm.id);
        PokemonImage.sprite = pkm.image;
        PokemonId = pkm.id;
    }

    public void HandleClick()
    {
        form.ValidatePokemonChoice(PokemonId);
    }
}
