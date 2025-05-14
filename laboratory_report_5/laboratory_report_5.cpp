#include <iostream>

using std::cin;
using std::cout;

static void variant6()
{
    int num;
    int x = 2;
    int root, a, b, c;

    cout << "Enter a: ";
    cin >> a;

    cout << "Enter b: ";
    cin >> b;

    cout << "Enter c: ";
    cin >> c;
    
    // num = (ax^2 + bx + c)/2;
    __asm {
        mov ecx, 0
        mov eax, x
        mul x
        mov x, eax // x^2

        mov eax, a
        mul x // ax^2

        add ecx, eax // ecx += ax^2

        mov x, 2
        mov eax, b
        mul x
        mov x, eax // bx
        
        add ecx, eax // ecx += bx
        
        mov eax, c // c

        add ecx, eax // ecx += c

        mov x, 2
        mov eax, ecx
        mov edx, 0
        div x 
        mov x, eax // div by 2
        
        mov num, eax // result
    }
    
    cout << "result:" << num << '\n';
}

int main()
{
    variant6();
    return 0;
}
