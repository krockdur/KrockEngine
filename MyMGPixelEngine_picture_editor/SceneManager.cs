

using System.Diagnostics;
using Microsoft.Xna.Framework;

internal class SceneManager
{
    private string _activeScene;
    public SceneManager()
    {

    }

    public void Change(string name)
    {
        switch (name)
        {
            case "menu":
                _activeScene = "menu";
                break;
            case "editor":
                _activeScene = "editor";
                break;
            case "game":
                _activeScene = "game";
                break;
            default:
                _activeScene = "none";
                break;
        }
    }
}

internal class Scene
{

}