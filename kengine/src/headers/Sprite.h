//
// Created by krock on 31/01/2026.
//

#ifndef KENGINE_SPRITE_H
#define KENGINE_SPRITE_H

#include <SFML/Graphics.hpp>
#include <string>

class Sprite {

public:
    Sprite();
    virtual ~Sprite();

    sf::Sprite get_sprite();

protected:

private:
    // filename (jpg, png, etc...)
    std::string _picture_filename;

    //
    sf::Texture _texture;

    //
    sf::IntRect _texture_rect;

    // Mask
    bool _sprite_mask[10];

};


#endif //KENGINE_SPRITE_H