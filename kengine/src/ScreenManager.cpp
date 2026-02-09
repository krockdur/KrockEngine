//
// Created by JF on 04/02/2026.
//

#include "ScreenManager.h"

#include <iostream>

ScreenManager::ScreenManager() {
    std::cout << "screenManager created" << std::endl;
    init();
}

ScreenManager::~ScreenManager() {

}

void ScreenManager::init() {

    std::cout << "screenManager init" << std::endl;

    screen_state = menu;

    const sf::VideoMode desktop_resolution = sf::VideoMode::getDesktopMode();
    //game_window.create( desktop_resolution, Config::title );
    game_window.create(sf::VideoMode({Config::SCREEN_WIDTH, Config::SCREEN_HEIGHT}), Config::title );
    //game_window.setPosition({0, 0});
    game_window.setFramerateLimit(60);
    //game_window.setMouseCursorGrabbed(true);
    sf::Mouse::setPosition( {100, 200}, game_window);


}

void ScreenManager::update(sf::Time elapsed_time) {

    if (sf::Keyboard::isKeyPressed(sf::Keyboard::Key::M)) {
        std::cout<<"select menu"<<std::endl;
        screen_state = menu;
    }
    if (sf::Keyboard::isKeyPressed(sf::Keyboard::Key::P)) {
        std::cout<<"select play"<<std::endl;
        screen_state = play;
    }
    if (sf::Keyboard::isKeyPressed(sf::Keyboard::Key::S)) {
        std::cout<<"select score"<<std::endl;
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
            //menuScreen.draw( &game_window );
            break;
        case play:
            //game.draw( &game_window );
            break;
        case score:
            break;
        default:
            break;
    }
}

void ScreenManager::run() {
    while (game_window.isOpen())
    {
        while (const std::optional event = game_window.pollEvent())
        {
            if (event->is<sf::Event::Closed>()) {
                game_window.close();
                break;
            }
            //Pour test
            if (const auto* keyPress = event->getIf<sf::Event::KeyPressed>()){
                if (keyPress->code == sf::Keyboard::Key::Escape)
                {
                    std::cout<<"escape"<<std::endl;
                }
            }

            sf::Clock clock;
            update( clock.getElapsedTime() );
        }

        game_window.clear();
        draw();
        game_window.display();
    }
}
