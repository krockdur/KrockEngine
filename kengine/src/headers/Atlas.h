//
// Created by krock on 31/01/2026.
//

#ifndef KENGINE_ATLAS_H
#define KENGINE_ATLAS_H

#include <list>
#include "Sprite.h"

class Atlas {

public:
    Atlas();
    virtual ~Atlas();

    static std::list<Sprite> atlas;

protected:

private:

};


#endif //KENGINE_ATLAS_H