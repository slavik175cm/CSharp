#pragma once

#include <iostream>
#include <vector> 
#include <string>
using namespace std;

extern "C" __declspec(dllexport) void _cdecl insert(char* key, int value);
extern "C" __declspec(dllexport) int _stdcall get(char* key);
extern "C" __declspec(dllexport) void _stdcall extract(char* key);
extern "C" __declspec(dllexport) void _cdecl change(char* key, int new_value);
extern "C" __declspec(dllexport) bool _cdecl key_exist(char* key);


int get_hash(string str);
void up(int& a, int b);
int mult(int a, int b);
