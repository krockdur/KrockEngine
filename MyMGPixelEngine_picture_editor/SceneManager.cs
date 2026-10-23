

using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MyMGPixelEngine;

internal class SceneManager
{
    private Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();
    private Scene _activeScene;
    private string _activeSceneKey;

    // Permet d'ajouter une scène au gestionnaire
    public void AddScene(string key, Scene scene)
    {
        _scenes[key] = scene;
    }

    // Change la scène active par sa clé
    public void Change(string key)
    {
        if (_scenes.ContainsKey(key))
        {
            _activeSceneKey = key;
            _activeScene = _scenes[key];

            // Initialisation et chargement automatiques lors du changement (optionnel mais propre)
            _activeScene.Initialize();
            _activeScene.LoadContent();
        }
    }

    public void Initialize()
    {
        _activeScene?.Initialize();
    }

    public void LoadContent()
    {
        _activeScene?.LoadContent();
    }

    public void Update(GameTime gameTime)
    {
        _activeScene?.Update(gameTime, this);
    }

    public void Draw(ref FrameBuffer framebuffer)
    {
        _activeScene?.Draw(ref framebuffer);
    }
}

internal abstract class Scene
{
    // Initialisation des variables propres à la scène
    public virtual void Initialize() { }

    // Chargement des ressources (sprites, etc.)
    public virtual void LoadContent() { }

    // Logique de mise à jour (entrées clavier/souris, jeu)
    public virtual void Update(GameTime gameTime, SceneManager sceneManager) { }

    // Dessin des éléments de la scène dans le FrameBuffer
    public virtual void Draw(ref FrameBuffer framebuffer) { }
}