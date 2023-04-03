using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Just: MonoBehaviour
{
    [SerializeField] string str;
    private void Start() {
        Stats s = Analyze(str);
        Debug.Log($"AllWords: {s.NumberOfAllWords}");
        Debug.Log($"NumberOfWordsStartingWithCapitalLetter: {s.NumberOfWordsStartingWithCapitalLetter}");
        Debug.Log($"NumberOfWordsStartingWithSmallLetter: {s.NumberOfWordsStartingWithSmallLetter}");
        Debug.Log($"NumberOfWordsThatContainOnlyDigits: {s.NumberOfWordsThatContainOnlyDigits}");
        Debug.Log($"TheLongestWord: {s.TheLongestWord}");
        Debug.Log($"TheShortestWord: {s.TheShortestWord}");
    }

    public Stats Analyze(string document)
    {
        if(document == "")
                return new Stats();
        if(string.IsNullOrEmpty(document))
            throw new ArgumentNullException("Parameter cannot be null");
        
        char[] inChar = document.ToCharArray();
        Stats stats = new Stats();
        stats.NumberOfAllWords = 0;
        stats.NumberOfWordsStartingWithCapitalLetter = 0;
        stats.NumberOfWordsStartingWithSmallLetter = 0;
        stats.NumberOfWordsThatContainOnlyDigits = 0;
        stats.TheLongestWord = "";
        stats.TheShortestWord = "";
        int longest = int.MinValue;
        int shortest = int.MaxValue;

        string currentWord = "";
        int limit = inChar.Length - 1;

        for (int i = 0; i <= limit; i++) {
            if(!char.IsWhiteSpace(inChar[i])) {
                currentWord += inChar[i];
                if(i == limit)
                    StatUpdater(ref stats, ref shortest, ref longest, currentWord);
            } else {
                if(currentWord != "") {
                    StatUpdater(ref stats, ref shortest, ref longest, currentWord);
                    currentWord = ""; 
                }
            }
        }
        return stats;
    }

    private void StatUpdater(ref Stats stats,ref int shortestSoFar, ref int longestSoFar, string word) {
        stats.NumberOfAllWords++;
        if(char.IsUpper(word[0]))
            stats.NumberOfWordsStartingWithCapitalLetter++;
        if(char.IsLower(word[0]))
            stats.NumberOfWordsStartingWithSmallLetter++;
        if(CheckIfOnlyDigits(word))
            stats.NumberOfWordsThatContainOnlyDigits++;
        if(word.Length > longestSoFar) {
            stats.TheLongestWord = word;
            longestSoFar = word.Length;
        }
        if(word.Length < shortestSoFar) {
            stats.TheShortestWord = word;
            shortestSoFar = word.Length;
        }
    }

    private bool CheckIfOnlyDigits(string str) {
        for (int i = 0; i < str.Length; i++) {
            if(!char.IsDigit(str[i]))
                return false;
        }
        return true;
    }
}

public class Stats
{
    // Total number of all words in the document
    public int NumberOfAllWords { get; set; }

    // Returns number of words that consist only from digits e.g. 13455, 98374
    public int NumberOfWordsThatContainOnlyDigits { get; set; }

    // Returns number of words that start with a lower letter e.g. a, d, z
    public int NumberOfWordsStartingWithSmallLetter { get; set; }

    // Returns number of words that start with a capital letter e.g. A, D, Z
    public int NumberOfWordsStartingWithCapitalLetter { get; set; }

    // Returns the first longest word in the document
    public string TheLongestWord { get; set; }

    // Returns the first shortest word in the document
    public string TheShortestWord { get; set; }
}
