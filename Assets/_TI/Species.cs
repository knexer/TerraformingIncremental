using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Species : MonoBehaviour
{
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] private string[] BonusDescriptions;
    [SerializeField] private string[] BonusFromDescriptions;
    [SerializeField] private int InitialCost;
    [SerializeField] private float CostExponent = 1.1f;
    [SerializeField] public int BiomassIncome;

    [HideInInspector] public int count = 0;

    public int TotalIncome => count * BiomassIncome;

    private int CostFor(int numExisting)
    {
        return (int) (Mathf.Pow(CostExponent, numExisting) * InitialCost);
    }

    public int CostNext()
    {
        return CostFor(count);
    }

    public int? CostPrevious()
    {
        return count > 0 ? (int?)CostFor(count - 1) : null;
    }

    public void Build()
    {
        count++;
    }

    public void Destroy()
    {
        count--;
    }
}
