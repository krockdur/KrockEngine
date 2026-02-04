//
// Created by JF on 04/02/2026.
//

#include "../ScreenManager.h"
#include "../headers/ScreenManager.h"

ScreenManager::ScreenManager() {
    init();
}

ScreenManager::~ScreenManager() {

}

void ScreenManager::init() {

    screen_state = menu;
}

void ScreenManager::update(sf::Time elapsed_time) {

    //Pour test
    if (sf::Keyboard::isKeyPressed(sf::Keyboard::Key::M)) {
        screen_state = menu;
    }
    if (sf::Keyboard::isKeyPressed(sf::Keyboard::Key::P)) {
        screen_state = play;
    }
    if (sf::Keyboard::isKeyPressed(sf::Keyboard::Key::S)) {
        screen_state = score;
    }

    switch (screen_state) {
        case menu:
            break;
        case play:
            break;
        case score:
            break;
        default:
            break;

    }
}

void ScreenManager::draw() {
    switch (screen_state)
    {
        case menu:
            menuScreen.draw( &game_window );
            break;
        case play:
            game.draw( &game_window );
            break;
        case score:
            break;
        default:
            break;
    }
}

void ScreenManager::run() {

}
