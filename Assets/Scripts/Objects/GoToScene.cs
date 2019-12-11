using UnityEngine;
using UnityEngine.SceneManagement;

public enum TypePortal { EnterDungeon, ExitDungeon, EnterCity, GoHome, GoShop};

public class GoToScene : MonoBehaviour
{
    public TypePortal typePortal;

    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!collision2D.collider.CompareTag("Player")) { return; }
        switch (typePortal)
        {
            case TypePortal.EnterDungeon:
                SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
                break;

            case TypePortal.ExitDungeon:
                Game.Save();
                SceneManager.LoadScene("House", LoadSceneMode.Single);
                break;

            case TypePortal.GoHome:
                SceneManager.LoadScene("House", LoadSceneMode.Single);
                break;

            case TypePortal.EnterCity:
                SceneManager.LoadScene("Town", LoadSceneMode.Single);
                break;
            case TypePortal.GoShop:
                SceneManager.LoadScene("Shop", LoadSceneMode.Single);
                break;
            default:
                break;
        };
    }
}
