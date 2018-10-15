using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This class with only static members provides us an in-memory storage mechanism that persists
// betweeen scene loads.
public class PersistGM
{
    static public int score = 0;
}

public class GameManager : MonoBehaviour
{
    public Text ui_score;
    public Text ui_center;
    public string[] levels;
    public string menu_level;
    public string victory_level;
    private bool mutex_lock;

    void OnDestroy()
    {
        // TODO 441: Store the GameManager's score into PersistGM
    } 

    void Start()
    {
        // TODO 441: Restore the GameManager's score from PersistGM

        // This makes it so that the score is printed as 0 to start with.
        update_score_ui();
        clear_notice_ui();
    }

    // We use C# properties to automatically update the score UI whenever the GameManager's score 
    // attribute is modified.
    private int _score = 0;
    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            update_score_ui();
        }
    }

    // Call this function to indicate to the GameManager that the game is over
    public void game_over()
    {
        if (!mutex_lock)
        {
            Debug.Log("Game over!");
            Invoke("restart_game", 2);
            update_notice_ui("Game Over!");
            mutex_lock = true;
        }
    }

    // Call this function to indicate to the GameManager that the player has completed a level
    public void level_finished()
    {
        if (!mutex_lock)
        {
            string active_level = SceneManager.GetActiveScene().name;
            // We have a special-case behavior if we are on the 'menu' level
            if (active_level == menu_level)
            {
                Debug.Log("Starting the game");
                Invoke("start_game", 3);
                update_notice_ready_set_go();
            }
            else {
                Debug.Log("Finished a level! Congratulations!");
                Invoke("next_level", 2);
                update_notice_ui("Level Complete!");
            }
            mutex_lock = true;
        }
    }

    //-=-=-=--=--=-=-=-=-
    // Helper Functions
    //-=-=-=--=--=-=-=-=-
    private void restart_game()
    {
        Debug.Log("Loading menu: " + menu_level);
        score = 0;
        SceneManager.LoadSceneAsync(menu_level);
    }

    private void start_game()
    {
        Debug.Log("Loading first level: " + levels[0]);
        SceneManager.LoadSceneAsync(levels[0]);
    }

    private void next_level()
    {
        string active_level = SceneManager.GetActiveScene().name;
        for (int i = 0; i < levels.Length; i++)
        {
            if (active_level == levels[i])
            {
                if (i+1 < levels.Length)
                {
                    Debug.Log("Loading next level: " + levels[i+1]);
                    SceneManager.LoadSceneAsync(levels[i + 1]);
                    return;
                }
                else
                {
                    Debug.Log("Finished all the main levels! Going to load 'victory lap' scene: " + victory_level);
                    SceneManager.LoadSceneAsync(victory_level);
                    return;
                }
            }
        }
        // If we are playing a that is not in our 'levels' then we just reload the active level
        Debug.Log("Current scene not handled by our game manager: " + active_level);
        SceneManager.LoadSceneAsync(active_level);
    }

    private void update_score_ui()
    {
        ui_score.text = "Score: " + score.ToString();
    }

    // Again using the 
    private void update_notice_ready_set_go()
    {
        Invoke("ui_ready", 0.0F);
        Invoke("ui_set", 0.75F);
        Invoke("ui_go", 1.5F);
    }

    private void ui_ready() { update_notice_ui("Ready?"); }
    private void ui_set() { update_notice_ui("Set..."); }
    private void ui_go() { update_notice_ui("Go!"); }

    private void update_notice_ui(string str)
    {
        ui_center.text = str;
    }

    private void clear_notice_ui()
    {
        ui_center.text = "";
    }

}
