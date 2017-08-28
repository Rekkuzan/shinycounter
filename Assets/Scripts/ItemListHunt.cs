using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemListHunt : MonoBehaviour {

    public Button buttonComponent;
    public Text PokemonName;
    public Text GameName;
    public Text MethodName;
    public Text Counter;
    public Image PokemonIcon;

    private HuntData item;

    public void Setup(HuntData currentItem)
    {
        item = currentItem;

        PokemonName.text = PokedexManager.instance.GetPokemon(item.pokemonNumber);
        GameName.text = DataPkm.GetGameVersionString(item.Game);
        MethodName.text = DataPkm.GetHuntingModeString(item.Method);
        Counter.text = item.totalCount.ToString();
        PokemonIcon.sprite = PokedexManager.instance.GetPokemonEntity(item.pokemonNumber).image;
    }

   public void HandleClick()
    {
        PersistantData.instance.data.HuntActive = item.id;
        SceneManager.LoadScene("Hunt");
    }
}
