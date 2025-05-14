#include <iostream>
#include <ostream>

using std::cout;
using std::cin;

constexpr double minutes_to_degrees = 60.0;
constexpr double seconds_to_degrees = 3600.0;
constexpr double _180 = 180.0;
constexpr double pi = 3.14;

int main(int argc, char* argv[])
{
    float degrees, minutes, seconds, radians;

    cout << "Input degrees: ";
    cin >> degrees;
    cout << "Input minutes: ";
    cin >> minutes;
    cout << "Input seconds: ";
    cin >> seconds;
    
    __asm
        {
        fld minutes             // S(0) = minutes
        fdiv minutes_to_degrees // S(1) = 60
        fld seconds_to_degrees  // S(2) = 3600
        fld seconds             // S(3) = seconds
        //fxch st(1)
        fdiv
        //faddp st(1), st(0) // ST0 = minutes/60 + seconds/360
        fadd

        fld degrees
        faddp st(1), st(0) // ST0 += degrees

        fld pi
        fdiv _180
        fmulp st(1), st(0) // ST0 *= PI

        fstp radians
        }

    cout << "Radians: " << '\n';
    cout << radians;

    return 0;
}
