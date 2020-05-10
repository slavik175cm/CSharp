#include "pch.h"
#include "hashtable.h"

const int md = 1000000 + 3;
const int p1 = 31;
vector<pair<int, string>> hash_table[md];
bool taken[md];

void _cdecl insert(char* key, int value) {
	string str = key;
	int hash = get_hash(str);
	for (int i = 0; i < hash_table[hash].size(); i++)
		if (hash_table[hash][i].second == str) return;
	hash_table[hash].push_back(make_pair(value, str));
}

int _stdcall get(char* key) {
	string str = key;
	int hash = get_hash(str);
	for (int i = 0; i < hash_table[hash].size(); i++)
		if (hash_table[hash][i].second == str) return hash_table[hash][i].first;
	return 0;
}

void _stdcall extract(char* key) {
	string str = key;
	int hash = get_hash(str);
	for (int i = 0; i < hash_table[hash].size(); i++)
		if (hash_table[hash][i].second == str) {
			hash_table[hash].erase(hash_table[hash].begin() + i, hash_table[hash].begin() + i + 1);
			break;
		}
}

void _cdecl change(char* key, int new_value) {
	string str = key;
	int hash = get_hash(str);
	for (int i = 0; i < hash_table[hash].size(); i++)
		if (hash_table[hash][i].second == str) {
			hash_table[hash][i].first = new_value;
			break;
		}
}

bool _cdecl key_exist(char* key) {
	string str = key;
	int hash = get_hash(str);
	for (int i = 0; i < hash_table[hash].size(); i++)
		if (hash_table[hash][i].second == str) return true;
	return false;
}

int get_hash(string str) {
	//returns polynomial hash
	int hash = 0, p = 1;
	for (int i = 0; i < str.size(); i++) {
		up(hash, mult(str[i], p));
		p = mult(p, p1);
	}
	return hash;
}

void up(int& a, int b) { a = (0ll + a + b) % md; }

int mult(int a, int b) { return (1ll * a * b) % md; }