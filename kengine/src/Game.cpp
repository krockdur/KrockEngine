//
// Created by krock on 06/02/2026.
//

#include "Game.h"

Game::Game() {

}

void Game::run() {
    _sm.init();
    _sm.run();
}
