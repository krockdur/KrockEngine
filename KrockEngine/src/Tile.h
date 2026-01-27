#ifndef TILE_H
#define TILE_H

#include <SFML/Graphics.hpp>


class Tile
{
    public:
        Tile();
        virtual ~Tile();

    protected:

    private:
        int pos_x;
        int pos_y;

};

#endif // TILE_H
