//
// Created by krock on 31/01/2026.
//

#ifndef KENGINE_MAP_H
#define KENGINE_MAP_H

#include <list>
#include "Tile.h"

class Map {

public:
    Map();
    virtual ~Map();


protected:

private:
    int nb_tiles_along_x;
    int nb_tiles_along_y;

    std::list<Tile> l_map;


};


#endif //KENGINE_MAP_H