using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;
using static GameVariables;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Word> _words;
    [SerializeField]
    public int _wordCount = 1;
    private string _wordString = "класте";
    [SerializeField]
    private Word _wordPrefab;
    [SerializeField]
    private Transform _wordsParent;
    [SerializeField]
    private Transform _clusterBase;

    [SerializeField]
    private Button _clusterValidateButton;
    void Awake()
    {
        _clusterValidateButton.onClick.AddListener(ValidateClusters);
    }
    void Start()
    {
        CreateWords();     
        CreateWordClusters();
    }
   
    private void CreateWords()
    {
        for (int i = 0; i < _wordCount; i++)
        {           
            Word w = Instantiate(_wordPrefab, _wordsParent);
            w.WordString = _wordString;
            _words.Add(w);
        }
    }
    private void PickWord()
    {

    }
    private void PickMiddle()
    {

    }
    public void ValidateClusters()
    {
        foreach(Word w in _words)
        {
            w.ValidateClusters();
        }
    }
    private void CreateWordClusters()
    {
        for (int i = 0; i < _words.Count; i++)
        {
            CreateWordClusterEntity(_words[i]);
        }
    }
    private async void CreateWordClusterEntity(Word word)
    {
        //ждем пока слово соберётся , при этом не мешаем остальным словам
        Task waitAllTrue = Task.Run(() =>
        {
            do
            {
                Thread.Sleep(100);
                print("wordNotReady");
            }
            while (!word.WordReady);

        });


        await waitAllTrue;
        
        CreateWordCluster(word);
      
    }

    private void CreateWordCluster(Word word)
    {
        ClusterMode mod = (ClusterMode)UnityEngine.Random.Range(0, Enum.GetNames(typeof(ClusterMode)).Length);
        switch (mod)
        {

            case ClusterMode.two:
                {

                    if (word.WordString.Length % 2 == 0)
                    {
                        word.CreateCluster(0, 1,_clusterBase);
                        word.CreateCluster(2, 3, _clusterBase);
                        word.CreateCluster(4, 5, _clusterBase);
                    }
                    else
                    {

                    }
                }
                break;
            case ClusterMode.three:
                {
                    if (word.WordString.Length % 3 == 0)
                    {
                        word.CreateCluster(0, 1, 2, _clusterBase);
                        word.CreateCluster(3, 4, 5, _clusterBase);

                    }
                    else
                    {


                    }
                }
                break;
            case ClusterMode.four:
                {
                    if (word.WordString.Length % 4 == 0)
                    {

                    }
                    else
                    {
                        int rand = UnityEngine.Random.Range(0, 2);
                        print(rand);
                        if (rand == 0)
                        {
                            word.CreateCluster(0, 1, 2, 3, _clusterBase);
                            word.CreateCluster(4, 5, _clusterBase);
                        }
                        else
                        {
                            word.CreateCluster(0, 1, _clusterBase);
                            word.CreateCluster(2, 3, 4, 5, _clusterBase);

                        }
                    }
                }
                break;

        }
        Debug.Log(word.Symbols.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
