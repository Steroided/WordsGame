using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEditor.PlayerSettings;

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
    public void CreateCluster(int s1, int s2, Transform clusterBase)
    {
        GameObject cluster = Instantiate(ClusterPrefab, clusterBase);
        cluster.name = "Cluster2";
        Symbols[s1].transform.SetParent(cluster.transform, false);
        Symbols[s2].transform.SetParent(cluster.transform, false);
        Cluster id = cluster.GetComponent<Cluster>();
        id.Key = Symbols[s1].Text.text + Symbols[s2].Text.text;
    }
    public void CreateCluster(int s1, int s2, int s3, Transform clusterBase)
    {
        GameObject cluster = Instantiate(ClusterPrefab, clusterBase);
        cluster.name = "Cluster3";
        Symbols[s1].transform.SetParent(cluster.transform, false);
        Symbols[s2].transform.SetParent(cluster.transform, false);
        Symbols[s3].transform.SetParent(cluster.transform, false);
        Cluster id = cluster.GetComponent<Cluster>();
        id.Key = Symbols[s1].Text.text + Symbols[s2].Text.text + Symbols[s3].Text.text;
    }
    public void CreateCluster(int s1, int s2, int s3, int s4, Transform clusterBase)
    {
        GameObject cluster = Instantiate(ClusterPrefab, clusterBase);
        cluster.name = "Cluster4";
        Symbols[s1].transform.SetParent(cluster.transform, false);
        Symbols[s2].transform.SetParent(cluster.transform, false);
        Symbols[s3].transform.SetParent(cluster.transform, false);
        Symbols[s4].transform.SetParent(cluster.transform, false);
        Cluster id = cluster.GetComponent<Cluster>();
        id.Key = Symbols[s1].Text.text + Symbols[s2].Text.text + Symbols[s3].Text.text + Symbols[s4].Text.text;
    }
    public void ValidateClusters()
    {
        Cluster[] clusters = GetComponentsInChildren<Cluster>();
        string check = string.Empty;
        print(clusters.Length);
        for(int i = clusters.Length-1; i >=0 ; i--)
        {
            check += clusters[i].Key;
        }
        print(check);
        if (check == WordString)
        {
            StartCoroutine(SetColorIE(clusters, Color.green));
        }
        else
        {
            StartCoroutine(SetColorIE(clusters, Color.red));
        }
    }
    private IEnumerator SetColorIE(Cluster[] clusters, Color colorEvent)
    {
        foreach (Cluster cluster in clusters)
        {         
            cluster.SetClusterColor(colorEvent);
        }
        yield return new WaitForSeconds(2);
        foreach (Cluster cluster in clusters)
        {
            cluster.SetClusterColor(cluster.DefaultColor);
        }
    }
}
