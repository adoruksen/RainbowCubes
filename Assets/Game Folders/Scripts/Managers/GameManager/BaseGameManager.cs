using System;
using UnityEngine;



/// <summary>
/// Game Managers should inherit this class.
/// </summary>
public abstract class BaseGameManager : MonoBehaviour
{
    public delegate void GameEvents();
    /// <summary>
    /// Subscribe to GameEvents if you like to know about level activity.
    /// To trigger an event, simply override and call [base.x()] one of the overriden methods (OnLevelStart).
    /// </summary>
    public static event GameEvents OnLevelStart;
    public static event GameEvents OnLevelFail;
    public static event GameEvents OnLevelComplete;

    /// <summary>
    /// Used to save level integer to PlayerPrefs.
    /// </summary>
    private const string LevelSaveString = "level";

    /// <summary>
    /// Flag to control GameManager PlayerPref level saves. Set this to false to disable saving levels.
    /// Editor only.
    /// </summary>
    public bool ShouldSaveProgress = true;

    /// <summary>
    /// Returns the current level. You should override this method.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual int GetLevel()
    {
        Debug.LogError("Please override this method first!");
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns the current level in a string format, such as "level_1". You should override this method.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public virtual string GetLevelString()
    {
        Debug.LogError("Please override this method first!");
        throw new NotImplementedException();
    }



    /// <summary>
    /// Override and call this method to save level (as an integer).
    /// You can skip saving by setting ShouldSaveProgress to false, although this only works in the Editor.
    /// </summary>
    /// <param name="targetLevel"></param>
    public virtual void SaveLevel(int targetLevel)
    {
        if (Application.isEditor && !ShouldSaveProgress)
        {
            Debug.Log("ShouldSaveProgress is false. Saving is disabled.");
            return;
        }

        PlayerPrefs.SetInt(LevelSaveString, targetLevel);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Returns the saved level from PlayerPrefs using the constant key LevelSaveString.
    /// </summary>
    /// <returns></returns>
    public int GetSavedLevel()
    {
        return PlayerPrefs.GetInt(LevelSaveString);
    }

    /// <summary>
    /// Call this method to record LevelStart event 
    /// </summary>
    protected void LevelStart()
    {
        OnLevelStart?.Invoke();
    }
}