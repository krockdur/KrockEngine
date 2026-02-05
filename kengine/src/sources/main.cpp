#include <SFML/Graphics.hpp>

#include <iostream>

#include "../headers/ScreenManager.h"

int main()
{
    ScreenManager screenManager;
    screenManager.init();
    screenManager.run();
    return 0;
}
