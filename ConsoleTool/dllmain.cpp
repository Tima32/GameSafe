#include "windows.h"
#include <iostream>

using std::cout;
using std::endl;

HWND wh = 0;

extern "C" _declspec(dllexport) void ConsoleToolInit()
{
    wh = GetConsoleWindow();
}
extern "C" _declspec(dllexport) bool GetMousePos(int* x, int* y)
{
    RECT r;
    POINT p;
    GetWindowRect(wh, &r);
    GetCursorPos(&p);

    *x = (p.x - r.left - 9) / 8;
    *y = (p.y - r.top - 30) / 16 /*- 1*/;
    static bool click = 0;
    bool result;
    bool mouse = (GetAsyncKeyState(1) & 0x8000) || (GetAsyncKeyState(2) & 0x8000);
    result = click != mouse && mouse;
    click = mouse;
    return result;
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

