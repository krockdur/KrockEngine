#ifndef SPRITE_H
#define SPRITE_H

#include <SFML/Graphics.hpp>
#include <string>

class Sprite
{
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

#endif // SPRITE_H
