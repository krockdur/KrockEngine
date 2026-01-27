#ifndef ATLAS_H
#define ATLAS_H

#include <list>
#include "Sprite.h"

class Atlas
{
    public:
        Atlas();
        virtual ~Atlas();

        static std::list<Sprite> atlas;

    protected:

    private:
};

#endif // ATLAS_H
