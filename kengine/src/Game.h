//
// Created by krock on 06/02/2026.
//

#ifndef KENGINE_GAME_H
#define KENGINE_GAME_H

#include "ScreenManager.h"

class Game {
public:
    Game();
    void run();

private:
    ScreenManager _sm;
};


#endif //KENGINE_GAME_H