//
// Created by Julien Fougery on 01/02/2026.
//

#ifndef KENGINE_GAME_H
#define KENGINE_GAME_H


class Game {

public:
    Game();
    virtual ~Game();

    void init();
    void update();
    void draw();

    private:
};


#endif //KENGINE_GAME_H