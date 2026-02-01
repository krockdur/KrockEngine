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
    Map(int nb_tiles_along_x, int nb_tiles_along_y);
    virtual ~Map();

    void set_tile(int index_x, int index_y, Tile tile);
    Tile get_tile(int index_x, int index_y);
    int get_tile_mask(int index_x, int index_y);

    int get_nb_tiles_along_x();
    int get_nb_tiles_along_y();

    void clean_map();

protected:

private:
    int nb_tiles_along_x;
    int nb_tiles_along_y;

    std::list<Tile> l_map;


};


#endif //KENGINE_MAP_H