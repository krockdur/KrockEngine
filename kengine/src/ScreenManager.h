//
// Created by JF on 04/02/2026.
//

#ifndef KENGINE_SCREENMANAGER_H
#define KENGINE_SCREENMANAGER_H

#include <SFML/Graphics.hpp>

#include "ConfigGame.h"

class ScreenManager {

private:
    sf::RenderWindow game_window;
    enum ScreenState { menu, play, score };
    ScreenState screen_state;
public:
    ScreenManager();
    ~ScreenManager();
    void init();
    void update( sf::Time elapsed_time );
    void draw();
    void run();

};


#endif //KENGINE_SCREENMANAGER_H