using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{
    [HideInInspector]
    public string WordString;
    [HideInInspector]
    public bool WordReady = false;
    public Symbol SymbolPrefab;
    public GameObject ClusterPrefab;
    public List<Symbol> Symbols;

  
    private void Start()
    {
        print(WordString);
        SpawnSymbols();
    }
    public void SpawnSymbols()
    {
        for (int i = 0; i < WordString.Length; i++)
        {
            Symbol s = Instantiate(SymbolPrefab, transform);
            s.SetText(WordString[i].ToString());
            Symbols.Add(s);
        }
        WordReady = true;
    }
    public void CreateCluster(int s1, int s2)
    {
        GameObject cluster = Instantiate(ClusterPrefab, transform);
        cluster.name = "Cluster2";
        Symbols[s1].transform.SetParent(cluster.transform, false);
        Symbols[s2].transform.SetParent(cluster.transform, false);
        ClusterId id = cluster.GetComponent<ClusterId>();
        id.Key = Symbols[s1].Text.text + Symbols[s2].Text.text;
    }
    public void CreateCluster(int s1, int s2, int s3)
    {
        GameObject cluster = Instantiate(ClusterPrefab, transform);
        cluster.name = "Cluster3";
        Symbols[s1].transform.SetParent(cluster.transform, false);
        Symbols[s2].transform.SetParent(cluster.transform, false);
        Symbols[s3].transform.SetParent(cluster.transform, false);
        ClusterId id = cluster.GetComponent<ClusterId>();
        id.Key = Symbols[s1].Text.text + Symbols[s2].Text.text + Symbols[s3].Text.text;
    }
    public void CreateCluster(int s1, int s2, int s3, int s4)
    {
        GameObject cluster = Instantiate(ClusterPrefab, transform);
        cluster.name = "Cluster4";
        Symbols[s1].transform.SetParent(cluster.transform, false);
        Symbols[s2].transform.SetParent(cluster.transform, false);
        Symbols[s3].transform.SetParent(cluster.transform, false);
        Symbols[s4].transform.SetParent(cluster.transform, false);
        ClusterId id = cluster.GetComponent<ClusterId>();
        id.Key = Symbols[s1].Text.text + Symbols[s2].Text.text + Symbols[s3].Text.text + Symbols[s4].Text.text;
    }
}
