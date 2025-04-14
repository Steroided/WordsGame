using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{
    [HideInInspector]
    public string WordString;
    [HideInInspector]
    public bool WordReady = false;
    public bool WordValidated = false;
    public Symbol SymbolPrefab;
    public GameObject ClusterPrefab;
    public List<Symbol> Symbols;

  
    private void Start()
    {
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
    public void ValidateClusters(string[] words)
    {
        Cluster[] clusters = GetComponentsInChildren<Cluster>();
        string sumClusters = string.Empty;
        for(int i = clusters.Length-1; i >=0 ; i--)
        {
            sumClusters += clusters[i].Key;
        }
        foreach(string check in words)
        {
            if(sumClusters == check)
            {
                WordValidated = true;

                return;
            }
            else
            {
                WordValidated = false;
            }
        }

    }
    public IEnumerator SetColorIE(Color colorEvent, bool hold)
    {
        Cluster[] clusters = GetComponentsInChildren<Cluster>();
        foreach (Cluster cluster in clusters)
        {         
            cluster.SetClusterColor(colorEvent);
        }
        if (!hold)
        {
            yield return new WaitForSeconds(2);

            foreach (Cluster cluster in clusters)
            {
                cluster.SetClusterColor(cluster.DefaultColor);
            }
        }

       
    }
}
