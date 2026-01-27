#ifndef MAP_H
#define MAP_H

#include <list>
#include "Tile.h"

class Map
{
    public:
        Map();
        virtual ~Map();


    protected:

    private:
        int nb_tiles_along_x;
        int nb_tiles_along_y;

        std::list<Tile> l_map;

};

#endif // MAP_H
