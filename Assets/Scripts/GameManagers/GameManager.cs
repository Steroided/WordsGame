using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Systems.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static GameVariables;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Word> _words;
    [SerializeField]
    public int _wordCount = 1;
    private string[] _wordStrings;
    [SerializeField]
    private Word _wordPrefab;
    [Header("Transforms")]
    [SerializeField]
    private Transform _wordsParent;
    [SerializeField]
    private Transform _clusterBase;
    [SerializeField]
    private Transform _winPanel;
    [Header("Buttons")]
    [SerializeField]
    private UnityEngine.UI.Button _clusterValidateButton;
    [SerializeField]
    private UnityEngine.UI.Button _nextLevel;
    [SerializeField]
    private UnityEngine.UI.Button _backMainMenu;
    
    private Dictionary<int,bool> _validatedOrder = new Dictionary<int, bool>();
    async void Awake()
    {
        
        RemoteConfigLoader.Instance.Init();
        Task waitAllTrue = Task.Run(() =>
        {
            do
            {
                Thread.Sleep(100);
                print("FetchNotCompleted");
            }
            while (!RemoteConfigLoader.Instance.Fetched);

        });
        await waitAllTrue;
      
        _wordStrings = RemoteConfigLoader.Instance.Words.AllWords.ContainsKey("Level" + SceneLoader.Instance.CurrentLevel) ? RemoteConfigLoader.Instance.Words.AllWords["Level"+ SceneLoader.Instance.CurrentLevel] : _wordStrings = RemoteConfigLoader.Instance.Words.AllWords["LevelDefault"];
        _clusterValidateButton.onClick.AddListener(()=> StartCoroutine(ValidateClusters(_wordStrings)));
        _nextLevel.onClick.AddListener(() => NextLevel());
        CreateWords(_wordStrings);
        CreateWordClusters();
    }
    private void NextLevel()
    {
        SceneLoader.Instance.LoadNextLevel();
    }
    private void Win()
    {
        for (int i = 0; i < _validatedOrder.Count; i++) 
        {
            _wordsParent.Find("Word" + _validatedOrder.ElementAt(i).Key).SetSiblingIndex(i);
        }
        _winPanel.gameObject.SetActive(true);
    }
    private void CreateWords(string[] words)
    {
        for (int i = 0; i < _wordCount; i++)
        {           
            Word w = Instantiate(_wordPrefab, _wordsParent);
            w.gameObject.name = "Word" + i;
            w.WordString = words[i];
            _words.Add(w);
        }
    }
    private void SetValidateButton(bool set) => _clusterValidateButton.gameObject.SetActive(set);

    private IEnumerator ValidateClusters(string[] words)
    {
        SetValidateButton(false);
        for(int i =0; i < _words.Count; i++)
        {
            _words[i].ValidateClusters(words);
            if (!_words[i].WordValidated)
            {
                StartCoroutine(_words[i].SetColorIE(Color.red, false));

            }
            else
            {
                if (!_validatedOrder.ContainsKey(i))
                _validatedOrder.Add(i, true);
                StartCoroutine(_words[i].SetColorIE(new Color(0, 0, 0, 0), true));
            }
        }

        yield return new WaitForSeconds(2);

        if (_validatedOrder.Keys.Count!=GameSettings.Instance.MaxWords && !_validatedOrder.Values.Any(v => v == false))
        {
            SetValidateButton(true);
            yield break;
        }
      
          
        print("win");
        Win();
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

}
