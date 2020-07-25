using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Species[] species;
    [SerializeField] private SpeciesView speciesViewPrefab;
    [SerializeField] private RectTransform speciesViewParent;
    [SerializeField] private Text BiomassDisplay;
    [SerializeField] private int InitialBiomass = 10;

    [HideInInspector] public int Biomass = 10;
    [HideInInspector] public float leftoverBiomass = 0f;

    private void Start()
    {
        Biomass = InitialBiomass;
        foreach (Species s in species)
        {
            SpeciesView view = Instantiate(speciesViewPrefab, speciesViewParent, false);
            view.Init(this, s);
        }
    }

    private void Update()
    {
        int biomassPerSecond = species.Sum(s => s.TotalIncome);
        float biomassPerTick = Time.deltaTime * biomassPerSecond;
        float biomassThisTick = biomassPerTick + leftoverBiomass;
        leftoverBiomass = biomassThisTick - Mathf.FloorToInt(biomassThisTick);
        Biomass += Mathf.FloorToInt(biomassThisTick);
        BiomassDisplay.text = "Biomass: " + Biomass;
    }

    public bool CanBuild(Species s)
    {
        return Biomass >= s.CostNext();
    }

    public bool CanDestroy(Species s)
    {
        return s.CostPrevious() != null;
    }

    public void Build(Species s)
    {
        if (!CanBuild(s)) return;

        Biomass -= s.CostNext();
        s.Build();
    }
    public void Destroy(Species s)
    {
        if (!CanDestroy(s)) return;

        Biomass += s.CostPrevious().Value;
        s.Destroy();
    }
}

