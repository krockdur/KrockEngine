//
// Created by Julien Fougery on 01/02/2026.
//

#ifndef KENGINE_GAME_H
#define KENGINE_GAME_H
#include "../../cmake-build-debug/_deps/freetype-src/include/freetype/ttnameid.h"


class Game {
    public:
    Game();

    void init();
    void update();
    void draw();
};


#endif //KENGINE_GAME_H