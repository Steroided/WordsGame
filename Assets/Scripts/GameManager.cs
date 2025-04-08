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
using static GameVariables;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Word> _words;

    public int WordCount = 1;
    public string WordString = "кластер";
    public Transform WordsParent;

    public Word WordPrefab;

    void Awake()
    {

    }
    void Start()
    {
        CreateWords();

        
        CreateWordClusters();



    }
   
    private void CreateWords()
    {
        for (int i = 0; i < WordCount; i++)
        {           
            Word w = Instantiate(WordPrefab, WordsParent);
            w.WordString = WordString;
            _words.Add(w);
        }
    }
    private void PickWord()
    {

    }
    private void PickMiddle()
    {

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
                        word.CreateCluster(0, 1);
                        word.CreateCluster(2, 3);
                        word.CreateCluster(4, 5);
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
                        word.CreateCluster(0, 1, 2);
                        word.CreateCluster(3, 4, 5);

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
                            word.CreateCluster(0, 1, 2, 3);
                            word.CreateCluster(4, 5);
                        }
                        else
                        {
                            word.CreateCluster(0, 1);
                            word.CreateCluster(2, 3, 4, 5);

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
