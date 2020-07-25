using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SpeciesView : MonoBehaviour
{
    [SerializeField] private Text NameText;
    [SerializeField] private Text DescriptionText;
    [SerializeField] private Button BuildButton;
    [SerializeField] private Button DestroyButton;
    [SerializeField] private Text Quantity;

    private Game game;
    private Species species;

    public void Init(Game game, Species species)
    {
        this.game = game;
        this.species = species;

        NameText.text = species.Name;
        DescriptionText.text = species.Description;
        BuildButton.onClick.AddListener(() => game.Build(species));
        DestroyButton.onClick.AddListener(() => game.Destroy(species));
    }

    private void Update()
    {
        if (species != null)
        {
            BuildButton.GetComponentInChildren<Text>().text = "" + species.CostNext();
            BuildButton.interactable = game.CanBuild(species);

            DestroyButton.GetComponentInChildren<Text>().text = "" + species.CostPrevious();
            DestroyButton.interactable = game.CanDestroy(species);

            Quantity.text = "" + species.count;
        }
    }
}
